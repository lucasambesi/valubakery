using Microsoft.AspNetCore.Components;
using ValuBakery.Data.DTOs;

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

        protected override async Task OnInitializedAsync()
        {
            if (Id != default)
            {
                ProductDto = await _productService.GetByIdAsync(Id);
            }
        }
    }
}
