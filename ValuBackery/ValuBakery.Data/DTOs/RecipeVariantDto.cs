using ValuBakery.Data.Entities;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.DTOs
{
    public class RecipeVariantDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Portions { get; set; }

        public decimal Cost { get; set; }

        public int RecipeId { get; set; }

        public RecipeDto Recipe { get; set; }

        public List<RecipeIngredientDto> Ingredients { get; set; }

        public List<RecipeComponentDto> Components { get; set; } = new();

        public List<RecipeComponentDto> UsedIn { get; set; } = new();

        public bool IsDeleted { get; set; }

        public string GetName()
        {
            return Recipe?.Name + " " + Name;
        }

        public decimal GetCost()
        {
            decimal cost = 0;
            cost += Ingredients.Sum(x => x.GetCost());
            cost += Components.Sum(x => x.GetCost());

            return cost;
        }

        public void SetCost()
        {
            decimal cost = 0;
            cost += Ingredients.Sum(x => x.GetCost());
            cost += Components.Sum(x => x.GetCost());

            Cost = cost;
        }

        public string GetIngredients()
        {
            var result = string.Empty;
            var parts = new List<string>();

            // Ingredientes
            if (Ingredients?.Count > 0)
            {
                var ingredientes = Ingredients
                    .Select(i => $"{i.Ingredient.Name} x{i.Quantity.ToString("N0")} {i.GetConvertUnit()}")
                    .ToList();

                parts.Add($"<strong>Ingredientes:</strong> {string.Join(", ", ingredientes)}.");

                result = string.Join("<br>", parts);
            }

            return result;
        } 

        public string GetComponents()
        {
            var result = string.Empty;
            var parts = new List<string>();
            // Recetas
            if (Components?.Count > 0)
            {
                var recetas = Components
                    .Select(c => $"{c.ChildRecipeVariant?.GetName()} x{c.Quantity.ToString("N2")} {UnitEnum.Ud}")
                    .ToList();

                parts.Add($"<strong>Recetas:</strong> {string.Join(", ", recetas)}.");

                result = string.Join("<br>", parts);
            }

            return result;
        }
    }
}
