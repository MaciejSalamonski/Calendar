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

namespace Calendar {
    // Interaction logic for the DeleteEvent.xaml class
    public partial class DeleteEvent : Window {
        // For a class containing both the collection of event types and the event itself, the collection of event types was not needed so far.
        EventsCollections collections = new EventsCollections();
        // Method that refreshes the list of events for the day.
        public void RefreshEventList(DateTime? date) {
            collections.ClearDailyEvents();
            BazaDanychEntities context = new BazaDanychEntities();

            var eventsUpdated = context.Events.Where(updateEvents => updateEvents.StartDate == date);
            collections.FillDailyEvents(eventsUpdated);
        }
        // Constructor of the event delete window. The collections are added to the DataContext.
        public DeleteEvent() {
            InitializeComponent();
            DataContext = collections;
        }
        // Reaction to changing the selected date. Refreshing the list of events/
        private void StartpickerSelectedDateChanged(object sender, SelectionChangedEventArgs eventsArgs) {
            var datePicker = sender as System.Windows.Controls.DatePicker;
            RefreshEventList(datePicker.SelectedDate);
        }
        //Response to clicking the delete button.Deletion only takes place when an event is selected.
        //The event is removed by querying it by ID. After deletion, the previous event is selected.
        private void DeleteClick(object sender, RoutedEventArgs eventsArgs) {
            if (EventsListBox.SelectedItem != null) {
                int index = EventsListBox.SelectedIndex;
                CalendarEvent selectedItem = (CalendarEvent)EventsListBox.SelectedItem;
                BazaDanychEntities context = new BazaDanychEntities();

                const string query = "DELETE FROM [dbo].[Events] WHERE [id]={0}";
                context.Database.ExecuteSqlCommand(query, selectedItem.eventProperties.ID);
                context.SaveChanges();
                RefreshEventList(startPicker.SelectedDate);
                if (index > 0) {
                    EventsListBox.SelectedItem = EventsListBox.Items[index - 1];
                }
            }
        }
    }
}
