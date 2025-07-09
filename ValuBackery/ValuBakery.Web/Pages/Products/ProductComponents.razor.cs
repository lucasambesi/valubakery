using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using System.ComponentModel;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Data.Enums;
using ValuBakery.Web.Data;
using ValuBakery.Web.Pages.Ingredients;
using ValuBakery.Web.Pages.Materials;
using ValuBakery.Web.Pages.Recipes;

namespace ValuBakery.Web.Pages.Products
{
    public partial class ProductComponents
    {
        [CascadingParameter]
        public ProductDto ProductDto { get; set; }

        private ProductComponentTable selectedItem = null;

        private ProductComponentTable ProductIngredientDtoBeforeEdit;

        private string searchString = "";

        [Parameter]
        public EventCallback OnChanged { get; set; }

        public List<ProductComponentTable> ProductComponentTables { get; set; } = new List<ProductComponentTable>();

        protected override async Task OnInitializedAsync()
        {
            if (ProductDto == null) return;

            ProductComponentTables.AddRange(ProductDto.ProductRecipeVariants
                .Select(component =>
                {
                    var cost = component.RecipeVariant.GetCost();

                    return new ProductComponentTable
                    {
                        Id = component.Id,
                        Name = component.RecipeVariant.GetName(),
                        Quantity = component.Quantity,
                        CostPerUnit = cost,
                        Total = (cost + (cost * ProductDto.ApplyProfitToRecipes / 100)) * component.Quantity,
                        Profit = ProductDto.ApplyProfitToRecipes,
                        Type = ProductComponentType.Recipe
                    };
                })
            .ToList());

            ProductComponentTables.AddRange(ProductDto.ProductMaterials
                .Select(component =>
                {
                    var cost = component.Material.CostPerUnit;
                    return new ProductComponentTable
                    {
                        Id = component.Id,
                        Name = component.Material.Name,
                        Quantity = component.Quantity,
                        CostPerUnit = cost,
                        Total = (cost + (cost * ProductDto.ApplyProfitToRecipes / 100)) * component.Quantity,
                        Profit = ProductDto.ApplyProfitToMaterials,
                        Type = ProductComponentType.Material
                    };
                })
            .ToList());

            ProductComponentTables.OrderBy(x => x.Type).ToList();
        }

        #region Ingredients table

            private void BackupItem(object dto)
            {
            ProductIngredientDtoBeforeEdit = new()
                {
                    Id = ((ProductComponentTable)dto).Id,
                    Quantity = ((ProductComponentTable)dto).Quantity,
                };
            }

