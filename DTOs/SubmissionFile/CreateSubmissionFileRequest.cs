namespace TraineeManagement.api.DTOs;
using TraineeManagement.api.Attributes;
using System.ComponentModel.DataAnnotations;
public class CreateSubmissionFileRequest{
    [Required]
    [AllowedExtensions([".jpg", ".png", ".pdf"])]
    public IFormFile File { get; set; } = null!;

}