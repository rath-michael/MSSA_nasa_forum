using System.ComponentModel.DataAnnotations;

namespace Week15Project.Models.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
