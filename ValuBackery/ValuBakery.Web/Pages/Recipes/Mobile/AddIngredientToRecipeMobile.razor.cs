using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Recipes.Mobile
{
    public partial class AddIngredientToRecipeMobile
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
        private HashSet<int> SelectedIngredientIds = new();

        protected override async Task OnInitializedAsync()
        {
            var ings = await _ingredientService.GetAllAsync();
            IngredientDtos = ings
                .Where(x => !RecipeDto.Ingredients
                    .Select(t => t.IngredientId)
                    .Contains(x.Id))
                .ToList();
        }

        private async Task Submit()
        {
            try
            {
                List<RecipeIngredientDto> igs = new();
                foreach (var ingredientDto in SelectedIngredientIds)
                {
                    RecipeIngredientDto recipeIngredientDto = new()
                    {
                        RecipeVariantId = RecipeDto.Id,
                        IngredientId = ingredientDto,
                        Quantity = 0
                    };

                    var id = await _recipeIngredientService.AddAsync(recipeIngredientDto);

                    recipeIngredientDto.Id = id;
                    igs.Add(recipeIngredientDto);
                }

                Dictionary<int, RecipeComponentType> componentMap = new();

                foreach (var ig in igs)
                {
                    componentMap[ig.Id] = RecipeComponentType.Ingredient;
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
        private void OnCheckedChanged(bool value, int id)
        {
            if (value)
                SelectedIngredientIds.Add(id);
            else
                SelectedIngredientIds.Remove(id);
        }
    }
}
