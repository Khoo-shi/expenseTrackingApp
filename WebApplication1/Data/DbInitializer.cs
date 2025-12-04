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

        public static void SeedMockData(ApplicationDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Groceries", Description = "Grocery shopping" },
                    new Category { Name = "Entertainment", Description = "Movies, games, etc." },
                    new Category { Name = "Health", Description = "Medical and fitness" },
                    new Category { Name = "Education", Description = "Books and courses" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Expenses.Any())
            {
                var expenses = new[]
                {
                    new Expense { Title = "Weekly Groceries", Amount = 85.00m, Date = DateTime.UtcNow.AddDays(-7), CategoryId = context.Categories.First(c => c.Name == "Groceries").Id },
                    new Expense { Title = "Movie Night", Amount = 15.00m, Date = DateTime.UtcNow.AddDays(-3), CategoryId = context.Categories.First(c => c.Name == "Entertainment").Id },
                    new Expense { Title = "Gym Membership", Amount = 45.00m, Date = DateTime.UtcNow.AddMonths(-1), CategoryId = context.Categories.First(c => c.Name == "Health").Id },
                    new Expense { Title = "Online Course", Amount = 120.00m, Date = DateTime.UtcNow.AddMonths(-2), CategoryId = context.Categories.First(c => c.Name == "Education").Id },

                    // Edge cases
                    new Expense { Title = "", Amount = 0.00m, Date = DateTime.UtcNow, CategoryId = context.Categories.First(c => c.Name == "Groceries").Id }, // Empty title
                    new Expense { Title = "Invalid Amount", Amount = -50.00m, Date = DateTime.UtcNow, CategoryId = context.Categories.First(c => c.Name == "Health").Id } // Negative amount
                };
                context.Expenses.AddRange(expenses);
                context.SaveChanges();
            }
        }
    }
}
