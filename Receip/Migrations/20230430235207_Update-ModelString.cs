using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipe.Migrations
{
    public partial class UpdateModelString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipes_RecipeId",
                table: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "Ingredient",
                newName: "RecipesRecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_RecipeId",
                table: "Ingredient",
                newName: "IX_Ingredient_RecipesRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipes_RecipesRecipeId",
                table: "Ingredient",
                column: "RecipesRecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipes_RecipesRecipeId",
                table: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "RecipesRecipeId",
                table: "Ingredient",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_RecipesRecipeId",
                table: "Ingredient",
                newName: "IX_Ingredient_RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipes_RecipeId",
                table: "Ingredient",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }
    }
}
