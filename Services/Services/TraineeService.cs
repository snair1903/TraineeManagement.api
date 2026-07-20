using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using NLog.LayoutRenderers;

public class TraineeService : ITraineeService
{
    private bool _redisAvailable = true;
    private readonly AppDbContext _traineeContext;

    private readonly ILogger<TraineeService> _logger;

    private readonly IDistributedCache _cache;

    public TraineeService(AppDbContext context, ILogger<TraineeService> logger, IDistributedCache cache)
    {
        _traineeContext = context;
        _logger = logger;
        _cache = cache;
    }

    public async Task<PagedResponse<TraineeResponse>> GetAll(int pageNumber, int pageSize, string? search, TraineeStatus? userStatus)
    {
        var query = _traineeContext.Trainees.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(t => t.FirstName.ToLower().Contains(search) ||
            t.LastName.ToLower().Contains(search) || t.Email.ToLower().Contains(search) ||
            t.TechStack.ToLower().Contains(search));
        }

        if (userStatus != null)
        {
            query = query.Where(t => t.Status.ToString() == userStatus.ToString());
        }

        var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(t => new TraineeResponse(t)).AsNoTracking().ToListAsync();
        _logger.LogInformation("Get Success");
        return new PagedResponse<TraineeResponse>(data, pageNumber, pageSize, query.Count());
    }


    public async Task<TraineeResponse?> GetById(int Id)
    {
        
        string cacheKey = $"Trainee_{Id}";
        string? cachedData = null;
            try
        {
            cachedData = await _cache.GetStringAsync(cacheKey);
            _redisAvailable = true;
        }
        catch
        {
            _redisAvailable = false;
        }
        // Console.WriteLine("{0}", cachedData);

        if (cachedData != null)
        {
            _logger.LogInformation("Cache hit for product {Id}", Id);
            return JsonSerializer.Deserialize<TraineeResponse>(cachedData)!;
        }

        _logger.LogInformation("Cache miss for Id {Id}", Id);
        var trainee = await _traineeContext.Trainees.FindAsync(Id);
        if (trainee == null)
        {
            _logger.LogInformation("Get Failure: No user found at id{}", Id);
            throw new NotFoundException($"Trainee not found at Id {Id}");
        }
        _logger.LogInformation("Get Success: user found at id {}", Id);
        var res = new TraineeResponse(trainee);
        var serialized = JsonSerializer.Serialize(res);

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };

        
        if(_redisAvailable)
        {
            await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);
        }
        return res;
    }

    public async Task<TraineeResponse> Create(CreateTraineeRequest trainee)
    {

        var nt = new Trainee(trainee);

        _traineeContext.Trainees.Add(nt);
        await _traineeContext.SaveChangesAsync();
        _logger.LogInformation("Create Success: Trainee id {}", nt.Id);
        return new TraineeResponse(nt);
    }

    public async Task<TraineeResponse?> Update(int Id, UpdateTraineeRequest updateTraineeRequest)
    {
        var trainee = await _traineeContext.Trainees.FindAsync(Id);
        if (trainee == null)
        {
            _logger.LogInformation("Update Fail: Trainee not found at id {}", Id);
            throw new NotFoundException($"Trainee not found at {Id}");
        }
        trainee.FirstName = updateTraineeRequest.FirstName;
        trainee.LastName = updateTraineeRequest.LastName;
        trainee.TechStack = updateTraineeRequest.TechStack;
        trainee.Email = updateTraineeRequest.Email;
        trainee.Status = updateTraineeRequest.Status;

        await _traineeContext.SaveChangesAsync();
        _logger.LogInformation("Update Success: id {}", Id);
        var serializedData = JsonSerializer.Serialize(new TraineeResponse(trainee));

        // 2. Set expiration parameters
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };
        string cacheKey = $"Trainee_{Id}";

        // 3. Overwrite the key with the new value
        await _cache.SetStringAsync(cacheKey, serializedData, options);
        return new TraineeResponse(trainee);
    }

    public async Task<bool> Delete(int Id)
    {
        var trainee = await _traineeContext.Trainees.FindAsync(Id);
        if (trainee == null)
        {
            _logger.LogInformation("Delete Error:Trainee not Found at id {}", Id);
            throw new NotFoundException($"Trainee not found at {Id}");
        }
        _traineeContext.Trainees.Remove(trainee);
        await _traineeContext.SaveChangesAsync();
        _logger.LogInformation("Delete Success: id{}", Id);
        string cacheKey = $"Trainee_{Id}";
        await _cache.RemoveAsync(cacheKey);
        return true;
    }
}
