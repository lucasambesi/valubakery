using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Products.Mobile
{
    public partial class DeleteMaterialToProductMobile
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public ProductDto ProductDto { get; set; }

        [Parameter]
        public EventCallback<Dictionary<int, ProductComponentType>> OnDeleteData
        {
            get; set;
        }

        private List<MaterialDto> MaterialsDtos = new();
        private HashSet<int> SelectedMaterialIds = new();

        protected override async Task OnInitializedAsync()
        {
            var ings = await _materialService.GetAllAsync();
            MaterialsDtos = ings
                .Where(x => ProductDto.ProductMaterials
                    .Select(t => t.MaterialId)
                    .Contains(x.Id))
                .ToList();
        }

        private async Task Submit()
        {
            try
            {
                Dictionary<int, ProductComponentType> componentMap = new();

                foreach (var ig in SelectedMaterialIds)
                {
                    componentMap[ig] = ProductComponentType.Material;
                }

                await OnDeleteData.InvokeAsync(componentMap);
            }
            catch
            {
                throw;
            }
            finally
            {
                MudDialog.Close();
                StateHasChanged();
            }
        }

        private void OnCheckedChanged(bool value, int id)
        {
            if (value)
                SelectedMaterialIds.Add(id);
            else
                SelectedMaterialIds.Remove(id);
        }
    }
}
