using Microsoft.AspNetCore.Components;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Recipes.Mobile
{
    public partial class RecipeMobile
    {
        [Parameter]
        public int Id { get; set; }

        public RecipeDto? RecipeDto { get; set; }

        [Parameter]
        public RecipeVariantDto RecipeVariantDto { get; set; }

        [Parameter]
        public EventCallback<RecipeDto> OnEditData { get; set; }

        private Guid dialogRenderKey = Guid.NewGuid();

        private string ingredientsText;
        private string componentsText;

        private bool infoInitialExpanded = true;
        private bool ingredientesInitialExpanded = false;
        private bool recipesInitialExpanded = false;

        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            if (Id != default)
            {
                isLoading = true;

                RecipeDto = await _recipeService.GetByIdAsync(Id);

                if (RecipeDto != null)
                {
                    RecipeVariantDto = await _recipeVariantService.GetByIdAsync(RecipeDto.Variants.First().Id);
                }

                isLoading = false;
            }
        }

        protected override void OnParametersSet()
        {
            ingredientsText = RecipeVariantDto?.GetIngredients();
            componentsText = RecipeVariantDto?.GetComponents();
            RecipeVariantDto?.SetCost();
        }

        private string Truncate(string? text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Length > maxLength
                ? text.Substring(0, maxLength) + "..."
                : text;
        }

        private async Task OnIngredientsChanged()
        {
            if (RecipeVariantDto != null)
            {
                RecipeVariantDto.SetCost();
                ingredientsText = RecipeVariantDto?.GetIngredients();
                componentsText = RecipeVariantDto?.GetComponents();

                dialogRenderKey = Guid.NewGuid();
                ingredientesInitialExpanded = true;

                StateHasChanged();
            }
        }
    }
}
