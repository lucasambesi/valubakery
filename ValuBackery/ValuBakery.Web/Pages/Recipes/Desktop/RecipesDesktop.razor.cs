using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Helpers;

namespace ValuBakery.Web.Pages.Recipes.Desktop
{
    public partial class RecipesDesktop
    {
        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        private void BackupItem(object RecipeDto)
        {
            RecipeDtoBeforeEdit = new()
            {
                Id = ((RecipeDto)RecipeDto).Id,
                Name = ((RecipeDto)RecipeDto).Name,
                IsDeleted = ((RecipeDto)RecipeDto).IsDeleted
            };
        }

        private async void ItemHasBeenCommitted(object RecipeDto)
        {
            var dto = (RecipeDto)RecipeDto;

            try
            {
                await _recipeService.UpdateAsync(dto);
                AddEditionEvent($"Fila editada: Cambios en {dto.Name} guardados");
            }
            catch (Exception ex)
            {
                Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
            }
        }

        private void ResetItemToOriginalValues(object RecipeDto)
        {
            ((RecipeDto)RecipeDto).Id = RecipeDtoBeforeEdit.Id;
            ((RecipeDto)RecipeDto).Name = RecipeDtoBeforeEdit.Name;
            ((RecipeDto)RecipeDto).IsDeleted = RecipeDtoBeforeEdit.IsDeleted;
        }

        protected bool FilterFunc(RecipeDto recipeDto)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            var normalizedSearch = StringHelper.RemoveDiacritics(searchString).ToLowerInvariant();
            var normalizedName = StringHelper.RemoveDiacritics(recipeDto.Name).ToLowerInvariant();

            return normalizedName.Contains(normalizedSearch);
        }
    }
}
