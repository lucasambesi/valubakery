using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;

namespace ValuBakery.Web.Pages.Materials
{
    public partial class CreateMaterial
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public EventCallback<int> OnCreateData { get; set; }

        [Parameter]
        public MaterialDto MaterialDto { get; set; } = new MaterialDto() { Unit = UnitMaterialEnum.Ud };

        private void Cancel() => MudDialog.Cancel();

        private async Task Submit()
        {
            if (string.IsNullOrWhiteSpace(MaterialDto.Name))
            {
                _snackbar.Add("El nombre es obligatorio", Severity.Warning);
                return;
            }

            try
            {
                var id = await _materialService.AddAsync(MaterialDto);
                if (id > 0)
                {
                    MudDialog?.Close(DialogResult.Ok(true));
                    await OnCreateData.InvokeAsync(id);
                }
                else
                {
                    _snackbar.Add("Error al crear", Severity.Error);
                }

                _snackbar.Add("Material creado", Severity.Success);
            }
            catch
            {
                throw;
            }
            finally
            {
                MudDialog.Close(DialogResult.Ok(MaterialDto.Id));
                StateHasChanged();
            }
        }
    }
}
