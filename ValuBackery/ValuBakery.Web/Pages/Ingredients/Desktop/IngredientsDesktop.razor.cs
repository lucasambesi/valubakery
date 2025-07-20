using MudBlazor;
using System.Text;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Helpers;

namespace ValuBakery.Web.Pages.Ingredients.Desktop
{
    public partial class IngredientsDesktop
    {
        protected void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        protected void BackupItem(object IngredientDto)
        {
            IngredientDtoBeforeEdit = new()
            {
                Id = ((IngredientDto)IngredientDto).Id,
                Name = ((IngredientDto)IngredientDto).Name,
                Unit = ((IngredientDto)IngredientDto).Unit,
                CostPerUnit = ((IngredientDto)IngredientDto).CostPerUnit,
                IsDeleted = ((IngredientDto)IngredientDto).IsDeleted
            };
        }

        protected async void ItemHasBeenCommitted(object ingredientDto)
        {
            var dto = (IngredientDto)ingredientDto;

            try
            {
                await _ingredientService.UpdateAsync(dto);
                AddEditionEvent($"Fila editada: Cambios en {dto.Name} guardados");
            }
            catch (Exception ex)
            {
                Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
            }
        }

        protected void ResetItemToOriginalValues(object IngredientDto)
        {
            ((IngredientDto)IngredientDto).Id = IngredientDtoBeforeEdit.Id;
            ((IngredientDto)IngredientDto).Name = IngredientDtoBeforeEdit.Name;
            ((IngredientDto)IngredientDto).Unit = IngredientDtoBeforeEdit.Unit;
            ((IngredientDto)IngredientDto).CostPerUnit = IngredientDtoBeforeEdit.CostPerUnit;
            ((IngredientDto)IngredientDto).IsDeleted = IngredientDtoBeforeEdit.IsDeleted;
        }

        protected bool FilterFunc(IngredientDto ingredientDto)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            var normalizedSearch = StringHelper.RemoveDiacritics(searchString).ToLowerInvariant();
            var normalizedName = StringHelper.RemoveDiacritics(ingredientDto.Name).ToLowerInvariant();

            return normalizedName.Contains(normalizedSearch);
        }
    }
}
