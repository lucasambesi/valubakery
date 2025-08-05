using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Products.Mobile
{
    public partial class AddMaterialToProductMobile
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public ProductDto ProductDto { get; set; }

        [Parameter]
        public EventCallback<Dictionary<int, ProductComponentType>> OnCreateData
        {
            get; set;
        }

        private List<MaterialDto> MaterialDtos = new();
        private HashSet<int> SelectedMaterialIds = new();

        protected override async Task OnInitializedAsync()
        {
            var ings = await _materialService.GetAllAsync();
            MaterialDtos = ings
                .Where(x => !ProductDto.ProductMaterials
                    .Select(t => t.MaterialId)
                    .Contains(x.Id))
                .ToList();
        }

        private async Task Submit()
        {
            try
            {
                List<ProductMaterialDto> igs = new();
                foreach (var materialDto in SelectedMaterialIds)
                {
                    ProductMaterialDto recipeMaterialDto = new()
                    {
                        ProductId = ProductDto.Id,
                        MaterialId = materialDto,
                        Quantity = 1
                    };

                    var id = await _productMaterialService.AddAsync(recipeMaterialDto);

                    recipeMaterialDto.Id = id;
                    igs.Add(recipeMaterialDto);
                }

                Dictionary<int, ProductComponentType> componentMap = new();

                foreach (var ig in igs)
                {
                    componentMap[ig.Id] = ProductComponentType.Material;
                }
                await OnCreateData.InvokeAsync(componentMap);
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
