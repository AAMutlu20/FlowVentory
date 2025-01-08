using System.ComponentModel.DataAnnotations;

namespace FlowVentoryApp.Models.ViewModels;

public class RegisterVM
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; }
    
    [Required] [EmailAddress] public string Email { get; set; }
    
    [Required]
    [StringLength(100)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}