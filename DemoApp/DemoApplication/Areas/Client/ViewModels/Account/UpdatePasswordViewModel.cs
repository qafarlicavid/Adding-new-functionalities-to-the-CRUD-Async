using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Areas.Client.ViewModels.Account
{
    public class UpdatePasswordViewModel
    {
        public string CurrentPassword { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password is not same")]
        public string ConfirmPassword { get; set; }
    }
}
