using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Data.Enums;
using ValuBakery.Web.Data;

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

        private async Task OnChanged()
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

        private void DialogAdd()
        {
            var parameters = new DialogParameters
            {
                { nameof(AddIngredientToRecipeMobile.RecipeDto), RecipeVariantDto },
                { nameof(AddIngredientToRecipeMobile.OnCreateData),
                    EventCallback.Factory.Create<Dictionary<int, RecipeComponentType>>(this, DialogAddEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<AddIngredientToRecipeMobile>("AddIngredientToRecipeMobile", parameters, options);
        }

        private void DialogDelete()
        {
            var parameters = new DialogParameters
            {
                { nameof(DeleteIngredientToRecipeMobile.RecipeDto), RecipeVariantDto },
                { nameof(DeleteIngredientToRecipeMobile.OnDeleteData),
                    EventCallback.Factory.Create<Dictionary<int, RecipeComponentType>>(this, DialogDeleteEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<DeleteIngredientToRecipeMobile>("DeleteIngredientToRecipeMobile", parameters, options);
        }
        public async Task DialogAddEvent(Dictionary<int, RecipeComponentType> ids)
        {
            try
            {
                List<RecipeComponentTable> recipeComponentDtos = new();

                foreach (var entry in ids)
                {
                    int id = entry.Key;
                    RecipeComponentType type = entry.Value;

                    switch (type)
                    {
                        case RecipeComponentType.Ingredient:
                            var newIngredient = await _recipeIngredientService.GetByIdAsync(id);
                            if (newIngredient != null)
                            {
                                recipeComponentDtos.Add(new RecipeComponentTable
                                {
                                    Id = newIngredient.Id,
                                    Name = newIngredient.Ingredient.Name,
                                    Unit = newIngredient.Ingredient.Unit,
                                    CostPerUnit = newIngredient.Ingredient.CostPerUnit,
                                    Type = RecipeComponentType.Ingredient
                                });

                                RecipeVariantDto.Ingredients.Add(newIngredient);
                            }
                            break;

                        case RecipeComponentType.Recipe:
                            var recipeComponent = await _recipeComponentService.GetByIdAsync(id);
                            if (recipeComponent != null)
                            {
                                recipeComponentDtos.Add(new RecipeComponentTable
                                {
                                    Id = recipeComponent.Id,
                                    Name = recipeComponent.ChildRecipeName,
                                    Unit = UnitEnum.Ud,
                                    Quantity = recipeComponent.Quantity,
                                    CostPerUnit = recipeComponent.ChildRecipeVariant.GetCost(),
                                    Type = RecipeComponentType.Recipe
                                });

                                RecipeVariantDto.Components.Add(recipeComponent);

                            }
                            break;
                    }
                }

                StateHasChanged();
            }
            catch
            {
                throw;
            }
        }

        public async Task DialogDeleteEvent(Dictionary<int, RecipeComponentType> ids)
        {
            try
            {
                foreach (var entry in ids)
                {
                    int id = entry.Key;
                    RecipeComponentType type = entry.Value;

                    switch (type)
                    {
                        case RecipeComponentType.Ingredient:
                            var ingredient = RecipeVariantDto.Ingredients.FirstOrDefault(x => x.Id == id);
                            if (ingredient != null)
                            {
                                var success = await _recipeIngredientService.DeleteAsync(ingredient.Id);

                                if (success)
                                {
                                    RecipeVariantDto.Ingredients.Remove(ingredient);
                                }
                            }
                            break;
                        case RecipeComponentType.Recipe:
                            var recipeEntity = RecipeVariantDto.Components.FirstOrDefault(x => x.ChildRecipeVariantId == id);

                            if (recipeEntity != null)
                            {
                                var success = await _recipeComponentService.DeleteAsync(RecipeDto.Id, id);

                                if (success)
                                {

                                    RecipeVariantDto.Components.Remove(recipeEntity);
                                }
                            }
                            break;
                    }
                }

                StateHasChanged();
            }
            catch
            {
                throw;
            }
        }
    }
}
