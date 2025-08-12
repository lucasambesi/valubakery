using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Recipes.Mobile
{
    public partial class DeleteIngredientToRecipeMobile
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

        private List<IngredientDto> IngredientDtos = new();
        private HashSet<int> SelectedIngredientIds = new();
        protected bool isLoading;
        protected override async Task OnInitializedAsync()
        {
            isLoading= true;
            var ings = await _ingredientService.GetAllAsync();
            IngredientDtos = ings
                .Where(x => RecipeDto.Ingredients
                    .Select(t => t.IngredientId)
                    .Contains(x.Id))
                .ToList();
            isLoading = false;
        }

        private async Task Submit()
        {
            try
            {
                Dictionary<int, RecipeComponentType> componentMap = new();

                foreach (var ig in SelectedIngredientIds)
                {
                    componentMap[ig] = RecipeComponentType.Ingredient;
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

        private void OnCheckedChanged(bool value, int id)
        {
            if (value)
                SelectedIngredientIds.Add(id);
            else
                SelectedIngredientIds.Remove(id);
        }
    }
}
