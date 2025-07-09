using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class DeleteIngredientsToRecipe
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public RecipeVariantDto RecipeDto { get; set; }

        [Parameter]
        public EventCallback<Dictionary<int, RecipeComponentType>> OnDeleteData
        {
            get; set;
        }

        private List<RecipeIngredientDto> IngredientDtos = new();

        private HashSet<RecipeIngredientDto> SelectedIngredientDtos = new();

        private List<RecipeVariantDto> RecipeDtos = new();

        private HashSet<RecipeVariantDto> SelectedRecipeDtos = new();

        protected override async Task OnInitializedAsync()
        {
            IngredientDtos = await _recipeIngredientService.GetByRecipeIdAsync(RecipeDto.Id);

            var rcps = await _recipeVariantService.GetAllAsync();
            RecipeDtos = rcps.Where(x => x.Id != RecipeDto.Id &&
                RecipeDto.Components.Select(t => t.ChildRecipeVariantId).Contains(x.Id)).ToList();
        }

        private void OnSelectedItemsChanged(HashSet<RecipeIngredientDto> elements)
        {
            SelectedIngredientDtos = elements;
        }

        private void OnSelectedItemsChanged(HashSet<RecipeVariantDto> elements)
        {
            SelectedRecipeDtos = elements;
        }

        private async Task Submit()
        {
            try
            {
                List<int> deletedIds = new();
                Dictionary<int, RecipeComponentType> componentMap = new();

                foreach (var ingredientDto in SelectedIngredientDtos)
                {
                    deletedIds.Add(ingredientDto.Id);
                    componentMap[ingredientDto.Id] = RecipeComponentType.Ingredient;
                }

                foreach (var recipeDto in SelectedRecipeDtos)
                {
                    componentMap[recipeDto.Id] = RecipeComponentType.Recipe;
                    deletedIds.Add(recipeDto.Id);
                }

                await OnDeleteData.InvokeAsync(componentMap);
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
