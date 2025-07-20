using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Web.Pages.Ingredients;
using ValuBakery.Web.Pages.Recipes.Mobile;

namespace ValuBakery.Web.Pages.Recipes
{
    public class RecipesBase : ComponentBase
    {
        protected List<string> editEvents = new();
        protected string searchString = "";
        protected RecipeDto selectedItem = null;
        protected RecipeDto RecipeDtoBeforeEdit;
        protected HashSet<RecipeDto> selectedItems = new HashSet<RecipeDto>();
        protected List<RecipeDto> RecipeDtos = new List<RecipeDto>();
        protected bool isLoading;
        [Inject] protected IRecipeService _recipeService { get; set; }
        [Inject] protected IDialogService _dialogService { get; set; }
        [Inject] protected ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isLoading= true;
            RecipeDtos = await _recipeService.GetAllAsync();
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
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            _dialogService.Show<CreateRecipe>("CreateRecipe", parameters, options);
        }

        protected async Task CreateDialogEvent(int id)
        {
            try
            {
                var entity = await _recipeService.GetByIdAsync(id);

                if (entity != null)
                {
                    var item = entity;

                    if (item != null)
                    {
                        RecipeDtos.Insert(0, item);
                        ViewRecipe(item.Id,false);
                    }

                    StateHasChanged();                    
                }
            }
            catch
            {
                throw;
            }
        }

        protected void ViewRecipe(int id, bool isMobile)
        {
            var parameters = new DialogParameters
            {
                { isMobile ? nameof(Recipe.Id) : nameof(RecipeMobile.Id), id },
                { isMobile ? nameof(Recipe.OnEditData) : nameof(RecipeMobile.OnEditData),
                EventCallback.Factory.Create<RecipeDto>(this, EditDialogEvent)  }

            };

            var options = new DialogOptions
                    {
                FullWidth = true,
                MaxWidth = MaxWidth.Medium,
                DisableBackdropClick = false,
                CloseOnEscapeKey= true,
                Position = DialogPosition.Center,
                NoHeader = false,
                CloseButton = true
            };

            if (isMobile)
            {

                _dialogService.Show<RecipeMobile>("", parameters, options);
            }
            else
            {
                _dialogService.Show<Recipe>("", parameters, options);
            }
        }

        private void EditDialogEvent(RecipeDto recipeDto)
        {
            var index = RecipeDtos.FindIndex(x => x.Id == recipeDto.Id);
            if (index != -1)
            {
                if (recipeDto.IsDeleted)
                {
                    RecipeDtos.RemoveAt(index); 
                }
                else
                {
                    RecipeDtos[index] = recipeDto;
                }

                StateHasChanged();
            }
        }

        #endregion
    }
}
