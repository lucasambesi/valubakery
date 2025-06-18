namespace ValuBakery.Data.DTOs
{
    public class RecipeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public List<RecipeVariantDto> Variants { get; set; } = new();
    }
}
