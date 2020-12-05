using FInal.Common.Services;
using Xamarin.Forms;

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



    }
}
