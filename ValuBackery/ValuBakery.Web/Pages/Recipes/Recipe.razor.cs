using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;

using ValuBakery.Web.Pages.Ingredients;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class Recipe
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public RecipeVariantDto? RecipeVariantDto { get; set; }

        [Parameter]
        public EventCallback<RecipeDto> OnEditData { get; set; }

        public RecipeDto? RecipeDto { get; set; }

        private bool showFullIngredients = false;
        private bool showFullComponents = false;
        private string ingredientsText { get; set; }
        private int maxLengthIngredients = 120;
        private string componentsText { get; set; }
        private int maxLengthComponents = 50;
        private Guid dialogRenderKey = Guid.NewGuid();

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

        protected override async Task OnParametersSetAsync()
        {
            ingredientsText = RecipeVariantDto?.GetIngredients();
            componentsText = RecipeVariantDto?.GetComponents();
            RecipeVariantDto?.SetCost();
        }

        private void ToggleIngredients()
        {
            showFullIngredients = !showFullIngredients;

            dialogRenderKey = Guid.NewGuid();
            StateHasChanged();
        }

        private void ToggleComponents()
        {
            showFullComponents = !showFullComponents;

            dialogRenderKey = Guid.NewGuid();
            StateHasChanged(); 
        }

        private async Task OnIngredientsChanged()
        {
            if (RecipeVariantDto != null)
            {
                RecipeVariantDto.SetCost();
                ingredientsText = RecipeVariantDto?.GetIngredients();
                componentsText = RecipeVariantDto?.GetComponents();

                dialogRenderKey = Guid.NewGuid();

                StateHasChanged();
            }
        }

        protected async void ChangeVariant(RecipeVariantDto variantDto)
        {
            isLoading = true;

            RecipeVariantDto = await _recipeVariantService.GetByIdAsync(variantDto.Id);

            if (RecipeVariantDto != null)
            {
                ingredientsText = RecipeVariantDto.GetIngredients();
                componentsText = RecipeVariantDto.GetComponents();
                showFullIngredients = showFullComponents = false;
                RecipeVariantDto.SetCost();

                dialogRenderKey = Guid.NewGuid(); // fuerza redibujado del diálogo
                await InvokeAsync(StateHasChanged); // asegura el render completo
            }

            isLoading = false;
        }

        protected async void ChangeRecipe(RecipeDto recipeDto)
        {
            isLoading = true;

            RecipeDto = await _recipeService.GetByIdAsync(recipeDto.Id);

            if (RecipeDto != null)
            {
                ChangeVariant(RecipeVariantDto);

                dialogRenderKey = Guid.NewGuid(); // fuerza redibujado del diálogo
                await InvokeAsync(StateHasChanged); // asegura el render completo
            }

            isLoading = false;
        }

        protected void DialogCreate()
        {
            var parameters = new DialogParameters
            {
                { nameof(CreateVariantRecipe.OnCreateData), EventCallback.Factory.Create<int>(this, EditDialogEvent) },
                { nameof(CreateVariantRecipe.RecipeDto), RecipeDto }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<CreateVariantRecipe>("CreateVariantRecipe", parameters, options);
        }

        protected void DialogEdit()
        {
            var parameters = new DialogParameters
            {
                { nameof(EditRecipe.OnCreateData), EventCallback.Factory.Create<RecipeDto>(this, EditDialogEvent) },
                { nameof(EditRecipe.RecipeDto), RecipeDto }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<EditRecipe>("EditRecipe", parameters, options);
        }

        protected async Task EditDialogEvent(int id)
        {
            try
            {
                isLoading = true;

                var entity = await _recipeVariantService.GetByIdAsync(id);

                if (entity != null)
                {
                    var item = entity;

                    if (item != null)
                    {
                        RecipeDto.Variants.Add(item);
                    }

                    StateHasChanged();
                }
            }
            catch
            {
                throw;
            }
            finally { isLoading = false; }
        }

        private string Truncate(string? text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Length > maxLength
                ? text.Substring(0, maxLength) + "..."
                : text;
        }

        protected async Task EditDialogEvent(RecipeDto recipeDto)
        {
            try
            {
                ChangeRecipe(recipeDto);
                await OnEditData.InvokeAsync(recipeDto);
            }
            catch
            {
                throw;
            }
        }
    }
}
