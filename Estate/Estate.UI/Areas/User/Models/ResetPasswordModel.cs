using System.ComponentModel.DataAnnotations;

namespace Estate.UI.Areas.User.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "Uygun formatta değil")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz")]
        [DataType(DataType.Password, ErrorMessage = "uygun formatta değil")]
        public string NewPassword { get; set; }
    }
}
