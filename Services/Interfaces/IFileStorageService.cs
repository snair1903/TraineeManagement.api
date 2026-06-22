using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;

namespace TraineeManagement.api.Services;
public interface IFileStorageService
{
    // Returns the generated Guid so you can save it to your database
    Task<SubmissionFileResponse> SaveAsync(CreateSubmissionFileRequest file,int UploadedById,int SubmissionId);
    Task<FileStream> OpenReadAsync(int fileId);
    Task<bool> ExistsAsync(string fileId);
    Task DeleteAsync(int fileId);
}
