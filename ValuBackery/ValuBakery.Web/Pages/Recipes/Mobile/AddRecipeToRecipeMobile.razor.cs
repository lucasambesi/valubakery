using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Recipes.Mobile
{
    public partial class AddRecipeToRecipeMobile
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

        private List<RecipeVariantDto> RecipeDtos = new();
        private HashSet<int> SelectedRecipeIds = new();

        protected override async Task OnInitializedAsync()
        {
            var ings = await _recipeVariantService.GetAllAsync();
            RecipeDtos = ings
                .Where(x => !RecipeDto.Components
                    .Select(t => t.ChildRecipeVariantId)
                    .Contains(x.Id))
                .ToList();
        }

        private async Task Submit()
        {
            try
            {
                List<RecipeComponentDto> igs = new();
                foreach (var recipeDto in SelectedRecipeIds)
                {
                    RecipeComponentDto recipeComponentDto = new()
                    {
                        ParentRecipeVariantId = RecipeDto.Id,
                        ChildRecipeVariantId = recipeDto,
                        Quantity = 1
                    };

                    var id = await _recipeComponentService.AddAsync(recipeComponentDto);

                    recipeComponentDto.Id = id;
                    igs.Add(recipeComponentDto);
                }

                Dictionary<int, RecipeComponentType> componentMap = new();

                foreach (var ig in igs)
                {
                    componentMap[ig.Id] = RecipeComponentType.Recipe;
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
                SelectedRecipeIds.Add(id);
            else
                SelectedRecipeIds.Remove(id);
        }
    }
}
