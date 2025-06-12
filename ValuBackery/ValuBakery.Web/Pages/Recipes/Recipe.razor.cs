using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Web.Pages.Ingredients;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class Recipe
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public RecipeDto RecipeDto { get; set; } = new RecipeDto();

        private RecipeIngredientDto selectedItem = null;
        private RecipeIngredientDto RecipeIngredientDtoBeforeEdit;
        private string searchString = "";
        private List<string> editEvents = new();

        protected override async Task OnInitializedAsync()
        {
            if (Id != default)
            {
                RecipeDto = await _recipeService.GetByIdAsync(Id);
            }
        }

        #region Ingredients table
        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        private void BackupItem(object RecipeIngredientDto)
        {
            RecipeIngredientDtoBeforeEdit = new()
            {
                Id = ((RecipeIngredientDto)RecipeIngredientDto).Id,
                Quantity = ((RecipeIngredientDto)RecipeIngredientDto).Quantity,
                RecipeId = ((RecipeIngredientDto)RecipeIngredientDto).RecipeId,
                IngredientId = ((RecipeIngredientDto)RecipeIngredientDto).IngredientId,
            };
        }

        private async void ItemHasBeenCommitted(object recipeIngredientDto)
        {
            var dto = (RecipeIngredientDto)recipeIngredientDto;

            try
            {
                await _recipeIngredientService.UpdateAsync(dto);
                //AddEditionEvent($"Fila editada: Cambios en {dto.Ingredient.Name} guardados");
            }
            catch (Exception ex)
            {
                Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
            }
        }


        private void ResetItemToOriginalValues(object RecipeIngredientDto)
        {
            ((RecipeIngredientDto)RecipeIngredientDto).Id = RecipeIngredientDtoBeforeEdit.Id;
            ((RecipeIngredientDto)RecipeIngredientDto).Quantity = RecipeIngredientDtoBeforeEdit.Quantity;
            ((RecipeIngredientDto)RecipeIngredientDto).RecipeId = RecipeIngredientDtoBeforeEdit.RecipeId;
            ((RecipeIngredientDto)RecipeIngredientDto).IngredientId = RecipeIngredientDtoBeforeEdit.IngredientId;
        }

        private bool FilterFunc(RecipeIngredientDto RecipeIngredientDto)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (RecipeIngredientDto.Ingredient.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion

        #region Dialog
        private void DialogAdd()
        {
            var parameters = new DialogParameters
            {
                { nameof(AddIngredientsToRecipe.RecipeDto), RecipeDto },
                { nameof(AddIngredientsToRecipe.OnCreateData), 
                    EventCallback.Factory.Create<List<int>>(this, DialogAddEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = false,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<AddIngredientsToRecipe>("AddIngredientsToRecipe", parameters, options);
        }
        private void DialogDelete()
        {
            var parameters = new DialogParameters
            {
                { nameof(DeleteIngredientsToRecipe.RecipeDto), RecipeDto },
                { nameof(DeleteIngredientsToRecipe.OnDeleteData),
                    EventCallback.Factory.Create<List<int>>(this, DialogDeleteEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = false,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<DeleteIngredientsToRecipe>("DeleteIngredientsToRecipe", parameters, options);
        }

        public async Task DialogAddEvent(List<int> ids)
        {
            try
            {
                List<RecipeIngredientDto> ingredientDtos = new();

                foreach (var item in ids)
                {
                    var newIngredient = await _recipeIngredientService.GetByIdAsync(item);

                    if (newIngredient != null)
                    {
                        ingredientDtos.Add(newIngredient);
                    }
                }

                RecipeDto.Ingredients.AddRange(ingredientDtos);
                StateHasChanged();
            }
            catch
            {
                throw;
            }
        }

        public async Task DialogDeleteEvent(List<int> ids)
        {
            try
            {
                List<RecipeIngredientDto> ingredientDtos = new();

                foreach (var item in ids)
                {
                    var newIngredient = RecipeDto.Ingredients.FirstOrDefault(x => x.Id == item);

                    if (newIngredient != null)
                    {
                        RecipeDto.Ingredients.Remove(newIngredient);
                    }
                }

                StateHasChanged();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
