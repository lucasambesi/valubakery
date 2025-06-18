using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Pages.Ingredients;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class Recipes
    {
        private List<string> editEvents = new();
        private string searchString = "";
        private RecipeDto selectedItem = null;
        private RecipeDto RecipeDtoBeforeEdit;
        private HashSet<RecipeDto> selectedItems = new HashSet<RecipeDto>();
        private List<RecipeDto> RecipeDtos = new List<RecipeDto>();

        protected override async Task OnInitializedAsync()
        {
            RecipeDtos = await _recipeService.GetAllAsync();
        }

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

        private bool FilterFunc(RecipeDto RecipeDto)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (RecipeDto.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        #region Dialog
        private void DialogCreate()
        {
            var parameters = new DialogParameters
            {
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<CreateRecipe>("CreateRecipe", parameters, options);
        }

        private void ViewRecipe(int id)
        {
            var parameters = new DialogParameters
            {
                { nameof(Recipe.Id), id }
            };

            var options = new DialogOptions
            {
                FullWidth = true,
                MaxWidth = MaxWidth.Medium,
                DisableBackdropClick = true,
                Position = DialogPosition.Center,
                NoHeader = false,
                CloseButton = true
            };

            _dialogService.Show<Recipe>("", parameters, options);
        }
        #endregion
    }
}
