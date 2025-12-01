using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // FK Relationship
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
