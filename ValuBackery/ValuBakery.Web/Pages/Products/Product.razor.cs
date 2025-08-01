using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;
using ValuBakery.Web.Pages.Recipes;

namespace ValuBakery.Web.Pages.Products
{
    public partial class Product
    {
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

        private int maxLengthDescription = 120;

        private bool showFullDescription = false;

        private int maxLengthComponents = 120;

        private bool showFullComponents = false;

        private string componentsText = string.Empty;

        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            if (Id != default)
            {
                isLoading = true;
                ProductDto = await _productService.GetByIdAsync(Id);
                isLoading = false;
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            componentsText = ProductDto?.GetComponents();
            ProductDto?.SetTotal();
        }

        private void ToggleDescription()
        {
            showFullDescription = !showFullDescription;
            StateHasChanged();
        }

        private void ToggleComponents()
        {
            showFullComponents = !showFullComponents;
            StateHasChanged();
        }

        private async Task OnComponentsChanged()
        {
            if (ProductDto != null)
            {
                dialogRenderKey = Guid.NewGuid();
                ProductDto.SetTotal();
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
                showFullComponents = false;
                componentsText = ProductDto?.GetComponents();
                ProductDto?.SetTotal();

                dialogRenderKey = Guid.NewGuid(); // fuerza redibujado del diálogo
                await InvokeAsync(StateHasChanged); // asegura el render completo
            }

            isLoading = false;
        }
    }
}
