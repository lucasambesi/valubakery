using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Materials
{
    public partial class Materials
    {
        private List<string> editEvents = new();
        private string searchString = "";
        private MaterialDto selectedItem = null;
        private MaterialDto MaterialDtoBeforeEdit;
        private HashSet<MaterialDto> selectedItems = new HashSet<MaterialDto>();
        private List<MaterialDto> MaterialDtos = new List<MaterialDto>();

        protected override async Task OnInitializedAsync()
        {
            MaterialDtos = await _materialService.GetAllAsync();
        }

        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        private void BackupItem(object MaterialDto)
        {
            MaterialDtoBeforeEdit = new()
            {
                Id = ((MaterialDto)MaterialDto).Id,
                Name = ((MaterialDto)MaterialDto).Name,
                Unit = ((MaterialDto)MaterialDto).Unit,
                CostPerUnit = ((MaterialDto)MaterialDto).CostPerUnit,
                IsDeleted = ((MaterialDto)MaterialDto).IsDeleted
            };
        }

        private async void ItemHasBeenCommitted(object materialDto)
        {
            var dto = (MaterialDto)materialDto;

            try
            {
                await _materialService.UpdateAsync(dto);
                AddEditionEvent($"Fila editada: Cambios en {dto.Name} guardados");
            }
            catch (Exception ex)
            {
                Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
            }
        }


        private void ResetItemToOriginalValues(object MaterialDto)
        {
            ((MaterialDto)MaterialDto).Id = MaterialDtoBeforeEdit.Id;
            ((MaterialDto)MaterialDto).Name = MaterialDtoBeforeEdit.Name;
            ((MaterialDto)MaterialDto).Unit = MaterialDtoBeforeEdit.Unit;
            ((MaterialDto)MaterialDto).CostPerUnit = MaterialDtoBeforeEdit.CostPerUnit;
            ((MaterialDto)MaterialDto).IsDeleted = MaterialDtoBeforeEdit.IsDeleted;
        }

        private bool FilterFunc(MaterialDto MaterialDto)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (MaterialDto.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        #region Dialog
        private void DialogCreate()
        {
            var parameters = new DialogParameters
            {
                { nameof(CreateMaterial.OnCreateData), EventCallback.Factory.Create<int>(this, CreateDialogEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<CreateMaterial>("CreateMaterial", parameters, options);
        }

        public async Task CreateDialogEvent(int id)
        {
            try
            {
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
        }
        #endregion
    }
}
