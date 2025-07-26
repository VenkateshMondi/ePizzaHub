using System.ComponentModel.DataAnnotations;

namespace ePizzaHub.UI.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5,ErrorMessage = "Password should be of minimum 5 characters")]
        [MaxLength(15, ErrorMessage = "Password should be of maximum 5 characters")]

        public string Password { get; set; } = default!;

    }
}
