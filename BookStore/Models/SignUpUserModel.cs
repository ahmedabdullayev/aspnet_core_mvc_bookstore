using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please provide your email")]
        [EmailAddress(ErrorMessage = "Please provide valid email")]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please enter password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password doesnt match")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}