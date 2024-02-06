using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearnAspNetCore.ViewModels;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    [Remote(action: "IsEmailInUse", controller: "Account")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage ="Password and ConfirmPassword do not match")]
    public string ConfirmPassword { get; set; }
}
