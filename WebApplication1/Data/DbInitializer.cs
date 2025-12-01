using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure database is created (migrations are used elsewhere)
            context.Database.EnsureCreated();

            if (context.Categories.Any()) return; // already seeded

            var categories = new[]
            {
                new Category { Name = "Food", Description = "Food & Dining" },
                new Category { Name = "Transport", Description = "Transport & Travel" },
                new Category { Name = "Utilities", Description = "Bills & Utilities" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            var expenses = new[]
            {
                new Expense { Title = "Lunch", Amount = 12.50m, Date = DateTime.UtcNow.Date, CategoryId = categories[0].Id },
                new Expense { Title = "Bus Ticket", Amount = 2.75m, Date = DateTime.UtcNow.Date, CategoryId = categories[1].Id }
            };
            context.Expenses.AddRange(expenses);
            context.SaveChanges();
        }

        public static async Task InitializeIdentityAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create roles if they don't exist
            string[] roles = { "User", "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create default user if it doesn't exist
            string email = "dario@gc.ca";
            string password = "Test123$";

            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "User");
                }
            }
        }
    }
}
