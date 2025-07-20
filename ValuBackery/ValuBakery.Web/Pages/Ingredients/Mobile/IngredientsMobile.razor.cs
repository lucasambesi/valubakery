using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Helpers;

namespace ValuBakery.Web.Pages.Ingredients.Mobile
{
    public partial class IngredientsMobile
    {
        private string search = "";

        private IEnumerable<IngredientDto> filteredIngredients =>
            string.IsNullOrWhiteSpace(search)
                ? IngredientDtos
                : IngredientDtos.Where(i =>
                    StringHelper.RemoveDiacritics(i.Name).Contains(StringHelper.RemoveDiacritics(search), StringComparison.OrdinalIgnoreCase));

        private Color GetColor(string unit) => unit?.ToLower() switch
        {
            "kg" => Color.Error,
            "lt" => Color.Info,
            "ud" => Color.Primary,
            _ => Color.Default
        };
    }
}
