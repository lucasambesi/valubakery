using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Products
{
    public partial class CreateProduct
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public EventCallback<int> OnCreateData { get; set; }

        [Parameter]
        public ProductDto ProductDto { get; set; } = new ProductDto() { ApplyProfitToMaterials = 100, ApplyProfitToRecipes = 100};

        private void Cancel() => MudDialog.Cancel();

        private async Task Submit()
        {
            if (string.IsNullOrWhiteSpace(ProductDto.Name))
            {
                _snackbar.Add("El nombre es obligatorio", Severity.Warning);
                return;
            }

            try
            {
                var id = await _productService.AddAsync(ProductDto);
                if (id > 0)
                {
                    MudDialog?.Close(DialogResult.Ok(true));
                    await OnCreateData.InvokeAsync(id);
                }
                else
                {
                    _snackbar.Add("Error al crear", Severity.Error);
                }

                _snackbar.Add("Product creado", Severity.Success);
            }
            catch
            {
                throw;
            }
            finally
            {
                MudDialog.Close(DialogResult.Ok(ProductDto.Id));
                StateHasChanged();
            }
        }
    }
}
