using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Recipes.Mobile
{
    public partial class DeleteRecipeToRecipeMobile
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

        private List<RecipeVariantDto> RecipeDtos = new();
        private HashSet<int> SelectedRecipeIds = new();

        protected override async Task OnInitializedAsync()
        {
            var recipes = await _recipeVariantService.GetAllAsync();

            RecipeDtos = recipes
                .Where(x => RecipeDto.Components
                    .Select(t => t.ChildRecipeVariantId)
                    .Contains(x.Id))
                .ToList();
        }

        private async Task Submit()
        {
            try
            {
                Dictionary<int, RecipeComponentType> componentMap = new();

                foreach (var ig in SelectedRecipeIds)
                {
                    componentMap[ig] = RecipeComponentType.Recipe;
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
                SelectedRecipeIds.Add(id);
            else
                SelectedRecipeIds.Remove(id);
        }
    }
}
