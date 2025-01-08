using System.ComponentModel.DataAnnotations;

namespace FlowVentoryApp.Models.ViewModels;

public class LoginVM
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; }
    
    [Required]
    [StringLength(100)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public bool RememberMe { get; set; } = false;
}