namespace TraineeManagement.api.Models;

using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.DTOs;

public class Mentor
{
    public int Id { get; set; }
    [Required(ErrorMessage = "FirstName is required.")]
    [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters.")]
    public string FirstName { get; set; } = "";
    [Required(ErrorMessage = "LastName is required.")]
    [StringLength(50, ErrorMessage = "LastName cannot exceed 50 characters.")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Valid email is required")]
    public string Email { get; set; } = "";
    [Required]
    public string Expertise { get; set; } = "";
    [Required]
    [EnumDataType(typeof(MentorStatus), ErrorMessage = "Invalid Mentor status.")]
    public MentorStatus Status { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public Mentor(CreateMentorRequest mentor)
    {
        FirstName = mentor.FirstName;
        LastName = mentor.LastName;
        Email = mentor.Email;
        Expertise = mentor.Expertise;
        Status = mentor.Status;
        DateTime d = DateTime.Now;
        DateTime timestmp = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        CreatedDate = timestmp;
        UpdatedDate = timestmp;
    }

    public Mentor() { }
}