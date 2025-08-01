namespace ValuBakery.Web.Data
{
    public class EditProductModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public decimal ApplyProfitToRecipes { get; set; }

        public decimal ApplyProfitToMaterials { get; set; }
    }
}
