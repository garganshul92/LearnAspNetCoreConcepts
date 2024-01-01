using System.ComponentModel.DataAnnotations;

namespace LearnAspNetCore.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Name can't exceed 50 characters")]
    public string Name { get; set; }

    [Required]
    [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-93](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Invalid Email Format")]
    [Display(Name="Official Email")]
    public string Email { get; set; }
    public Dept Department { get; set; }
}
