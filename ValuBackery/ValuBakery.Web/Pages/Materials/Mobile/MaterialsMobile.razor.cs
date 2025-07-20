using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Helpers;

namespace ValuBakery.Web.Pages.Materials.Mobile
{
    public partial class MaterialsMobile
    {
        private string search = "";

        private IEnumerable<MaterialDto> filteredMaterials =>
            string.IsNullOrWhiteSpace(search)
                ? MaterialDtos
                : MaterialDtos.Where(i =>
                    StringHelper.RemoveDiacritics(i.Name).Contains(StringHelper.RemoveDiacritics(search), StringComparison.OrdinalIgnoreCase));

        private Color GetColor(string unit) => unit?.ToLower() switch
        {
            "kg" => Color.Error,
            "meter" => Color.Error,
            "lt" => Color.Info,
            "box" => Color.Info,
            "ud" => Color.Primary,
            "roll" => Color.Primary,
            _ => Color.Default
        };
    }
}