            private async void ItemHasBeenCommitted(object productoComponentTableDto)
            {
                var dto = (ProductComponentTable)productoComponentTableDto;

                switch (dto.Type)
                {
                    case ProductComponentType.Recipe:
                        var recipe = ProductDto.ProductRecipeVariants.FirstOrDefault(x => x.Id == dto.Id);

                        if (recipe == null) return;

                        recipe.Quantity = dto.Quantity;

                        try
                        {
                            await _productRecipeVariantService.UpdateAsync(recipe);

                            var cost = recipe.RecipeVariant.GetCost(); 
                            dto.Total = (cost + (cost * ProductDto.ApplyProfitToRecipes / 100)) * recipe.Quantity;
                        }
                        catch (Exception ex)
                        {
                            Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
                        }
                        break;
                    case ProductComponentType.Material:
                        var component = ProductDto.ProductMaterials.FirstOrDefault(x => x.Id == dto.Id);

                        if (component == null) return;

                        component.Quantity = dto.Quantity;

                        try
                        {
                            await _productMaterialService.UpdateAsync(component);

                            var cost = component.Material.CostPerUnit;
                            dto.Total = (cost + (cost * ProductDto.ApplyProfitToRecipes / 100)) * component.Quantity;
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


            private void ResetItemToOriginalValues(object dto)
            {
                ((ProductComponentTable)dto).Id = ProductIngredientDtoBeforeEdit.Id;
                ((ProductComponentTable)dto).Quantity = ProductIngredientDtoBeforeEdit.Quantity;
            }

            private bool FilterFunc(ProductComponentTable dto)
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (dto.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
        #endregion

        #region Dialog
        private void DialogAdd()
        {
            var parameters = new DialogParameters
            {
                { nameof(AddComponentsToProduct.ProductDto), ProductDto },
                { nameof(AddComponentsToProduct.OnCreateData),
                    EventCallback.Factory.Create<Dictionary<int, ProductComponentType>>(this, DialogAddEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<AddComponentsToProduct>("AddComponentsToProduct", parameters, options);
        }

        private void DialogDelete()
        {
            var parameters = new DialogParameters
            {
                { nameof(DeleteComponentsToProduct.ProductDto), ProductDto },
                { nameof(DeleteComponentsToProduct.OnDeleteData),
                    EventCallback.Factory.Create<Dictionary<int, ProductComponentType>>(this, DialogDeleteEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<DeleteComponentsToProduct>("DeleteComponentsToProduct", parameters, options);
        }

        public async Task DialogAddEvent(Dictionary<int, ProductComponentType> ids)
        {
            try
            {
                List<ProductComponentTable> productComponentDtos = new();

                foreach (var entry in ids)
                {
                    int id = entry.Key;
                    ProductComponentType type = entry.Value;

                    switch (type)
                    {
                        case ProductComponentType.Material:
                            var newMaterial = await _productMaterialService.GetByIdAsync(id);
                            if (newMaterial != null)
                            {
                                var cost = newMaterial.Material.CostPerUnit;

                                productComponentDtos.Add(new ProductComponentTable
                                {
                                    Id = newMaterial.Id,
                                    Name = newMaterial.Material.Name,
                                    Quantity = newMaterial.Quantity,
                                    CostPerUnit = cost,
                                    Profit = ProductDto.ApplyProfitToMaterials,
                                    Total = (cost + (cost * ProductDto.ApplyProfitToRecipes / 100)) * newMaterial.Quantity,
                                    Type = ProductComponentType.Material
                                });

                                ProductDto.ProductMaterials.Add(newMaterial);
                            }
                            break;

                        case ProductComponentType.Recipe:
                            var recipeComponent = await _productRecipeVariantService.GetByIdAsync(id);
                            if (recipeComponent != null)
                            {
                                var cost = recipeComponent.RecipeVariant.GetCost();

                                productComponentDtos.Add(new ProductComponentTable
                                {
                                    Id = recipeComponent.Id,
                                    Name = recipeComponent.RecipeVariant.GetName(),
                                    Quantity = recipeComponent.Quantity,
                                    CostPerUnit = cost,
                                    Profit = ProductDto.ApplyProfitToRecipes,
                                    Total = (cost + (cost * ProductDto.ApplyProfitToRecipes / 100)) * recipeComponent.Quantity,
                                    Type = ProductComponentType.Recipe
                                });

                                ProductDto.ProductRecipeVariants.Add(recipeComponent);

                            }
                            break;
                    }
                }

                ProductComponentTables.AddRange(productComponentDtos);

                await OnChanged.InvokeAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DialogDeleteEvent(Dictionary<int, ProductComponentType> ids)
        {
            try
            {
                foreach (var entry in ids)
                {
                    int id = entry.Key;
                    ProductComponentType type = entry.Value;

                    switch (type)
                    {
                        case ProductComponentType.Material:
                            var productMaterial = ProductDto.ProductMaterials.FirstOrDefault(x => x.Id == id);
                            if (productMaterial != null)
                            {
                                var materialComp = ProductComponentTables.FirstOrDefault(x => x.Id == id && x.Type == type);
                                if (materialComp != null)
                                {
                                    ProductComponentTables.Remove(materialComp);
                                }

                                ProductDto.ProductMaterials.Remove(productMaterial);

                                var success = await _productMaterialService.DeleteAsync(productMaterial.Id);
                            }
                            break;
                        case ProductComponentType.Recipe:
                            var recipeEntity = ProductDto.ProductRecipeVariants.FirstOrDefault(x => x.Id == id);

                            if (recipeEntity != null)
                            {
                                var recipeComp = ProductComponentTables.FirstOrDefault(x => x.Id == recipeEntity.Id && x.Type == type);
                                if (recipeComp != null)
                                {
                                    ProductComponentTables.Remove(recipeComp);
                                }

                                ProductDto.ProductRecipeVariants.Remove(recipeEntity);

                                await _productRecipeVariantService.DeleteAsync(recipeEntity.Id);
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

        private void ViewItem(int componentId, ProductComponentType type)
        {
            switch (type)
            {
                case ProductComponentType.Material:
                    ViewMaterial(componentId);
                    break;
                case ProductComponentType.Recipe:
                    ViewRecipe(componentId);
                    break;
                default:
                    break;
            }
        }

        private void ViewMaterial(int id)
        {
            var item = ProductDto.ProductMaterials.FirstOrDefault(x => x.Id == id);

            var parameters = new DialogParameters
            {
                { nameof(ViewMaterialResumen.MaterialDto), item.Material }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<ViewMaterialResumen>("ViewMaterialResumen", parameters, options);
        }

        private void ViewRecipe(int id)
        {
            var item = ProductDto.ProductRecipeVariants.FirstOrDefault(x => x.Id == id);

            var parameters = new DialogParameters
            {
                { nameof(ViewRecipeResumen.Id), item.RecipeVariant.Id }
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
