using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Helpers;

namespace ValuBakery.Web.Pages.Products.Mobile
{
    public partial class ProductsMobile
    {
        private string search = "";

        private IEnumerable<ProductDto> filteredProducts =>
            string.IsNullOrWhiteSpace(search)
                ? ProductDtos
                : ProductDtos.Where(i =>
                    StringHelper.RemoveDiacritics(i.Name).Contains(StringHelper.RemoveDiacritics(search), StringComparison.OrdinalIgnoreCase));

        private Color GetColor(string unit) => unit?.ToLower() switch
        {
            "kg" => Color.Error,
            "lt" => Color.Info,
            "ud" => Color.Primary,
            _ => Color.Primary
        };
    }
}
