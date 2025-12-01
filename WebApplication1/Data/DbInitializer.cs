using System;
using System.Linq;
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
    }
}
