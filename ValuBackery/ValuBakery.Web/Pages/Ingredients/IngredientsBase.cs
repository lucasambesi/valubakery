using Microsoft.AspNetCore.Components;
using MudBlazor.Services;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Application.Services.Interfaces;

namespace ValuBakery.Web.Pages.Ingredients
{
    public class IngredientsBase : ComponentBase
    {
        [Inject] protected IIngredientService _ingredientService { get; set; }
        [Inject] protected IDialogService _dialogService { get; set; }
        [Inject] protected ISnackbar Snackbar { get; set; }

        protected List<string> editEvents = new();
        protected string searchString = "";
        protected IngredientDto selectedItem = null;
        protected IngredientDto IngredientDtoBeforeEdit;
        protected HashSet<IngredientDto> selectedItems = new HashSet<IngredientDto>();
        protected List<IngredientDto> IngredientDtos = new List<IngredientDto>();
        protected bool isLoading;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;

            IngredientDtos = await _ingredientService.GetAllAsync();

            isLoading = false;
        }

        #region Dialog
        protected void DialogCreate()
        {
            var parameters = new DialogParameters
            {
                { nameof(CreateIngredient.OnCreateData), EventCallback.Factory.Create<int>(this, CreateDialogEvent) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                CloseOnEscapeKey = true,
                FullWidth = true
            };

            _dialogService.Show<CreateIngredient>("CreateIngredient", parameters, options);
        }

        protected async Task CreateDialogEvent(int id)
        {
            try
            {
                isLoading = true;

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
            finally { isLoading = false; }
        }

        protected void ViewIngredient(IngredientDto ingredient)
        {
            var parameters = new DialogParameters
            {
                { nameof(ViewIngredientResumen.IngredientDto), ingredient }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                CloseOnEscapeKey = true,
                DisableBackdropClick = false,
                FullWidth = true
            };

            _dialogService.Show<ViewIngredientResumen>("ViewIngredientResumen", parameters, options);
        }
        #endregion
    }
}
