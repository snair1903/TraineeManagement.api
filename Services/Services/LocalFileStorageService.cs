using System.Security.Cryptography;
using TraineeManagement.api.Data;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Exceptions;
using TraineeManagement.api.Models;
namespace TraineeManagement.api.Services;

public class LocalFileStorageService : IFileStorageService
{
    // private string[] permittedExtensions = { ".txt", ".pdf" };
    private readonly string _storageDirectory;
    private readonly AppDbContext _SubmissionFileContext;
    // private long sizeLt =  10*1024*1024;

    public LocalFileStorageService(IConfiguration configuration, AppDbContext context)
    {
        _SubmissionFileContext = context;
        var baseDirectory = configuration.GetSection("StorageRoot");
        _storageDirectory = Path.GetFullPath(baseDirectory["StoragePath"]!);
        Directory.CreateDirectory(_storageDirectory);
    }

    public async Task<SubmissionFileResponse> SaveAsync(CreateSubmissionFileRequest createSubmissionFileRequest, int UploadedById, int submissionId)
    {
         var Submission =await _SubmissionFileContext.Submissions.FindAsync(submissionId);
        if (Submission == null)
        {
            throw new NotFoundException("Submission not found at Id"+submissionId);
        }
        IFormFile file = createSubmissionFileRequest.File;
        string fileId = Guid.NewGuid().ToString("N");
        using var sha256 = SHA256.Create();
        using var stream = file.OpenReadStream();
        byte[] hashBytes = sha256.ComputeHash(stream);
        string ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        // if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext)) throw new UnsupportedMediaTypeException("Unallowed file type");
        // if(file.Length>sizeLt||file.Length==0) throw new RequestEntityTooLargeException("File size greater than 10mb or Empty");
        string checksum = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        stream.Position = 0;
        var metaData = new SubmissionFile
        {
            OriginalFileName = file.FileName,
            SubmissionId = submissionId,
            FileStorageName = fileId + ext,
            Size = file.Length,
            ContentType = file.ContentType,
            Checksum = checksum,
            UploadedById = UploadedById
        };
        await _SubmissionFileContext.SubmissionFiles.AddAsync(metaData);
        await _SubmissionFileContext.SaveChangesAsync();
        string path = Path.Combine(_storageDirectory, fileId);
        string fullpath = Path.GetFullPath(path);
        fullpath = fullpath + ext;
        using var fileStream = new FileStream(fullpath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
        await stream.CopyToAsync(fileStream);
        return new SubmissionFileResponse(metaData);

    }

    public async Task<FileStream> OpenReadAsync(int Id)
    {
        var sub = await _SubmissionFileContext.SubmissionFiles.FindAsync(Id);
        if (sub == null) throw new NotFoundException($"File not found at Id{Id}");
        string fileId = sub.FileStorageName;

        if (!await ExistsAsync(fileId))
        {
            throw new NotFoundException($"The file with ID {Id} was not found.");
        }
        string path = Path.Combine(_storageDirectory, fileId);
        string filePath = Path.GetFullPath(path);

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        return stream;
    }

    public async Task<bool> ExistsAsync(string fileId)
    {
        string filePath = Path.Combine(_storageDirectory, fileId.ToString());
        return File.Exists(filePath);
    }

    public async Task DeleteAsync(int Id)
    {
        var sub = await _SubmissionFileContext.SubmissionFiles.FindAsync(Id);
        if (sub == null) throw new NotFoundException($"File not found at Id{Id}");
        string fileId = sub.FileStorageName;
        if (!await ExistsAsync(fileId))
        {
            throw new NotFoundException($"The file with ID {Id} was not found.");
        }
        string path = Path.Combine(_storageDirectory, fileId);
        string filePath = Path.GetFullPath(path);
        File.Delete(filePath);
        _SubmissionFileContext.SubmissionFiles.Remove(sub);
        await _SubmissionFileContext.SaveChangesAsync();

    }
}
