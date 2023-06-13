using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipe.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Recipes")]
        public int RecipesRecipeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}