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

        public DbSet<Ingredient> ingredients { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<RecipeStep>().ToTable("RecipeSteps");

            modelBuilder.Entity<RecipeStep>().HasKey(s => new { s.RecipeId, s.StepNumber });
            modelBuilder.Entity<Ingredient>().HasKey(i => new { i.RecipesRecipeId, i.Id });
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

        public List<Ingredient> Ingredients { get; set; }

        public List<RecipeStep> Steps { get; set; }

        [DataType(DataType.Text)]
        public string Picture { get; set; }
        
    }
}
