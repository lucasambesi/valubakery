using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;

namespace ValuBakery.Web.Pages.Ingredients
{
    public partial class Ingredients
    {
        private List<string> editEvents = new();
        private string searchString = "";
        private IngredientDto selectedItem = null;
        private IngredientDto IngredientDtoBeforeEdit;
        private HashSet<IngredientDto> selectedItems = new HashSet<IngredientDto>();
        private List<IngredientDto> IngredientDtos = new List<IngredientDto>();

        protected override async Task OnInitializedAsync()
        {
            IngredientDtos = await _ingredientService.GetAllAsync();
        }

        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        private void BackupItem(object IngredientDto)
        {
            IngredientDtoBeforeEdit = new()
            {
                Id = ((IngredientDto)IngredientDto).Id,
                Name = ((IngredientDto)IngredientDto).Name,
                Unit = ((IngredientDto)IngredientDto).Unit,
                CostPerUnit = ((IngredientDto)IngredientDto).CostPerUnit,
                IsDeleted = ((IngredientDto)IngredientDto).IsDeleted
            };
        }

        private async void ItemHasBeenCommitted(object ingredientDto)
        {
            var dto = (IngredientDto)ingredientDto;

            try
            {
                await _ingredientService.UpdateAsync(dto);
                AddEditionEvent($"Fila editada: Cambios en {dto.Name} guardados");
            }
            catch (Exception ex)
            {
                Snackbar.Add("Error al guardar: " + ex.Message, Severity.Error);
            }
        }


        private void ResetItemToOriginalValues(object IngredientDto)
        {
            ((IngredientDto)IngredientDto).Id = IngredientDtoBeforeEdit.Id;
            ((IngredientDto)IngredientDto).Name = IngredientDtoBeforeEdit.Name;
            ((IngredientDto)IngredientDto).Unit = IngredientDtoBeforeEdit.Unit;
            ((IngredientDto)IngredientDto).CostPerUnit = IngredientDtoBeforeEdit.CostPerUnit;
            ((IngredientDto)IngredientDto).IsDeleted = IngredientDtoBeforeEdit.IsDeleted;
        }

        private bool FilterFunc(IngredientDto IngredientDto)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (IngredientDto.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        #region Dialog
        private void DialogCreate()
        {
            var parameters = new DialogParameters
            {
                { nameof(CreateIngredient.OnCreateData), EventCallback.Factory.Create<int>(this, CreateDialogEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<CreateIngredient>("CreateIngredient", parameters, options);
        }

        public async Task CreateDialogEvent(int id)
        {
            try
            {
                var entity = await _ingredientService.GetByIdAsync(id);

                if (entity != null)
                {
                    var item = entity;

                    if (item != null)
                    {
                        IngredientDtos.Insert(0, item);
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
