using ValuBakery.Data.Entities.Common;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.DTOs
{
    public class RecipeDto : AuditableBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public List<RecipeVariantDto> Variants { get; set; } = new();

        public string GetVariants()
        {
            var result = string.Empty;
            var parts = new List<string>();
            // Recetas
            if (Variants?.Count > 0)
            {
                var recetas = Variants
                    .Select(c => c.Name)
                    .ToList();

                parts.Add($"{string.Join(", ", recetas)}.");

                result = string.Join("", parts);
            }

            return result;
        }
    }
}
