using System.ComponentModel.DataAnnotations;

namespace FCVT.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordH { get; set; }
    }
}
