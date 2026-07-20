namespace TraineeManagement.api.Models;
public class SubmissionFile
{
    public int Id{get;set;}
    public string OriginalFileName {get;set;} = string.Empty;
    public int SubmissionId{get;set;}

    public string FileStorageName {get;set;} = string.Empty;

    public long Size{get;set;}

    public string ContentType{get;set;} = string.Empty;

    public string Checksum {get;set;} = string.Empty;

    public int UploadedById {get;set;}

    public DateTime Timestamp {get; set;}

   
}