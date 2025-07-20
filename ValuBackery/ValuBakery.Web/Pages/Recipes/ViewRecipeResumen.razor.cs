using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class ViewRecipeResumen
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter] public int Id { get; set; }

        [Parameter] public RecipeVariantDto? RecipeDto { get; set; }

        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            if (RecipeDto == null)
            {
                isLoading = true;
                RecipeDto = await _recipeVariantService.GetByIdAsync(Id);
                isLoading = false;
            }
        }

        private void Close() => MudDialog?.Cancel();
    }
}
