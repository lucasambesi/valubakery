using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Web.Pages.Ingredients
{
    public partial class EditIngredient
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public IngredientDto IngredientDto { get; set; } = new();

        private MudForm? form;
        private bool isSaving = false;

        private async Task Save()
        {
            if (form is null)
                return;

            await form.Validate();

            if (form.IsValid)
            {
                isSaving = true;
                MudDialog?.Close(DialogResult.Ok(IngredientDto));
            }
        }

        private void Cancel() => MudDialog?.Cancel();
    }
}
