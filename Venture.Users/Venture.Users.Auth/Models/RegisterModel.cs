using System.ComponentModel.DataAnnotations;

namespace Venture.Users.Auth.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
