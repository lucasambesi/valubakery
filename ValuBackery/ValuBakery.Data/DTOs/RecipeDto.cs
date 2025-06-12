using ValuBakery.Data.Entities;

namespace ValuBakery.Data.DTOs
{
    public class RecipeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Cost { get; set; }

        public List<RecipeIngredientDto> Ingredients { get; set; }

        public List<RecipeComponentDto> Components { get; set; } = new();

        public List<RecipeComponentDto> UsedIn { get; set; } = new();

        public bool IsDeleted { get; set; }
    }
}
