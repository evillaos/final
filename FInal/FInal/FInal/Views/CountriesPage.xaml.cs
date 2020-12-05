


using FInal.Common.Models;
using FInal.Common.Responses;
using FInal.Common.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = Xamarin.Forms.Maps.Position;

namespace FInal.Views
{
    public partial class CountriesPage : ContentPage
    {
        private readonly IGeolocatorService _geolocatorService;
        private readonly IApiService _apiService;

        public CountriesPage(IGeolocatorService geolocatorService, IApiService apiService)
        {
            InitializeComponent();
            _geolocatorService = geolocatorService;
            _apiService = apiService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MoveMapToCurrentPositionAsync();
            LoadCountriesAsync();
        }

        private async void LoadCountriesAsync()
        {
            IsEnabled = false;
        
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "connection error", "Aceptar");
                return;
            }
        
        
            string url = "https://restcountries.eu/";
            Response response = await _apiService.GetListAsync<Country>(url, "rest/v2/all");
            IsEnabled = true;
        
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
        
            //List<Country> list = (List<Country>)response.Result;
            //foreach (Country country in list)
            //{
            //    MyMap.Pins.Add(new Pin
            //    {
            //        Address = country.Region,
            //        Label = country.Name,
            //        Position = new Position(country.Latlng[0], country.Latlng[1]),
            //        Type = PinType.Place
            //    });
            //}
        }

        private async void MoveMapToCurrentPositionAsync()
        {
            bool isLocationPermision = await CheckLocationPermisionsAsync();

            if (isLocationPermision)
            {
                MyMap.IsShowingUser = true;

                await _geolocatorService.GetLocationAsync();
                if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
                {
                    Position position = new Position(
                        _geolocatorService.Latitude,
                        _geolocatorService.Longitude);
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                        position,
                        Distance.FromKilometers(.5)));
                }
            }
        }

        private async Task<bool> CheckLocationPermisionsAsync()
        {
            Plugin.Permissions.Abstractions.PermissionStatus permissionLocation = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            Plugin.Permissions.Abstractions.PermissionStatus permissionLocationAlways = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            Plugin.Permissions.Abstractions.PermissionStatus permissionLocationWhenInUse = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            bool isLocationEnabled = permissionLocation == Plugin.Permissions.Abstractions.PermissionStatus.Granted ||
                                        permissionLocationAlways == Plugin.Permissions.Abstractions.PermissionStatus.Granted ||
                                        permissionLocationWhenInUse == Plugin.Permissions.Abstractions.PermissionStatus.Granted;
            if (isLocationEnabled)
            {
                return true;
            }

            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

            permissionLocation = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            permissionLocationAlways = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            permissionLocationWhenInUse = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            return permissionLocation == Plugin.Permissions.Abstractions.PermissionStatus.Granted ||
                    permissionLocationAlways == Plugin.Permissions.Abstractions.PermissionStatus.Granted ||
                    permissionLocationWhenInUse == Plugin.Permissions.Abstractions.PermissionStatus.Granted;
        }
    }


}

