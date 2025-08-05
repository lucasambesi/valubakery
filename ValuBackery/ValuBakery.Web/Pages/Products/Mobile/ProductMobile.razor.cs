using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;
using ValuBakery.Web.Data;
using ValuBakery.Web.Pages.Recipes.Mobile;

namespace ValuBakery.Web.Pages.Products.Mobile
{
    public partial class ProductMobile
    {
        private bool isLoading = true;
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public ProductDto? ProductDto { get; set; }

        [Parameter]
        public EventCallback<ProductDto> OnEditData
        {
            get; set;
        }

        private Guid dialogRenderKey = Guid.NewGuid();

        private bool infoInitialExpanded = true;
        private bool materialsInitialExpanded = false;
        private bool recipesInitialExpanded = false;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            if (Id != default)
            {
                ProductDto = await _productService.GetByIdAsync(Id);
            }
            isLoading = false;
        }

        protected override void OnParametersSet()
        {
            ProductDto?.SetTotal();
        }

        private async Task OnChanged()
        {
            if (ProductDto != null)
            {
                ProductDto.SetTotal();

                dialogRenderKey = Guid.NewGuid();
                materialsInitialExpanded = true;

                StateHasChanged();
            }
        }

        protected void DialogEdit()
        {
            var parameters = new DialogParameters
            {
                { nameof(EditProduct.OnCreateData), EventCallback.Factory.Create<ProductDto>(this, EditDialogEvent) },
                { nameof(EditProduct.ProductDto), ProductDto }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<EditProduct>("EditProduct", parameters, options);
        }

        protected async Task EditDialogEvent(ProductDto productDto)
        {
            try
            {
                UpdateProduct(productDto);
                await OnEditData.InvokeAsync(productDto);
            }
            catch
            {
                throw;
            }
        }

        protected async void UpdateProduct(ProductDto productDto)
        {
            isLoading = true;

            ProductDto = await _productService.GetByIdAsync(productDto.Id);

            if (ProductDto != null)
            {
                ProductDto?.SetTotal();

                dialogRenderKey = Guid.NewGuid(); // fuerza redibujado del diálogo
                await InvokeAsync(StateHasChanged); // asegura el render completo
                infoInitialExpanded= true;
            }

            isLoading = false;
        }

        #region dialogs de materials y recipes
        private void DialogAdd()
        {
            var parameters = new DialogParameters
            {
                { nameof(AddMaterialToProductMobile.ProductDto), ProductDto },
                { nameof(AddMaterialToProductMobile.OnCreateData),
                    EventCallback.Factory.Create<Dictionary<int, ProductComponentType>>(this, DialogAddEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<AddMaterialToProductMobile>("AddMaterialToProductMobile", parameters, options);
        }

        private void DialogRecipeAdd()
        {
            var parameters = new DialogParameters
            {
                { nameof(AddRecipeToProductMobile.ProductDto), ProductDto },
                { nameof(AddRecipeToProductMobile.OnCreateData),
                    EventCallback.Factory.Create<Dictionary<int, ProductComponentType>>(this, DialogAddEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<AddRecipeToProductMobile>("AddRecipeToProductMobile", parameters, options);
        }

        private void DialogDelete()
        {
            var parameters = new DialogParameters
            {
                { nameof(DeleteMaterialToProductMobile.ProductDto), ProductDto },
                { nameof(DeleteMaterialToProductMobile.OnDeleteData),
                    EventCallback.Factory.Create<Dictionary<int, ProductComponentType>>(this, DialogDeleteEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<DeleteMaterialToProductMobile>("DeleteMaterialToProductMobile", parameters, options);
        }

        private void DialogRecipeDelete()
        {
            var parameters = new DialogParameters
            {
                { nameof(DeleteRecipeToProductMobile.ProductDto), ProductDto },
                { nameof(DeleteRecipeToProductMobile.OnDeleteData),
                    EventCallback.Factory.Create<Dictionary<int, ProductComponentType>>(this, DialogDeleteEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<DeleteRecipeToProductMobile>("DeleteRecipeToProductMobile", parameters, options);
        }

        public async Task DialogAddEvent(Dictionary<int, ProductComponentType> ids)
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
                            var newMaterial = await _productMaterialService.GetByIdAsync(id);
                            if (newMaterial != null)
                            {
                                ProductDto.ProductMaterials.Add(newMaterial);
                            }
                            materialsInitialExpanded = true;
                            break;

                        case ProductComponentType.Recipe:
                            var recipeComponent = await _productRecipeVariantService.GetByIdAsync(id);
                            if (recipeComponent != null)
                            {
                                ProductDto.ProductRecipeVariants.Add(recipeComponent);

                            }
                            recipesInitialExpanded = true;
                            break;
                    }
                }

                ProductDto.SetTotal();
                dialogRenderKey = Guid.NewGuid();
                StateHasChanged();
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
                            var productMaterial = ProductDto.ProductMaterials.FirstOrDefault(x => x.MaterialId == id);
                            if (productMaterial != null)
                            {
                                var success = await _productMaterialService.DeleteAsync(productMaterial.Id);

                                if (success)
                                {
                                    ProductDto.ProductMaterials.Remove(productMaterial);
                                }
                            }
                            materialsInitialExpanded = true;
                            break;
                        case ProductComponentType.Recipe:
                            var productRecipe = ProductDto.ProductRecipeVariants.FirstOrDefault(x => x.Id == id);

                            if (productRecipe != null)
                            {
                                var success = await _productRecipeVariantService.DeleteAsync(productRecipe.Id);

                                if (success)
                                {
                                    ProductDto.ProductRecipeVariants.Remove(productRecipe);
                                }
                            }
                            recipesInitialExpanded = true;
                            break;
                    }
                }

                ProductDto.SetTotal();
                dialogRenderKey = Guid.NewGuid();
                StateHasChanged();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
