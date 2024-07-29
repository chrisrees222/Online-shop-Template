using System.ComponentModel.DataAnnotations;

namespace Online_shop_Template.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Address Is Required")]
        [Display(Name = "Current Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your City/Town")]
        [Display(Name = "Enter City/ Town")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter your County/State")]
        [Display(Name = "Enter County/state")]
        public string State { get; set; }
        [Required(ErrorMessage = "Post Code is required")]
        [Display(Name = "Post Code")]
        public string PostalCode { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
