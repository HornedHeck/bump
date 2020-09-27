using System.ComponentModel.DataAnnotations;

namespace Bump.Models
{
    public class RegistrationModel
    {
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }
        
    }
}