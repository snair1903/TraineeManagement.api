namespace TraineeManagement.api.DTOs;

using System;
using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Models;
public class LoginRequest
{
    [Required(ErrorMessage = "UserName is required.")]
    [StringLength(50, ErrorMessage = "UserName cannot exceed 50 characters.")]
    public string UserName { get; set; } = "";
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
    public string Password { get; set; } = "";
}