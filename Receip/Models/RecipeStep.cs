using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipe.Models
{
    public class RecipeStep
    {
        [Key]
        [Column(Order=1)]
        [Required]
        public int RecipeId { get; set; }
        [Key]
        [Column(Order=2)]
        [Required]
        public int StepNumber { get; set; }
        public string Step { get; set; }
    }
}
