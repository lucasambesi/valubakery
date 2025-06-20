﻿using Microsoft.AspNetCore.Components;
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

        public RecipeDto? RecipeDto { get; set; }

        private bool showFullIngredients = false;
        private bool showFullComponents = false;
        private string ingredientsText { get; set; }
        private int maxLengthIngredients = 120;
        private string componentsText { get; set; }
        private int maxLengthComponents = 50;
        private Guid dialogRenderKey = Guid.NewGuid();
        protected override async Task OnInitializedAsync()
        {
            if (Id != default)
            {
                RecipeDto = await _recipeService.GetByIdAsync(Id);

                if (RecipeDto != null)
                {
                    RecipeVariantDto = await _recipeVariantService.GetByIdAsync(RecipeDto.Variants.First().Id);
                    
                }
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
            StateHasChanged();
        }

        private void ToggleComponents()
        {
            showFullComponents = !showFullComponents;
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


        private MarkupString GetShortOrFull(string? content, bool expanded, int maxLength, out bool isTruncated)
        {
            isTruncated = false;

            if (string.IsNullOrWhiteSpace(content))
                return (MarkupString)"";

            if (expanded || content.Length <= maxLength)
                return (MarkupString)content;

            isTruncated = true;
            return (MarkupString)(content.Substring(0, maxLength) + "...");
        }
    }
}
