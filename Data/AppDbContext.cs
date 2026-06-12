using Microsoft.EntityFrameworkCore;

namespace TraineeManagement.api.Data;
using TraineeManagement.api.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }

    public DbSet<Trainee> Trainees {get;set;}
    public DbSet<Mentor> Mentors {get;set;}
    public DbSet<User> Users {get;set;}
}