using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Products
{
    public partial class Products
    {

        private List<string> editEvents = new();
        private string searchString = "";
        private ProductDto selectedItem = null;
        private ProductDto ProductDtoBeforeEdit;
        private HashSet<ProductDto> selectedItems = new HashSet<ProductDto>();
        private List<ProductDto> ProductDtos = new List<ProductDto>();

        protected override async Task OnInitializedAsync()
        {
            ProductDtos = await _productService.GetAllAsync();
        }

        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        private void BackupItem(object ProductDto)
        {
            ProductDtoBeforeEdit = new()
            {
                Id = ((ProductDto)ProductDto).Id,
                Name = ((ProductDto)ProductDto).Name,
            };
        }

        private async void ItemHasBeenCommitted(object productDto)
        {
            var dto = (ProductDto)productDto;

            try
            {
                await _productService.UpdateAsync(dto);
                AddEditionEvent($"Fila editada: Cambios en {dto.Name} guardados");
            }
            catch (Exception ex)
            {
                Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
            }
        }


        private void ResetItemToOriginalValues(object ProductDto)
        {
            ((ProductDto)ProductDto).Id = ProductDtoBeforeEdit.Id;
            ((ProductDto)ProductDto).Name = ProductDtoBeforeEdit.Name;
        }

        private bool FilterFunc(ProductDto ProductDto)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (ProductDto.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        #region Dialog
        private void DialogCreate()
        {
            var parameters = new DialogParameters
            {
                { nameof(CreateProduct.OnCreateData), EventCallback.Factory.Create<int>(this, CreateDialogEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<CreateProduct>("CreateProduct", parameters, options);
        }

        private void ViewProduct(int id)
        {
            var parameters = new DialogParameters
            {
                { nameof(ProductDto.Id), id }
            };

            var options = new DialogOptions
            {
                FullWidth = true,
                MaxWidth = MaxWidth.Medium,
                DisableBackdropClick = true,
                Position = DialogPosition.Center,
                NoHeader = false,
                CloseButton = true
            };

            _dialogService.Show<Product>("", parameters, options);
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
