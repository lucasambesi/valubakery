using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Products
{
    public partial class EditProduct
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        public EditProductModel EditProductModel { get; set; } = new EditProductModel();

        [Parameter]
        public ProductDto? ProductDto { get; set; }

        private void Cancel() => MudDialog.Cancel();



        [Parameter]
        public EventCallback<ProductDto> OnCreateData { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EditProductModel.Name = ProductDto.Name;
            EditProductModel.Description = ProductDto.Description;
            EditProductModel.IsActive = !ProductDto.IsDeleted;
            EditProductModel.ApplyProfitToMaterials = ProductDto.ApplyProfitToMaterials;
            EditProductModel.ApplyProfitToRecipes = ProductDto.ApplyProfitToRecipes;
        }

        private async Task Submit()
        {
            // Normalizar entradas
            EditProductModel.Name = EditProductModel.Name?.Trim();
            EditProductModel.Description = EditProductModel.Description?.Trim();

            if (string.IsNullOrWhiteSpace(EditProductModel.Name))
            {
                _snackbar.Add("El nombre es obligatorio", Severity.Warning);
                return;
            }

            try
            {
                var productDto = new ProductDto
                {
                    Id = ProductDto.Id,
                    Name = EditProductModel.Name,
                    Description = EditProductModel.Description,
                    IsDeleted = !EditProductModel.IsActive,
                    ApplyProfitToRecipes = EditProductModel.ApplyProfitToRecipes,
                    ApplyProfitToMaterials = EditProductModel.ApplyProfitToMaterials
                };

                var sucess = await _productService.UpdateAsync(productDto);
                if (sucess)
                {
                    _snackbar.Add("Producto editado", Severity.Success);
                    MudDialog?.Close(DialogResult.Ok(true));
                    await OnCreateData.InvokeAsync(productDto);
                }
                else
                {
                    _snackbar.Add("Error al crear", Severity.Error);
                }
            }
            catch (Exception)
            {
                _snackbar.Add("Ocurrió un error inesperado", Severity.Error);
                throw;
            }
        }

        private void CapitalizeName()
        {
            if (!string.IsNullOrWhiteSpace(EditProductModel.Name))
            {
                var name = EditProductModel.Name.Trim();
                EditProductModel.Name = char.ToUpper(name[0]) + name[1..];
            }
        }

        private async Task HandleEnterKey(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await Submit();
            }
        }
    }
}
