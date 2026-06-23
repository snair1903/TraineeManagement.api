namespace TraineeManagement.api.Models;
public class SubmissionFileResponse
{
    public string OriginalFileName {get;set;} = "";

    // public string FileStorageName {get;set;} = "";

    public long Size{get;set;}

    public string ContentType{get;set;} = "";

    public string Checksum {get;set;} = string.Empty;

    // public int UploadedById {get;set;}

    public DateTime Timestamp {get; set;}


    public SubmissionFileResponse(SubmissionFile file)
    {
        OriginalFileName = file.OriginalFileName;
        Size = file.Size;
        ContentType = file.ContentType;
        Checksum = file.Checksum;
        Timestamp = DateTime.Now;
    }

}