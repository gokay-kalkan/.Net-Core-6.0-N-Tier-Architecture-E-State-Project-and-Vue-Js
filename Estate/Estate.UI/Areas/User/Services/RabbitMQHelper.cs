using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using NuGet.Protocol.Plugins;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using IModel = RabbitMQ.Client.IModel;

namespace Estate.UI.Areas.User.Services
{
    public class RabbitMQHelper
    {
        private readonly ConnectionFactory _factory;
        private readonly IModel _channel;

        public RabbitMQHelper()
        {
            _factory = new ConnectionFactory();
            _factory.Uri = new Uri("amqps://qdeanzbw:KaLks6V_qmki8RY7l9IsXJ5ECb7OIN-8@beaver.rmq.cloudamqp.com/qdeanzbw");
            var connection = _factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "password_reset_request", durable: false, exclusive: false, autoDelete: false, arguments: null);
            
            
//durable: Eğer true değeri verilirse, kuyruk kapatıldığında mesajlarını kaybetmez ve kuyruk tekrar açıldığında mesajlarını korur. Eğer false değeri verilirse, mesajlar kapatıldıktan sonra silinir.
//exclusive: Eğer true değeri verilirse, kuyruk sadece oluşturulan bağlantı tarafından kullanılabilir. Eğer false değeri verilirse, kuyruk paylaşılabilir ve farklı bağlantılar tarafından da kullanılabilir.
//autoDelete:Bu parametre, kuyruğun otomatik olarak silinip silinmeyeceğini belirtir. Eğer true değeri verilirse, kuyruk içerisinde hiç mesaj kalmayınca kuyruk otomatik olarak silinir.
        }

        public void SendPasswordResetRequest(string email, string passwordResetLink)
        {
            string message = $"{email}|{passwordResetLink}";
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "password_reset_request", basicProperties: null, body: body);
//exchange:mesajın direk kuyruğa iletilmesini sağlar.

//basicproperties: rametre, mesajın özelliklerini belirtir. Örneğin, mesajın düşürülmemesi için 'persistent' özelliği verilebilir. Bu özellik, mesajın RabbitMQ sunucusunda sürekli olarak saklanmasını sağlar.
        }

        public void ConsumePasswordResetRequests(Action<string, string> onReceived) //senide işlyen method PasswordResetRequestHandler bu sınıf içinde start handlingde çağırılıyorsun controllerda o method ile tetiklenecek
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                // İstek bilgilerini alın
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                string[] parts = message.Split('|');
                string email = parts[0];
                string passwordresetlink = parts[1];

                onReceived(email, passwordresetlink);
            };
            _channel.BasicConsume(queue: "password_reset_request", autoAck: true, consumer: consumer);
        }
    }

}
