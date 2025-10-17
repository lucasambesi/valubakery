namespace ValuBakery.Web.Data
{
    public class CreateRecipeModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Portions { get; set; }

        public string VarianteName { get; set; }

        public bool IsActive { get; set; }
    }
}
