using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Index View 
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Has Password
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// List of Logins
        /// </summary>
        public IList<UserLoginInfo> Logins { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Two  Factor
        /// </summary>
        public bool TwoFactor { get; set; }

        /// <summary>
        /// Browser Remembered flag
        /// </summary>
        public bool BrowserRemembered { get; set; }
    }

    /// <summary>
    /// Manage Logins View Model
    /// </summary>
    public class ManageLoginsViewModel
    {
        /// <summary>
        /// LIst of Current Logins
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// LIst of Other Logins
        /// </summary>
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    /// <summary>
    /// Factor View Model
    /// </summary>
    public class FactorViewModel
    {
        /// <summary>
        /// Purpose
        /// </summary>
        public string Purpose { get; set; }
    }

    /// <summary>
    /// Set Password View Model
    /// </summary>
    public class SetPasswordViewModel
    {
        /// <summary>
        /// New Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm Password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Change Password View Model
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Old Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// New Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm Password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Add PhoneNumber View Model
    /// </summary>
    public class AddPhoneNumberViewModel
    {
        /// <summary>
        /// Phone Number
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    /// <summary>
    /// Verify Phone Number View Model
    /// </summary>
    public class VerifyPhoneNumberViewModel
    {
        /// <summary>
        /// Code
        /// </summary>
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    /// <summary>
    /// Configure Two Factor View Model
    /// </summary>
    public class ConfigureTwoFactorViewModel
    {
        /// <summary>
        /// Selected Provider
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// List of Provider
        /// </summary>
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}