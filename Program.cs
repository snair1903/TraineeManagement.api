using TraineeManagement.api.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TraineeManagement.api.Middleware;
using TraineeManagement.api.Data;
using Microsoft.OpenApi;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new InvalidOperationException("connection string not found");

// Add services to the container.

builder.Services.AddScoped<ITraineeService, TraineeService>();
builder.Services.AddScoped<IMentorService, MentorService>();
builder.Services.AddScoped<ILearningTaskService, LearningTaskService>();
builder.Services.AddScoped<ITaskAssignService, TaskAssignService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IProcessingJobService, ProcessingJobService>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(
    connectionString
));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });
});

var factory = new ConnectionFactory
{
    HostName = builder.Configuration["RabbitMQ:HostName"]!,
    UserName = builder.Configuration["RabbitMQ:UserName"]!,
    Password = builder.Configuration["RabbitMQ:Password"]!
};

var connection = await factory.CreateConnectionAsync();

builder.Services.AddSingleton(connection);
builder.Services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("MyAllowSpecificOrigins",
            builder => builder.WithOrigins("http://localhost:3000", "http://localhost:5173/")
                              .AllowAnyMethod()
                              .AllowAnyHeader());
    });




builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole(); 
    logging.SetMinimumLevel(LogLevel.Trace);
});
builder.Services.AddScoped<JwtService>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionhandler>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "TraineeManagementApp_"; // Prefix for Redis keys
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI(c=>
    //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")
    // );
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyAllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();

app.Run();
