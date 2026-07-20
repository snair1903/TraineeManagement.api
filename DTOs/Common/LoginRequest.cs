namespace TraineeManagement.api.DTOs;

using System.ComponentModel.DataAnnotations;
public class LoginRequest
{
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string Password { get; set; } = string.Empty;
}