namespace TraineeManagement.api.Models;

using System.ComponentModel.DataAnnotations;
using System.Xml;
using TraineeManagement.api.DTOs;

public class Mentor
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Expertise { get; set; } = string.Empty;
    [Required]
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