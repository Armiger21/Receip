using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipe.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Recipe.Controllers
{
    public class RecipesController : Controller
    {
        private readonly RecipeContext _context;

        public RecipesController(RecipeContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            return _context.Recipes != null ?
                        View(await _context.Recipes.ToListAsync()) :
                        Problem("Entity set 'RecipeContext.Recipes'  is null.");
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }


            var recipe = await _context.Recipes.Include(s => s.Steps).Include(i => i.Ingredients)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,Title,Description,Picture")] Recipes recipe, IFormFile Picture, string Ingredients, string Steps)
        {
            recipe.Picture = Picture.FileName;
            await UploadFile(Picture);


            if (recipe.Picture != null && recipe.Title != null && recipe.Description != null)
            {
                string i = Ingredients;
                List<Ingredient> ing = new List<Ingredient>();
                Ingredient ingredient = new Ingredient { Name = i };
                ing.Add(ingredient);
                recipe.Ingredients = ing;

                string s = Steps;
                List<RecipeStep> spt = new List<RecipeStep>();
                RecipeStep recipeStep = new RecipeStep { Step = s, StepNumber = 1 };
                spt.Add(recipeStep);
                recipe.Steps = spt;

                _context.Add(recipe);
                await _context.SaveChangesAsync();


                //How do I capture a List of Objects in a View

                //_context.ingredients.Add(recipe.Ingredient);
                //_context.RecipeSteps.Add(recipe.RecipeSteps);

                //add custom code to save ingriedents and steps
                //Get the RecipeID recipe.RecipeID
                //Insert the ingredients into the database INSERT INTO Ingredients [Name], [RecipesRecipeId] VALUES (recipe.Ingredients, recipe.RecipeID
                ////string[] ingredientsList = Ingredients.Split(',');

                ////foreach (string ingredient in ingredientsList)
                ////{
                ////    string insertQuery = $"";
                ////    await _context.Database.ExecuteSqlRawAsync(insertQuery);
                ////}
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeId,Title,Description,Picture")] Recipes recipe)
        {
            if (id != recipe.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'RecipeContext.Recipes'  is null.");
            }
            var recipe = await _context.Recipes.
                FindAsync(id);
            List<Ingredient> ingredients = await _context.ingredients.Where(i => i.RecipesRecipeId == id).ToListAsync();
            recipe.Ingredients = ingredients;
            List<RecipeStep> steps = await _context.RecipeSteps.Where(s => s.RecipeId == id).ToListAsync();
            recipe.Steps = steps;

            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipes?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new FileNotFoundException("The file was null.");
            }
            else
            {
                string fileName = file.FileName;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok("File Upload Complete");
        }
    }
}
