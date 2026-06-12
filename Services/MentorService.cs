using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;


public class MentorService : IMentorService
{
    private readonly AppDbContext _MentorContext;

    // private readonly ILogger<MentorService>  _logger;
    public MentorService(AppDbContext context,ILogger<MentorService> logger)
    {
        _MentorContext = context;
        // _logger = logger;
    }

    public async Task<PagedResponse<MentorResponse>> GetAll(int pageNumber, int pageSize,string? search,MentorStatus? userStatus)
    {
        var query = _MentorContext.Mentors.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(t => t.FirstName.ToLower().Contains(search) ||
            t.LastName.ToLower().Contains(search) || t.Email.ToLower().Contains(search) ||
            t.Expertise.ToLower().Contains(search));
        }

        if (userStatus != null)
        {
            query = query.Where(t => t.Status.ToString() == userStatus.ToString());
        }

        var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(t => new MentorResponse(t)).ToListAsync();
        // _logger.LogInformation("Get Success");
        return new PagedResponse<MentorResponse>( data,pageNumber,pageSize, data.Count());
    }


    public async Task<MentorResponse?> GetById(int Id)
    {
        var Mentor = _MentorContext.Mentors.FirstOrDefault(t => t.Id == Id);
        if (Mentor == null)
        {
            // _logger.LogInformation("Get Failure: No user found at id{}",Id);
            return null;
        }
        // _logger.LogInformation("Get Success: user found at id {}",Id);
        return new MentorResponse(Mentor);
    }

    public async Task<MentorResponse> Create(CreateMentorRequest Mentor)
    {

        var nt = new Mentor(Mentor);
        _MentorContext.Mentors.Add(nt);
        await _MentorContext.SaveChangesAsync();
        // _logger.LogInformation("Create Success: Mentor id {}",nt.Id);
        return new MentorResponse(nt);
    }

    public async Task<MentorResponse?> Update(int Id, UpdateMentorRequest updateMentorRequest)
    {
        var Mentor = _MentorContext.Mentors.FirstOrDefault(t => t.Id == Id);
        if (Mentor == null)
        {
            // _logger.LogInformation("Update Fail: Mentor not found at id {}",Id);
            return null;
        }
        Mentor.FirstName = updateMentorRequest.FirstName;
        Mentor.LastName = updateMentorRequest.LastName;
        Mentor.Expertise = updateMentorRequest.Expertise;
        Mentor.Email = updateMentorRequest.Email;
        Mentor.UpdatedDate = updateMentorRequest.UpdatedDate;
        Mentor.Status = updateMentorRequest.Status;

        await _MentorContext.SaveChangesAsync();
        // _logger.LogInformation("Update Success: id {}",Id);
        return new MentorResponse(Mentor);
    }

    public async Task<bool> Delete(int Id)
    {
        var Mentor = _MentorContext.Mentors.FirstOrDefault(t => t.Id == Id);
        if (Mentor == null)
        {
            // _logger.LogInformation("Delete Error:Mentor not Found at id {}",Id);
            return false;
        }
        _MentorContext.Mentors.Remove(Mentor);
        await _MentorContext.SaveChangesAsync();
        // _logger.LogInformation("Delete Success: id{}",Id);
        return true;
    }
}
