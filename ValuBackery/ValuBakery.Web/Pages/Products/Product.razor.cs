using Microsoft.AspNetCore.Components;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Products
{
    public partial class Product
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public ProductDto? ProductDto { get; set; }

        private Guid dialogRenderKey = Guid.NewGuid();

        private int maxLengthDescription = 120;

        private bool showFullDescription = false;

        private int maxLengthComponents = 120;

        private bool showFullComponents = false;

        private string componentsText = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (Id != default)
            {
                ProductDto = await _productService.GetByIdAsync(Id);
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
    }
}
