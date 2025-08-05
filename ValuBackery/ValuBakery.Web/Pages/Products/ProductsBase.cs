using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Web.Pages.Products.Mobile;
using ValuBakery.Web.Pages.Recipes.Mobile;

namespace ValuBakery.Web.Pages.Products
{
    public class ProductsBase : ComponentBase
    {
        protected List<ProductDto> ProductDtos = new List<ProductDto>();
        [Inject] protected IProductService _productService { get; set; }
        [Inject] protected IDialogService _dialogService { get; set; }
        [Inject] protected ISnackbar Snackbar { get; set; }

        protected bool isLoading;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            ProductDtos = await _productService.GetAllAsync();
            isLoading = false;
        }

        #region Dialog
        protected void DialogCreate()
        {
            var parameters = new DialogParameters
            {
                { nameof(CreateProduct.OnCreateData), EventCallback.Factory.Create<int>(this, CreateDialogEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<CreateProduct>("CreateProduct", parameters, options);
        }

        protected void ViewProduct(int id, bool isMobile)
        {
            var parameters = new DialogParameters
            {
                { isMobile ? nameof(ProductMobile.Id) : nameof(Product.Id), id },
                { isMobile ? nameof(ProductMobile.OnEditData) : nameof(Product.OnEditData),
                EventCallback.Factory.Create<ProductDto>(this, EditDialogEvent)  }

            };

            var options = new DialogOptions
            {
                FullWidth = true,
                MaxWidth = MaxWidth.Medium,
                DisableBackdropClick = false,
                CloseOnEscapeKey = true,
                Position = DialogPosition.Center,
                NoHeader = false,
                CloseButton = true
            };

            if (isMobile)
            {

                _dialogService.Show<ProductMobile>("", parameters, options);
            }
            else
            {
                _dialogService.Show<Product>("", parameters, options);
            }
        }

        private void EditDialogEvent(ProductDto recipeDto)
        {
            var index = ProductDtos.FindIndex(x => x.Id == recipeDto.Id);
            if (index != -1)
            {
                if (recipeDto.IsDeleted)
                {
                    ProductDtos.RemoveAt(index);
                }
                else
                {
                    ProductDtos[index] = recipeDto;
                }

                StateHasChanged();
            }
        }

        public async Task CreateDialogEvent(int id)
        {
            try
            {
                var entity = await _productService.GetByIdAsync(id);

                if (entity != null)
                {
                    var item = entity;

                    if (item != null)
                    {
                        ProductDtos.Insert(0, item);
                        ViewProduct(item.Id, true);
                    }

                    StateHasChanged();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
