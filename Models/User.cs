using System;
using System.ComponentModel.DataAnnotations;
 
namespace login_registration.Models
{
    public class User : BaseEntity
    
    {   
        [Required(ErrorMessage="first name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="first name may only contain letters")]
        [MinLength(2, ErrorMessage="first name must be at least two characters long")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage="last name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="last name may only contain letters")]
        [MinLength(2, ErrorMessage="last name must be at least two characters long")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        
        [Required(ErrorMessage="e-mail address is required")]
        [EmailAddress(ErrorMessage="e-mail address must be valid")]
        [Display(Name = "e-mail")]
        public string Email { get; set; }
        
        [Required(ErrorMessage="password is required")]
        [MinLength(8, ErrorMessage="password must be at least eight characters long")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [CompareAttribute("Password", ErrorMessage = "passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confrimation")]
        public string PasswordConfirmation { get; set; }
    }

    public class UserLogin : BaseEntity
    
    {   
        [Required(ErrorMessage="e-mail address is required")]
        [EmailAddress(ErrorMessage="e-mail address must be valid")]
        [Display(Name = "e-mail")]
        public string Email { get; set; }
        
        [Required(ErrorMessage="password is required")]
        [MinLength(8, ErrorMessage="password must be at least eight characters long")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

}