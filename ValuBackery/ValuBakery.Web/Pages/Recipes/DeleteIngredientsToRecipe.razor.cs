using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class DeleteIngredientsToRecipe
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public RecipeDto RecipeDto { get; set; }

        [Parameter]
        public EventCallback<List<int>> OnDeleteData
        {
            get; set;
        }

        private List<RecipeIngredientDto> IngredientDtos = new();

        private HashSet<RecipeIngredientDto> SelectedIngredientDtos = new();

        protected override async Task OnInitializedAsync()
        {
            IngredientDtos = await _recipeIngredientService.GetByRecipeIdAsync(RecipeDto.Id);
        }

        private void OnSelectedItemsChanged(HashSet<RecipeIngredientDto> elements)
        {
            SelectedIngredientDtos = elements;
        }

        private async Task Submit()
        {
            try
            {
                List<RecipeIngredientDto> igs = new();
                foreach (var ingredientDto in SelectedIngredientDtos)
                {
                    await _recipeIngredientService.DeleteAsync(ingredientDto.Id);

                    igs.Add(ingredientDto);
                }

                await OnDeleteData.InvokeAsync(igs.Select(x => x.Id).ToList());
            }
            catch
            {
                throw;
            }
            finally
            {
                MudDialog.Close();
                StateHasChanged();
            }
        }
    }
}
