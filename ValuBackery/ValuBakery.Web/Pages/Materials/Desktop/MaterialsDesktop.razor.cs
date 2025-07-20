using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Helpers;

namespace ValuBakery.Web.Pages.Materials.Desktop
{
    public partial class MaterialsDesktop
    {
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

        protected bool FilterFunc(MaterialDto materialDto)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            var normalizedSearch = StringHelper.RemoveDiacritics(searchString).ToLowerInvariant();
            var normalizedName = StringHelper.RemoveDiacritics(materialDto.Name).ToLowerInvariant();

            return normalizedName.Contains(normalizedSearch);
        }

    }
}
