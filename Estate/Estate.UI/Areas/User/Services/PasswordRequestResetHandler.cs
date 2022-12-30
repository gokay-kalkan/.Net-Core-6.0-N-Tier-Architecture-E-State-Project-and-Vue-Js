using Microsoft.AspNetCore.Mvc;
using System;

namespace Estate.UI.Areas.User.Services
{
    public class PasswordResetRequestHandler
    {
        private RabbitMQHelper _rabbitMQHelper;


        public PasswordResetRequestHandler(RabbitMQHelper rabbitMQHelper)
        {
            _rabbitMQHelper = rabbitMQHelper;
        }

        public void StartHandling() //bu methodda rabitmqhelper içindeki kuyruktaki isteği işleyen mail göndermeye yarayan methodu çalıştıran bir method bu şekilde controllerda isteği alan ve işleyen yapıları daha sade hale getirdik
        {
            _rabbitMQHelper.ConsumePasswordResetRequests((email, passwordResetLink) =>
    {
        // Eposta gönderme işlemi gerçekleştirin
        MailHelper.ResetPassword.PasswordSendMail(passwordResetLink);
    });
        }
    }

}
