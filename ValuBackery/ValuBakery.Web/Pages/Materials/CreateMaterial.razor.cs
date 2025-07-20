using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;
using ValuBakery.Web.Pages.Common;

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

        private void OpenCalculateDialog()
        {
            var parameters = new DialogParameters
            {
                {nameof(CalculateVariable.Title), "Calcular costo" },
                {nameof(CalculateVariable.FirstField), "Total gastado" },
                {nameof(CalculateVariable.SecondField), "Cantidad de unidades" },
                { nameof(CalculateVariable.OnChanged),
                    EventCallback.Factory.Create<decimal>(this, DialogCalculateEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<CalculateVariable>("CalculateVariable", parameters, options);
        }

        private void DialogCalculateEvent(decimal total)
        {
            if (total > 0)
            {
                MaterialDto.CostPerUnit = total;
            }
        }

        private void CapitalizeName()
        {
            if (!string.IsNullOrWhiteSpace(MaterialDto.Name))
            {
                var name = MaterialDto.Name.Trim();
                MaterialDto.Name = char.ToUpper(name[0]) + name[1..];
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
