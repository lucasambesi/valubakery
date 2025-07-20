using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;

namespace ValuBakery.Web.Pages.Recipes.Mobile
{
    public partial class RecipeIngredientsMobile
    {
        [Parameter]
        public List<RecipeIngredientDto> Ingredients { get; set; } = new();

        private RecipeIngredientDto? EditingItem;
        private decimal originalQuantity;

        [Parameter]
        public EventCallback OnChanged { get; set; }

        private void StartEditing(RecipeIngredientDto item)
        {
            EditingItem = item;
            originalQuantity = item.Quantity;
        }

        private async Task ConfirmEdit()
        {
            var ingredient = Ingredients.FirstOrDefault(x => x.Id == EditingItem.Id);


            if (ingredient == null) return;

            ingredient.Quantity = EditingItem.Quantity;

            try
            {
                var response = await _recipeIngredientService.UpdateAsync(EditingItem);

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
