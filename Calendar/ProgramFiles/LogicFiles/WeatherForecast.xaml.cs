using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

// Interaction logic for weatherForecast.xaml.

namespace Calendar {
    public partial class weatherForecast : Window {
        // WebClient for weather forecast.
        public WebClient webClient_ = new WebClient();
        // Variable used to get the city name from the main project window.
        public string cityName { get; set; }
        // Variable used to change the displayed weather tables in the weather forecast window.
        public int nextWeatherData { get; set; }
        // Setting class variables using constructor
        public weatherForecast(string inputCityName) {
            // Assigning values ​​to class variables
            cityName = inputCityName;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            nextWeatherData = 1;

            InitializeComponent();
            // Closing the main window.
            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow != null) {
                mainWindow.Closed += (sender, eventArgs) => Close();
            }
            // Running a method that downloads the weather forecast.
            GetForecast(cityName, webClient_, nextWeatherData);
        }
        // Downloading data using the API from the OpenWeather website. Using JsonConvert to convert between JSON and .Net
        void GetForecast(string cityName, WebClient webClient, int nextWeatherData) {
                // Api key assignment
                string apiKey = "8b7c4545e3874014261a301b43f9d144";
                // Assign a suitable url for the selected city with apikey
                string url = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&appid={1}", cityName, apiKey);
                // An object of class CityNameHandler with a method for validating the query sent to the server.
                CityNameHandler isCityNameValid = new CityNameHandler();
                // Handling exceptions caused by an invalid city name
                if (isCityNameValid.checkUrl(url)) {
                    MessageBox.Show("Niepoprawna nazwa miasta.\n");
                    cityTextBox.Text = "Błąd: Nieznaleziono miasta";
                } else {
                    cityTextBox.Text = cityName;
                    var json = webClient.DownloadString(url);
                    // Using JsonConvert
                    var jsonConverted = JsonConvert.DeserializeObject<weatherforecast.RootObject>(json);
                    weatherforecast.RootObject weatherData = jsonConverted;

                    // Get the weather forecast data for a specific day by referring to the corresponding element in the list.
                    forecastZeroRowFirstElementTextBox.Text = "Data:";              
                    forecastZeroRowSecondElementTextBox.Text = "Temperatura:";
                    forecastZeroRowThirdElementTextBox.Text = "Wiatr:";
                    forecastZeroRowFourthElementTextBox.Text = "Ciśnienie:";
                    forecastZeroRowFifthElementTextBox.Text = "Wilgotność:";
                    // Variable used to convert Kelvin to Celsius
                    int KelvinToCelsius = 273;
                    // Variable used for temperature rounding
                    int precision = 2;

                    // Filling the next columns and rows in the weather forecast table with data
                    string forecastDate = weatherData.list[nextWeatherData].dt_txt;
                    string forecastTemperature = string.Format("{0}", Math.Round(weatherData.list[nextWeatherData].main.temp - KelvinToCelsius, precision));
                    string forecastPressure = string.Format("{0}", weatherData.list[nextWeatherData].main.pressure);
                    string forecastWind = string.Format("{0}", weatherData.list[nextWeatherData].wind.speed);
                    string forecastHumidity = string.Format("{0}", weatherData.list[nextWeatherData].main.humidity);
                    forecastFirstRowFirstElementTextBox.Text = forecastDate;
                    forecastFirstRowSecondElementTextBox.Text = forecastTemperature + " °C";
                    forecastFirstRowThirdElementTextBox.Text = forecastWind + " m/s";
                    forecastFirstRowFourthElementTextBox.Text = forecastPressure + " hPa";
                    forecastFirstRowFifthElementTextBox.Text = forecastHumidity + " %";
                    ++nextWeatherData;

                    forecastDate = weatherData.list[nextWeatherData].dt_txt;
                    forecastTemperature = string.Format("{0}", Math.Round(weatherData.list[nextWeatherData].main.temp - KelvinToCelsius, precision));
                    forecastPressure = string.Format("{0}", weatherData.list[nextWeatherData].main.pressure);
                    forecastWind = string.Format("{0}", weatherData.list[nextWeatherData].wind.speed);
                    forecastHumidity = string.Format("{0}", weatherData.list[nextWeatherData].main.humidity);
                    forecastSecondRowFirstElementTextBox.Text = forecastDate;
                    forecastSecondRowSecondElementTextBox.Text = forecastTemperature + " °C";
                    forecastSecondRowThirdElementTextBox.Text = forecastWind + " m/s";
                    forecastSecondRowFourthElementTextBox.Text = forecastPressure + " hPa";
                    forecastSecondRowFifthElementTextBox.Text = forecastHumidity + " %";
                    ++nextWeatherData;

                    forecastDate = weatherData.list[nextWeatherData].dt_txt;
                    forecastTemperature = string.Format("{0}", Math.Round(weatherData.list[nextWeatherData].main.temp - KelvinToCelsius, precision));
                    forecastPressure = string.Format("{0}", weatherData.list[nextWeatherData].main.pressure);
                    forecastWind = string.Format("{0}", weatherData.list[nextWeatherData].wind.speed);
                    forecastHumidity = string.Format("{0}", weatherData.list[nextWeatherData].main.humidity);
                    forecastThirdRowFirstElementTextBox.Text = forecastDate;
                    forecastThirdRowSecondElementTextBox.Text = forecastTemperature + " °C";
                    forecastThirdRowThirdElementTextBox.Text = forecastWind + " m/s";
                    forecastThirdRowFourthElementTextBox.Text = forecastPressure + " hPa";
                    forecastThirdRowFifthElementTextBox.Text = forecastHumidity + " %";
                    ++nextWeatherData;

                    forecastDate = weatherData.list[nextWeatherData].dt_txt;
                    forecastTemperature = string.Format("{0}", Math.Round(weatherData.list[nextWeatherData].main.temp - KelvinToCelsius, precision));
                    forecastPressure = string.Format("{0}", weatherData.list[nextWeatherData].main.pressure);
                    forecastWind = string.Format("{0}", weatherData.list[nextWeatherData].wind.speed);
                    forecastHumidity = string.Format("{0}", weatherData.list[nextWeatherData].main.humidity);
                    forecastFourthRowFirstElementTextBox.Text = forecastDate;
                    forecastFourthRowSecondElementTextBox.Text = forecastTemperature + " °C";
                    forecastFourthRowThirdElementTextBox.Text = forecastWind + " m/s";
                    forecastFourthRowFourthElementTextBox.Text = forecastPressure + " hPa";
                    forecastFourthRowFifthElementTextBox.Text = forecastHumidity + " %";
                    ++nextWeatherData;

                    forecastDate = weatherData.list[nextWeatherData].dt_txt;
                    forecastTemperature = string.Format("{0}", Math.Round(weatherData.list[nextWeatherData].main.temp - KelvinToCelsius, precision));
                    forecastPressure = string.Format("{0}", weatherData.list[nextWeatherData].main.pressure);
                    forecastWind = string.Format("{0}", weatherData.list[nextWeatherData].wind.speed);
                    forecastHumidity = string.Format("{0}", weatherData.list[nextWeatherData].main.humidity);
                    forecastFifthRowFirstElementTextBox.Text = forecastDate;
                    forecastFifthRowSecondElementTextBox.Text = forecastTemperature + " °C";
                    forecastFifthRowThirdElementTextBox.Text = forecastWind + " m/s";
                    forecastFifthRowFourthElementTextBox.Text = forecastPressure + " hPa";
                    forecastFifthRowFifthElementTextBox.Text = forecastHumidity + " %";
                    ++nextWeatherData;
                }
            
        }
        // Method displaying consecutive weather forecast dates in the current window
        private void NextWeatherButton(object sender, RoutedEventArgs eventArgs) {
            int weatherDataIncrement = 5;
            int repeatWeatherForevast = 35;
            nextWeatherData += weatherDataIncrement;
            nextWeatherData %= repeatWeatherForevast;

            GetForecast(cityName, webClient_, nextWeatherData);
        }
    }
}
