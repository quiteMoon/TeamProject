using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using WebWorker.Data.Entities.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebWorker.DAL.Settings;

namespace WebWorker.DAL.Initializer
{
    public static class Seeder
    {
        public static async void Seed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.MigrateAsync();

            if (!await roleManager.RoleExistsAsync(RoleSettings.Admin))
            {
                var adminRole = new RoleEntity { Name = RoleSettings.Admin };

                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync(RoleSettings.User))
            {
                var userRole = new RoleEntity { Name = RoleSettings.User };

                await roleManager.CreateAsync(userRole);
            }

            if (!await dbContext.Users.AnyAsync())
            {
                var adminUser = new UserEntity
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Vlad",
                    LastName = "Kidun",
                };

                var adminResult = await userManager.CreateAsync(adminUser, "123456");

                if (adminResult.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, RoleSettings.Admin);
                else
                    throw new Exception($"Failed to create admin user: {string.Join(", ", adminResult.Errors.Select(e => e.Description))}");

                var user = new UserEntity
                {
                    UserName = "user@gmail.com",
                    Email = "user.gmail.com",
                    FirstName = "User",
                    LastName = "User",
                };

                var userResult = await userManager.CreateAsync(user, "123456");

                if (userResult.Succeeded)
                    await userManager.AddToRoleAsync(user, RoleSettings.User);
                else
                    throw new Exception($"Failed to create user: {string.Join(", ", userResult.Errors.Select(e => e.Description))}");
            }
        }
    }
}
