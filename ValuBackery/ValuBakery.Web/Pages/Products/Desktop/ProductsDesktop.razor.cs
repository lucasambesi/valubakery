using MudBlazor;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Products.Desktop
{
    public partial class ProductsDesktop
    {
        private List<string> editEvents = new();
        private string searchString = "";
        private ProductDto selectedItem = null;
        private ProductDto ProductDtoBeforeEdit;
        private HashSet<ProductDto> selectedItems = new HashSet<ProductDto>();

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
    }
}
