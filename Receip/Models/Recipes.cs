using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Recipe.Models
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options) { }

        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeStep>().HasKey(s => new { s.RecipeId, s.StepNumber });
        }
    }

    public class Recipes
    {
        [Key]
        [Required]
        [DataType(DataType.Text)]
        public int RecipeId { get; set;}
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public List<Ingredient> Ingredients { get; set; }
        [Required]
        public List<RecipeStep> Steps { get; set; }
        [DataType(DataType.Upload)]
        public byte[]? Picture { get; set; }
        
    }
}
