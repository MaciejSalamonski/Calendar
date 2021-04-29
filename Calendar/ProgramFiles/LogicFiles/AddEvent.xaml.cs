using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

// Interaction logic for AddEvent.xaml.
namespace Calendar {
    public partial class AddEvent : Window {
        CalendarEvent calendarEvent_;
        PriorityProperties priorityProperties_;
        EventProperties eventProperties_;
        System.Windows.Controls.Calendar calendar_;
        ObservableCollection<PriorityProperties> priorities_;
        Action<DateTime?> refresh_;

        public AddEvent(DateTime? selectedDate, EventsCollections collections, System.Windows.Controls.Calendar calendar, Action<DateTime?> refresh) {
            priorityProperties_ = new PriorityProperties("", "", "", 0);
            eventProperties_ = new EventProperties("", new DateTime(), new DateTime(), "", 0);
            calendarEvent_ = new CalendarEvent(priorityProperties_, eventProperties_);
            DataContext = calendarEvent_;
            calendar_ = calendar;
            InitializeComponent();
            startpicker.SelectedDate = selectedDate;
            endpicker.SelectedDate = selectedDate;
            refresh_ = refresh;
            priorities_ = new ObservableCollection<PriorityProperties>();
        }
        // Date parser to correctly enter data to display the start and end times of the event.
        private DateTime AddTime(string input, DateTime? date) {
            int hours = 0;
            int minutes = 0;

            if (input[2] == ':') {
                hours = Int32.Parse(input.Substring(0, 2));
                string minutes_ = input.Substring(3, input.Length - 3);
                minutes = Int32.Parse(minutes_);
            } else if (input[1] == ':') {
                hours = Int32.Parse(input.Substring(0, 1));
                minutes = Int32.Parse(input.Substring(2, input.Length-2));
            }
            // Add to default date: hours and minutes.
            DateTime dateTime = new DateTime(eventProperties_.TimeStart.Ticks);
            dateTime = eventProperties_.TimeStart.AddHours(hours);
            dateTime = eventProperties_.TimeStart.AddMinutes(minutes);

            return dateTime;
        }

        // The button responsible for adding an event.
        private void ButtonClick(object sender, RoutedEventArgs eventArgs) {
            if (prioritesComboBox.SelectedItem != null) {
                var type = prioritesComboBox.SelectedItem as PriorityProperties;
                BazaDanychEntities context = new BazaDanychEntities();
                // Boolean variable that indicates if a refresh is needed.
                bool refreshRequired = calendar_.SelectedDate == calendarEvent_.eventProperties.TimeStart;
                // Adding an event.
                context.Events.Add(new Events() { 
                    Name = calendarEvent_.eventProperties.Name, 
                    StartDate = AddTime(startTime.Text, eventProperties_.TimeStart), 
                    EndDate = AddTime(endTime.Text, eventProperties_.TimeEnd), 
                    TypeID = type.Id, Note = "Uwagi: " + calendarEvent_.eventProperties.Note });
                // Save changes.
                context.SaveChanges();
                if (refreshRequired) {
                    refresh_(calendar_.SelectedDate);
                }
            }
        }
        // Add an event priority.
        void AddPriority(EventsTypes priority) {
            priorities_.Add(new PriorityProperties(priority.Color1, priority.Color2, priority.Name, priority.Id));
        }
        // Method responsible for combobox menu opening
        private void ComboBoxContextMenuOpening(object sender, ContextMenuEventArgs eventArgs) {
            BazaDanychEntities context = new BazaDanychEntities();
            var comboBoxPriorities = context.EventsTypes;
            comboBoxPriorities.ToList().ForEach(AddPriority);
        }
        // Method responsible for the correct opening of the combobox
        private void ComboBoxDropDownOpened(object sender, EventArgs eventArgs) {
            priorities_.Clear();
            BazaDanychEntities context = new BazaDanychEntities();
            var comboBoxPriorities = context.EventsTypes;
            comboBoxPriorities.ToList().ForEach(AddPriority);
            prioritesComboBox.ItemsSource = priorities_;
        }
    }
}
