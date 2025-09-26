using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebWorker.BLL.Managers.ImageManager;
using WebWorker.BLL.Managers.JwtToken;
using WebWorker.BLL.Services.Account;
using WebWorker.BLL.Services.User;
using WebWorker.BLL.Services.Category;
using WebWorker.BLL.Settings;
using WebWorker.DAL;
using WebWorker.DAL.Initializer;
using WebWorker.DAL.Repositories.User;
using WebWorker.Data.Entities.Identity;
using WebWorker.DAL.Repositories.Category;
using WebWorker.DAL.Repositories.Ingredient;
using WebWorker.BLL.Services.Ingredient;
using WebWorker.DAL.Repositories.Product;
using WebWorker.BLL.Services.Product;

var builder = WebApplication.CreateBuilder(args);

// ?? Додаємо CORS (лише один раз)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // фронтенд
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ?? Реєстрація репозиторіїв
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// ?? Реєстрація сервісів
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
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

var app = builder.Build();

PathSettings.Init(app.Services.GetRequiredService<IWebHostEnvironment>());

// ?? Використовуємо CORS
app.UseCors("AllowFrontend");

app.UseSwagger();
app.UseSwaggerUI();

// ?? Статичні файли
if (!Directory.Exists(PathSettings.ImageDirectory))
    Directory.CreateDirectory(PathSettings.ImageDirectory);

Directory.CreateDirectory(PathSettings.UsersImages);
Directory.CreateDirectory(PathSettings.IngredientsImages);
Directory.CreateDirectory(PathSettings.CategoriesImages);
Directory.CreateDirectory(PathSettings.ProductsImages);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(PathSettings.ImageDirectory),
    RequestPath = $"/images"
});

app.UseAuthorization();

app.MapControllers();

app.Seed(); // Seeder для ролей/користувачів

app.Run();
