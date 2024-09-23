using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class SignUpViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare(nameof(Password))]         //Password ve PasswordConfirm i kıyasya
        public string PasswordConfirm { get; set; }

    }
}
