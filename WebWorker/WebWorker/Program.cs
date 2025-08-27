using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebWorker.BLL.Managers.ImageManager;
using WebWorker.BLL.Managers.JwtToken;
using WebWorker.BLL.Services.Account;
using WebWorker.BLL.Services.User;
using WebWorker.BLL.Settings;
using WebWorker.DAL;
using WebWorker.DAL.Initializer;
using WebWorker.DAL.Repositories.User;
using WebWorker.Data.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.

// Register repositories and services
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IImageManager, ImageManager>();

builder.Services.AddScoped<IJwtTokenManager, JwtTokenManager>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresNeon"));
});

builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
       .AllowAnyMethod()
       .AllowCredentials()
       .WithOrigins(builder.Configuration["ClientAppUrl"]!);
});

app.UseSwagger();
app.UseSwaggerUI();

// Static Files
if(!Directory.Exists(PathSettings.ImageDirectory))
    Directory.CreateDirectory(PathSettings.ImageDirectory);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(PathSettings.ImageDirectory),
    RequestPath = $"/images"
});
//

// Configure the HTTP request pipeline.

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Seed(); // Call the Seed method to initialize the database with roles and users

app.Run();
