namespace TraineeManagement.api.Models;
public class CreateSubmissionFileRequest
{
    public string OriginalFileName {get;set;} = "";

    public int SubmissionId{get;set;}

    public string FileStorageName {get;set;} = "";

    public long Size{get;set;}

    public string ContentType{get;set;} = "";

    public string Checksum {get;set;} = string.Empty;

    public int UploadedById {get;set;}    

}