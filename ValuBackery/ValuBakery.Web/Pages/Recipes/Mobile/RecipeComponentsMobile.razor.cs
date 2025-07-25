using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;

namespace ValuBakery.Web.Pages.Recipes.Mobile
{
    public partial class RecipeComponentsMobile
    {
        [Parameter]
        public List<RecipeComponentDto> Components { get; set; } = new();

        private RecipeComponentDto? EditingItem;
        private decimal originalQuantity;

        [Parameter]
        public EventCallback OnChanged { get; set; }

        private void StartEditing(RecipeComponentDto item)
        {
            EditingItem = item;
            originalQuantity = item.Quantity;
        }

        private async Task ConfirmEdit()
        {
            var component = Components.FirstOrDefault(x => x.Id == EditingItem.Id);


            if (component == null) return;

            component.Quantity = EditingItem.Quantity;

            try
            {
                var response = await _recipeComponentService.UpdateAsync(EditingItem);

            }
            catch (Exception ex)
            {
                Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
            }

            EditingItem = null;

            await OnChanged.InvokeAsync();
        }

        private void CancelEdit()
        {
            if (EditingItem != null)
            {
                EditingItem.Quantity = originalQuantity;
                EditingItem = null;
            }
        }

        private string GetUnitAbbr(UnitEnum unit) =>
            unit switch
            {
                UnitEnum.Kg => "Kg",
                UnitEnum.Grs => "g",
                UnitEnum.Lt => "L",
                UnitEnum.Mls => "ml",
                UnitEnum.Ud => "u",
                _ => ""
            };
    }
}
