using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;

namespace ValuBakery.Web.Pages.Products.Mobile
{
    public partial class ProductMaterialsMobile
    {
        [Parameter]
        public ProductDto ProductDto { get; set; } = new();

        [Parameter]
        public List<ProductMaterialDto> Materials { get; set; } = new();

        private ProductMaterialDto? EditingItem;
        private decimal originalQuantity;

        [Parameter]
        public EventCallback OnChanged { get; set; }

        private void StartEditing(ProductMaterialDto item)
        {
            EditingItem = item;
            originalQuantity = item.Quantity;
        }

        private async Task ConfirmEdit()
        {
            var material = Materials.FirstOrDefault(x => x.Id == EditingItem.Id);


            if (material == null) return;

            material.Quantity = EditingItem.Quantity;

            try
            {
                var response = await _productMaterialService.UpdateAsync(EditingItem);

            }
            catch (Exception ex)
            {
                Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
            }

            EditingItem = null;

            await OnChanged.InvokeAsync();
        }

        private void CancelEdit()
        {
            if (EditingItem != null)
            {
                EditingItem.Quantity = originalQuantity;
                EditingItem = null;
            }
        }

        private string GetUnitAbbr(UnitMaterialEnum unit) =>
            unit switch
            {
                UnitMaterialEnum.Kg => "Kg",
                UnitMaterialEnum.Grs => "g",
                UnitMaterialEnum.Lt => "L",
                UnitMaterialEnum.Mls => "ml",
                UnitMaterialEnum.Box => "caja",
                UnitMaterialEnum.Meter => "mt",
                UnitMaterialEnum.Roll => "rollo",
                UnitMaterialEnum.Ud => "u",
                _ => ""
            };
    }
}
