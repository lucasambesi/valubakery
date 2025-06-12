using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Pages.Ingredients;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class AddIngredientsToRecipe
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public RecipeDto RecipeDto { get; set; }

        [Parameter]
        public EventCallback<List<int>> OnCreateData
        {
            get; set;
        }

        private List<IngredientDto> IngredientDtos = new();
        
        private HashSet<IngredientDto> SelectedIngredientDtos = new();

        protected override async Task OnInitializedAsync()
        {
            var ings = await _ingredientService.GetAllAsync();
            IngredientDtos = ings.Where(x => !RecipeDto.Ingredients.Select(t => t.IngredientId).Contains(x.Id)).ToList();
        }

        private void OnSelectedItemsChanged(HashSet<IngredientDto> elements)
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
                    RecipeIngredientDto recipeIngredientDto = new()
                    {
                        RecipeId = RecipeDto.Id,
                        IngredientId = ingredientDto.Id,
                        Quantity = 0
                    };

                    var id = await _recipeIngredientService.AddAsync(recipeIngredientDto);

                    recipeIngredientDto.Id = id;
                    igs.Add(recipeIngredientDto);
                }

                await OnCreateData.InvokeAsync(igs.Select(x => x.Id).ToList());
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
