using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Web.Data;
using ValuBakery.Web.Pages.Ingredients;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class AddIngredientsToRecipe
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public RecipeVariantDto RecipeDto { get; set; }

        [Parameter]
        public EventCallback<Dictionary<int, RecipeComponentType>> OnCreateData
        {
            get; set;
        }

        private List<IngredientDto> IngredientDtos = new();
        
        private HashSet<IngredientDto> SelectedIngredientDtos = new();

        private List<RecipeVariantDto> RecipeDtos = new();

        private HashSet<RecipeVariantDto> SelectedRecipeDtos = new();

        protected override async Task OnInitializedAsync()
        {
            var ings = await _ingredientService.GetAllAsync();
            IngredientDtos = ings.Where(x => !RecipeDto.Ingredients.Select(t => t.IngredientId).Contains(x.Id)).ToList();

            var rcps = await _recipeVariantService.GetAllExpandedAsync();
            RecipeDtos = rcps.Where(x => x.Id != RecipeDto.Id &&
                !RecipeDto.Components.Select(t => t.ChildRecipeVariantId).Contains(x.Id) &&
                !RecipeDto.UsedIn.Select(t => t.ParentRecipeVariantId).Contains(x.Id)).ToList();
        }

        private void OnSelectedItemsChanged(HashSet<IngredientDto> elements)
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
                List<RecipeIngredientDto> igs = new();
                foreach (var ingredientDto in SelectedIngredientDtos)
                {
                    RecipeIngredientDto recipeIngredientDto = new()
                    {
                        RecipeVariantId = RecipeDto.Id,
                        IngredientId = ingredientDto.Id,
                        Quantity = 0
                    };

                    var id = await _recipeIngredientService.AddAsync(recipeIngredientDto);

                    recipeIngredientDto.Id = id;
                    igs.Add(recipeIngredientDto);
                }

                List<RecipeComponentDto> rcps = new();
                foreach (var recipeDto in SelectedRecipeDtos)
                {
                    RecipeComponentDto recipeComponentDto = new()
                    {
                        ParentRecipeVariantId = RecipeDto.Id,
                        ChildRecipeVariantId = recipeDto.Id,
                        Quantity = 1
                    };

                    var id = await _recipeComponentService.AddAsync(recipeComponentDto);

                    recipeComponentDto.Id = id;
                    rcps.Add(recipeComponentDto);
                }
                Dictionary<int, RecipeComponentType> componentMap = new();

                foreach (var ig in igs)
                {
                    componentMap[ig.Id] = RecipeComponentType.Ingredient;
                }

                foreach (var rc in rcps)
                {
                    componentMap[rc.Id] = RecipeComponentType.Recipe;
                }


                await OnCreateData.InvokeAsync(componentMap);
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
