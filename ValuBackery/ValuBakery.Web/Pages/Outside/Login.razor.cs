using Microsoft.AspNetCore.Components;
using MudBlazor;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.DTOs.Authorization;
using ValuBakery.Shared.Constants;
using ValuBakery.Web.Data;

namespace ValuBakery.Web.Pages.Outside
{
    public partial class Login
    {
        #region Properties
        private LoginModel _authModel = new();

        [Inject] protected ISnackbar Snackbar { get; set; }
        #endregion

        #region Component Methods
        protected override async Task OnInitializedAsync()
        {
        }

        private async Task SubmitAsync()
        {
            if (!ValidateLogin())
                return;

            var request = new LoginDto
            {
                UserName = _authModel.UserName,
                Password = _authModel.Password,
            };

            var result = await _authService.LoginProccess(request);

            if(result != null)
            {
                var token = result.Token;
                var refreshToken = result.RefreshToken;

                await _localStorage.SetItemAsync(StorageConstants.Local.UserName, _authModel.UserName);
                await _localStorage.SetItemAsync(StorageConstants.Local.UserId, result.UserId);
                await _localStorage.SetItemAsync(StorageConstants.Local.AuthToken, token);
                await _localStorage.SetItemAsync(StorageConstants.Local.RefreshToken, refreshToken);

                _navigationManager.NavigateTo("/");

                Snackbar.Add(string.Format("Bienvenido" + " {0}", request.UserName), Severity.Normal);
            }
            else
            {
                Snackbar.Add("Usuario o contraseña incorrectos");
            }
            //foreach (var error in response.Errors)
            //    Snackbar.Add(error.Message, Severity.Error);
        }

        private bool ValidateLogin()
        {
            List<string> errors = new List<string>();

            if (String.IsNullOrEmpty(_authModel.UserName))
                errors.Add("Ingresar nombre de usuario");

            if (String.IsNullOrEmpty(_authModel.Password))
                errors.Add("Ingresar contraseña");

            if (errors.Count > 0)
            {
                foreach (var error in errors)
                    Snackbar.Add(error, Severity.Error);

                return false;
            }

            return true;
        }
        #endregion

        #region PasswordVisibility

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
        #endregion
    }
}
