using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using System.ComponentModel;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Data.Enums;
using ValuBakery.Web.Data;
using ValuBakery.Web.Pages.Ingredients;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class RecipeIngredients
    {
        [CascadingParameter]
        public RecipeVariantDto RecipeDto { get; set; }

        private RecipeComponentTable selectedItem = null;

        private RecipeComponentTable RecipeIngredientDtoBeforeEdit;

        private string searchString = "";

        [Parameter]
        public EventCallback OnChanged { get; set; }


        public List<RecipeComponentTable> RecipeComponents { get; set; } = new List<RecipeComponentTable>();

        protected override async Task OnInitializedAsync()
        {
            if (RecipeDto == null) return;

            RecipeComponents.AddRange(RecipeDto.Ingredients
                .Select(ingredient =>
                {
                    return new RecipeComponentTable
                    {
                        Id = ingredient.Id,
                        Name = ingredient.Ingredient.Name,
                        Unit = ingredient.Ingredient.Unit,
                        Quantity = ingredient.Quantity,
                        CostPerUnit = ingredient.Ingredient.CostPerUnit,
                        Type = RecipeComponentType.Ingredient
                    };
                })
                .ToList());

            RecipeComponents.AddRange(RecipeDto.Components.Where(x => x.ParentRecipeVariantId == RecipeDto.Id)
                .Select(recipe =>
                {
                    return new RecipeComponentTable
                    {
                        Id = recipe.Id,
                        Name = recipe.ChildRecipeVariant.GetName(),
                        Unit = UnitEnum.Ud,
                        Quantity = recipe.Quantity,
                        CostPerUnit = recipe.ChildRecipeVariant.GetCost(),
                        Type = RecipeComponentType.Recipe
                    };
                })
                .ToList());

            RecipeComponents.OrderBy(x => x.Type).ToList();
        }

        #region Ingredients table

        private void BackupItem(object RecipeIngredientDto)
        {
            RecipeIngredientDtoBeforeEdit = new()
            {
                Id = ((RecipeComponentTable)RecipeIngredientDto).Id,
                Quantity = ((RecipeComponentTable)RecipeIngredientDto).Quantity,
            };
        }

        private async void ItemHasBeenCommitted(object recipeIngredientDto)
        {
            var dto = (RecipeComponentTable)recipeIngredientDto;

            switch (dto.Type)   
            {
                case RecipeComponentType.Ingredient:
                    var ingredient = RecipeDto.Ingredients.FirstOrDefault(x => x.Id == dto.Id);

                    if (ingredient == null) return;

                    ingredient.Quantity = dto.Quantity;
                    try
                    {
                        await _recipeIngredientService.UpdateAsync(ingredient);
                    }
                    catch (Exception ex)
                    {
                        Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
                    }
                    break;
                case RecipeComponentType.Recipe:
                    var component = RecipeDto.Components.FirstOrDefault(x => x.Id == dto.Id);

                    if (component == null) return;

                    component.Quantity = dto.Quantity;
                    try
                    {
                        await _recipeComponentService.UpdateAsync(component);
                    }
                    catch (Exception ex)
                    {
                        Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
                    }
                    break;
                default:
                    break;
            }


            await OnChanged.InvokeAsync();
        }


        private void ResetItemToOriginalValues(object RecipeIngredientDto)
        {
            ((RecipeComponentTable)RecipeIngredientDto).Id = RecipeIngredientDtoBeforeEdit.Id;
            ((RecipeComponentTable)RecipeIngredientDto).Quantity = RecipeIngredientDtoBeforeEdit.Quantity;
        }

        private bool FilterFunc(RecipeComponentTable RecipeIngredientDto)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (RecipeIngredientDto.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        #endregion

        #region Dialog
        private void DialogAdd()
        {
            var parameters = new DialogParameters
            {
                { nameof(AddIngredientsToRecipe.RecipeDto), RecipeDto },
                { nameof(AddIngredientsToRecipe.OnCreateData),
                    EventCallback.Factory.Create<Dictionary<int, RecipeComponentType>>(this, DialogAddEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<AddIngredientsToRecipe>("AddIngredientsToRecipe", parameters, options);
        }

        private void DialogDelete()
        {
            var parameters = new DialogParameters
            {
                { nameof(DeleteIngredientsToRecipe.RecipeDto), RecipeDto },
                { nameof(DeleteIngredientsToRecipe.OnDeleteData),
                    EventCallback.Factory.Create<Dictionary<int, RecipeComponentType>>(this, DialogDeleteEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<DeleteIngredientsToRecipe>("DeleteIngredientsToRecipe", parameters, options);
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

                                RecipeDto.Ingredients.Add(newIngredient);
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

                                RecipeDto.Components.Add(recipeComponent);

                            }
                            break;
                    }
                }

                RecipeComponents.AddRange(recipeComponentDtos);

                await OnChanged.InvokeAsync();
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
                            var ingredient = RecipeDto.Ingredients.FirstOrDefault(x => x.Id == id);
                            if (ingredient != null)
                            {
                                var recipeComp = RecipeComponents.FirstOrDefault(x => x.Id == id && x.Type == type);
                                if (recipeComp != null)
                                {
                                    RecipeComponents.Remove(recipeComp);
                                }
                                RecipeDto.Ingredients.Remove(ingredient);
                            }
                            break;
                        case RecipeComponentType.Recipe:
                            var recipeEntity = RecipeDto.Components.FirstOrDefault(x => x.ChildRecipeVariantId == id);

                            if (recipeEntity != null)
                            {
                                var recipeComp = RecipeComponents.FirstOrDefault(x => x.Id == recipeEntity.Id && x.Type == type);
                                if (recipeComp != null)
                                {
                                    RecipeComponents.Remove(recipeComp);
                                }

                                RecipeDto.Components.Remove(recipeEntity);
                            }
                            break;
                    }
                }

                await OnChanged.InvokeAsync();
            }
            catch
            {
                throw;
            }
        }

        private void ViewItem(int componentId, RecipeComponentType type)
        {
            switch (type)
            {
                case RecipeComponentType.Ingredient:
                    ViewIngredient(componentId);
                    break;
                case RecipeComponentType.Recipe:
                    ViewRecipe(componentId);
                    break;
                default:
                    break;
            }            
        }

        private void ViewIngredient(int id)
        {
            var item = RecipeDto.Ingredients.FirstOrDefault(x => x.Id == id);

            var parameters = new DialogParameters
            {
                { nameof(ViewIngredientResumen.IngredientDto), item.Ingredient }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<ViewIngredientResumen>("ViewIngredientResumen", parameters, options);
        }

        private void ViewRecipe(int id)
        {
            var item = RecipeDto.Components.FirstOrDefault(x => x.Id == id);

            var parameters = new DialogParameters
            {
                { nameof(ViewRecipeResumen.Id), item.ChildRecipeVariantId }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };

            _dialogService.Show<ViewRecipeResumen>("ViewRecipeResumen", parameters, options);
        }

        #endregion
    }
}
