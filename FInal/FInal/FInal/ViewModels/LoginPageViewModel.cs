using FInal.Common.Models;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FInal.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private bool _isEnabled;
        private DelegateCommand _loginFacebookCommand;
        private readonly IFacebookClient _facebookService = CrossFacebookClient.Current;
        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Login";
            IsEnabled = true;
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public DelegateCommand LoginFacebookCommand => _loginFacebookCommand ?? (_loginFacebookCommand = new DelegateCommand(LoginFacebookAsync));

        private async void LoginFacebookAsync()
        {
            await App.Current.MainPage.DisplayAlert("Error", "Funciona", "Aceptar");
            try
            {

                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                async void userDataDelegate(object sender, FBEventArgs<string> e)
                {
                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            FacebookProfile facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                            await LoginFacebookAsync(facebookProfile);
                            break;
                        case FacebookActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Canceled", "Ok");
                            break;
                        case FacebookActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Error", "Ok");
                            break;
                        case FacebookActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _facebookService.OnUserData -= userDataDelegate;
                }

                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "picture.width(999)", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async Task LoginFacebookAsync(FacebookProfile facebookProfile)
        {
            IsEnabled = false;

            await App.Current.MainPage.DisplayAlert("Error", "Pasa Login", "Aceptar");

            //await _navigationService.NavigateAsync($"/{nameof(OnSaleMasterDetailPage)}/NavigationPage/{nameof(ProductsPage)}");

        }

    }
}
