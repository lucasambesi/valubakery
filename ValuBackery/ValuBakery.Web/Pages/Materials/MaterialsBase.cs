using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Materials
{
    public class MaterialsBase : ComponentBase
    {
        protected List<string> editEvents = new();
        protected string searchString = "";
        protected MaterialDto selectedItem = null;
        protected MaterialDto MaterialDtoBeforeEdit;
        protected HashSet<MaterialDto> selectedItems = new HashSet<MaterialDto>();
        protected List<MaterialDto> MaterialDtos = new List<MaterialDto>();

        [Inject] protected IMaterialService _materialService { get; set; }
        [Inject] protected IDialogService _dialogService { get; set; }
        [Inject] protected ISnackbar Snackbar { get; set; }

        protected bool isLoading;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            MaterialDtos = await _materialService.GetAllAsync();
            isLoading = false;
        }

        #region Dialog
        protected void DialogCreate()
        {
            var parameters = new DialogParameters
            {
                { nameof(CreateMaterial.OnCreateData), EventCallback.Factory.Create<int>(this, CreateDialogEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<CreateMaterial>("CreateMaterial", parameters, options);
        }

        public async Task CreateDialogEvent(int id)
        {
            try
            {
                isLoading = true;

                var entity = await _materialService.GetByIdAsync(id);

                if (entity != null)
                {
                    var item = entity;

                    if (item != null)
                    {
                        MaterialDtos.Insert(0, item);
                    }

                    StateHasChanged();
                }
            }
            catch
            {
                throw;
            }
            finally { isLoading = false; }
        }

        protected void ViewMaterial(MaterialDto material)
        {
            var parameters = new DialogParameters
            {
                { nameof(ViewMaterialResumen.MaterialDto), material }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                CloseOnEscapeKey = true,
                DisableBackdropClick = false,
                FullWidth = true
            };

            _dialogService.Show<ViewMaterialResumen>("ViewMaterialResumen", parameters, options);
        }
        #endregion
    }
}
