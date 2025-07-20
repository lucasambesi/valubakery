using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Recipes
{
    public partial class EditRecipe
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        public CreateRecipeModel CreateRecipeModel { get; set; } = new CreateRecipeModel();

        [Parameter]
        public RecipeDto? RecipeDto { get; set; }

        private void Cancel() => MudDialog.Cancel();

      

        [Parameter]
        public EventCallback<RecipeDto> OnCreateData { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CreateRecipeModel.Name = RecipeDto.Name;
            CreateRecipeModel.Description = RecipeDto.Description;
            CreateRecipeModel.IsActive = !RecipeDto.IsDeleted;
        }

        private async Task Submit()
        {
            // Normalizar entradas
            CreateRecipeModel.Name = CreateRecipeModel.Name?.Trim();
            CreateRecipeModel.Description = CreateRecipeModel.Description?.Trim();

            if (string.IsNullOrWhiteSpace(CreateRecipeModel.Name))
            {
                _snackbar.Add("El nombre es obligatorio", Severity.Warning);
                return;
            }

            try
            {
                var recipeDto = new RecipeDto
                {
                    Id = RecipeDto.Id,
                    Name = CreateRecipeModel.Name,
                    Description = CreateRecipeModel.Description,
                    IsDeleted = !CreateRecipeModel.IsActive,
                };

                var sucess = await _recipeService.UpdateAsync(recipeDto);
                if (sucess)
                {
                    _snackbar.Add("Receta editada", Severity.Success);
                    MudDialog?.Close(DialogResult.Ok(true));
                    await OnCreateData.InvokeAsync(recipeDto);
                }
                else
                {
                    _snackbar.Add("Error al crear", Severity.Error);
                }
            }
            catch (Exception)
            {
                _snackbar.Add("Ocurrió un error inesperado", Severity.Error);
                throw;
            }
        }

        private void CapitalizeName()
        {
            if (!string.IsNullOrWhiteSpace(CreateRecipeModel.Name))
            {
                var name = CreateRecipeModel.Name.Trim();
                CreateRecipeModel.Name = char.ToUpper(name[0]) + name[1..];
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
