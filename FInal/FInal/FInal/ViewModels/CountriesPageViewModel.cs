using FInal.Common.Models;
using FInal.Common.Responses;
using FInal.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace FInal.ViewModels
{
    public class CountriesPageViewModel : ViewModelBase
    {
        private FacebookProfile _facebook { get; set; }
        private readonly INavigationService _navigationService;
        private bool _isEnabled;
        private readonly IApiService _apiService;
        private string _Name;
        private string _LastName;
        private string _Email;
        private string _Picture;
        public CountriesPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Countries";
            IsEnabled = true;
        }

        public string FirstName
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }
        public string LastName
        {
            get => _LastName;
            set => SetProperty(ref _LastName, value);
        }
        public string Email
        {
            get => _Email;
            set => SetProperty(ref _Email, value);
        }
        public string Picture
        {
            get => _Picture;
            set => SetProperty(ref _Picture, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            if (parameters.ContainsKey("facebookProfile"))
            {
                var response = parameters["facebookProfile"];


                _facebook = (FacebookProfile)response;


                LoadFacebookData();
            }
        }

        private void LoadFacebookData()
        {
            try
            {
                FirstName = _facebook.FirstName;
                LastName = _facebook.LastName;
                Email = _facebook.Email;
                Picture = _facebook.Picture.Data.Url;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }


       
    }
}
