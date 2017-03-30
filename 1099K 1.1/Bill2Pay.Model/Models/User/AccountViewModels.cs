using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bill2Pay.Model
{
    /// <summary>
    ///  Externa lLogin Confirmation View Model
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// External LoginList View Model
    /// </summary>
    public class ExternalLoginListViewModel
    {
        /// <summary>
        /// Return Url
        /// </summary>
        public string ReturnUrl { get; set; }
    }

    /// <summary>
    /// Send Code View Model
    /// </summary>
    public class SendCodeViewModel
    {
        /// <summary>
        /// Selected Provider
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Providers
        /// </summary>
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }

        /// <summary>
        /// ReturnUrl 
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Remember flag
        /// </summary>
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Structure of Verify Code View Model
    /// </summary>
    public class VerifyCodeViewModel
    {
        /// <summary>
        /// Provider
        /// </summary>
        [Required]
        public string Provider { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// Return Url
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Remember Browser flag
        /// </summary>
        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        /// <summary>
        /// Remember flag
        /// </summary>
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Forgot password View Model
    /// </summary>
    public class ForgotViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// Login View Model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Remember flag
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Register View Model
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm Password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Structure of Reset Password View Model
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    /// <summary>
    /// Structure of Forgot Password View Model
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
