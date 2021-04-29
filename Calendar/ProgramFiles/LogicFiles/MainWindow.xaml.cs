using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Drawing;


namespace Calendar {
    // Interaction logic for MainWindow.xaml.
    public partial class MainWindow : Window {
        // WebClient for the weather in the main window.
        public WebClient webClient_ = new WebClient();
        // Object of the class handling the collection of events and their priority types.
        public EventsCollections collections_ = new EventsCollections();

        // Use of a class constructor. Setting the central position of the main window, setting the current date on the calendar. Adding DataContext to data collection.
        public MainWindow() {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            GetProperlyDirectory();
            if (CalendarContol.SelectedDate == null) {
                CalendarContol.SelectedDate = DateTime.Now;
            }

            DataContext = collections_ ;
        }
        // Retrieving records from Database via Entity Framework by pressing the button.
        private void GetProperlyDirectory() {
            string directoryPath = Environment.CurrentDirectory;
            string[] appPath = directoryPath.Split(new string[] { "bin" }, StringSplitOptions.None);
            AppDomain.CurrentDomain.SetData("DataDirectory", appPath[0]);
        }

        // Button to display the weather in the main window.
        private void WeatherButton(object sender, RoutedEventArgs eventArgs) {
            GetWeather(webClient_);
        }
        // Downloading data using the API from the OpenWeather website. Using JsonConvert to convert between. JSON and .Net
        void GetWeather(WebClient webClient) {
            // An object of class CityNameHandler with a method for validating the query sent to the server.
            CityNameHandler cityNameHandler = new CityNameHandler();
            // Getting the city name from the user.
            string cityName = cityTextBox.Text.ToString();
            // Api key assignment.
            string apiKey = "8b7c4545e3874014261a301b43f9d144";
            // Assign a suitable url for the selected city with apikey.
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?appid={0}&q={1}", apiKey, cityName);
            // Handling exceptions caused by an invalid city name.
            if (cityNameHandler.checkUrl(url)) {
                MessageBox.Show("Niepoprawna nazwa miasta.");
            } else {
                var json = webClient.DownloadString(url);
                // Using JsonConvert.
                var jsonConverted = JsonConvert.DeserializeObject<weatherinfo.root>(json);
                weatherinfo.root weatherData = jsonConverted;
                // Variable used to convert Kelvin to Celsius.
                int KelvinToCelsius = 273;
                // Variable used for temperature rounding.
                int precision = 2;
                // Assigning appropriate values ​​to variables.
                string temperature = string.Format("{0}", Math.Round(weatherData.main.temp - KelvinToCelsius, precision));
                string humidity = string.Format("{0}", weatherData.main.humidity);
                string pressure = string.Format("{0}", weatherData.main.pressure);
                string windSpeed = string.Format("{0}", weatherData.wind.speed);
                // Display of weather data in the appropriate TextBoxes.
                temperatureTextBox.Text = temperature + " °C";
                humidityTextBox.Text = humidity + " %";
                pressureTextBox.Text = pressure + " hPa";
                windSpeedTextBox.Text = windSpeed + " m/s";
                // Displaying the weather image using the API in the main window.
                string imageId = string.Format("{0}", weatherData.weather[0].icon);
                string imageUrl = string.Format("https://openweathermap.org/img/wn/{0}@2x.png", imageId);
                var bitMapWeather = new BitmapImage();
                bitMapWeather.BeginInit();
                bitMapWeather.UriSource = new Uri(imageUrl);
                bitMapWeather.EndInit();
                weatherLogo.Source = bitMapWeather;
            }
        }
        // Button that opens a new window with the weather forecast.
        private void ForecastButton(object sender, RoutedEventArgs eventArgs) {
            // Getting the city name from the user.
            string cityName = cityTextBox.Text.ToString();
            // Create a new window.
            weatherForecast showWeather = new weatherForecast(cityName);
            showWeather.Show();
        }
        // Uploading the current data to the event collection.
        public void RefreshEventList(DateTime? date) {
            collections_.ClearDailyEvents();
            BazaDanychEntities context = new BazaDanychEntities();
            var eventsUpdated = context.Events.Where(updateEvents => updateEvents.StartDate == date); ;
            collections_.FillDailyEvents(eventsUpdated);
        }
        // Reaction to changes in the calendar date. Shows the relevant events
        public void EventsListBoxSelectionChanged(object sender, SelectionChangedEventArgs eventArgs) {
            var calendar = sender as System.Windows.Controls.Calendar;
            RefreshEventList(calendar.SelectedDate);
        }
        // Button that add new event.
        private void AddEventButton(object sender, RoutedEventArgs eventArgs) {
            (new AddEvent(CalendarContol.SelectedDate, collections_, CalendarContol, RefreshEventList)).Show();
        }
        // Button deleting the selected event.
        private void DeleteEventButton(object sender, RoutedEventArgs eventArgs) {
            (new DeleteEvent()).Show();
        }
        // Menu button for adding and removing events
        private void MenuItemButton(object sender, RoutedEventArgs eventArgs) {}

        private void CityTextBoxTextChanged(object sender, TextChangedEventArgs eventArgs) {}
    }
}