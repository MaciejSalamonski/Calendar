using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Calendar {
    // The class contains the definitions of the event. It consists of two parts:
    // Individual features of the event and features resulting from the priority type.
    public class CalendarEvent {
        private PriorityProperties priorityProperties_;
        private EventProperties eventProperties_;
        public PriorityProperties priorityProperties { get { return priorityProperties_; } set { priorityProperties_ = value; } }
        public EventProperties eventProperties { get { return eventProperties_; } set { eventProperties_ = value; } }
        public CalendarEvent(PriorityProperties priorityProper, EventProperties eventProper) {
            priorityProperties = priorityProper;
            eventProperties = eventProper;
        }
    }
    // Priority type components: colors and name of the priority type.
    // The class implements INotifyPropertyChanged because it works with the GUI
    public class PriorityProperties : INotifyPropertyChanged // class object is created on the basis of priority types.The events are user-defined 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string colorOnCalendar_;
        private string colorOnList_;
        private string priorityName_;
        private int id_;

        public String ColorOnCalendar { get { return colorOnCalendar_; } set { colorOnCalendar_ = value; OnPropertyChanged(); } }
        public String ColorOnList { get { return colorOnList_; } set { colorOnList_ = value; OnPropertyChanged(); } }
        public String PriorityName { get { return priorityName_; } set { priorityName_ = value; OnPropertyChanged(); } }
        public int Id { get { return id_; } set { id_ = value; OnPropertyChanged(); } }

        public PriorityProperties(string colorOnCalendar, string colorOnList, string priorityName, int id) {
            ColorOnCalendar = colorOnCalendar;
            ColorOnList = colorOnList;
            PriorityName = priorityName;
            Id = id;
        }
    }
    
    // Individual components: name, start and end time of the event and note to the event.
    // The class implements INotifyPropertyChanged because it works with the GUI
    public class EventProperties : INotifyPropertyChanged // class object is created on the basis of priority types.The events are user-defined 
    {    
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Data retrieved from the database
        private string name_;
        private DateTime timeStart_;
        private DateTime timeEnd_;
        private string note_;
        private int id_;
        public String Name { get { return name_; } set { name_ = value; OnPropertyChanged(); } }
        public DateTime TimeStart { get { return timeStart_; } set { timeStart_ = value; OnPropertyChanged(); } }
        public DateTime TimeEnd { get { return timeEnd_; } set { timeEnd_ = value; OnPropertyChanged(); } }
        public String Note { get { return note_; } set { note_ = value; OnPropertyChanged(); } }
        public int ID { get { return id_; } private set { id_ = value; OnPropertyChanged(); } }
        public EventProperties(string name, DateTime start, DateTime end, string note, int id) {
            Name = name;
            TimeStart = start;
            TimeEnd = end;
            Note = note;
            ID = id;
        }
    }

    // A class that handles collections of events and event priorities types
    public class EventsCollections {
        private ObservableCollection<CalendarEvent> DailyEventList_ = new ObservableCollection<CalendarEvent>();
        private ObservableCollection<PriorityProperties> PrioritesList_ = new ObservableCollection<PriorityProperties>();

        public ObservableCollection<CalendarEvent> DailyEventList { get { return DailyEventList_; } set { value = DailyEventList_; }}
        public ObservableCollection<PriorityProperties> PrioritesList { get { return PrioritesList_; } set { value = PrioritesList_; } }
        // The method populates the event collection with data from the database.
        public void FillDailyEvents(IQueryable<Events> events) {
            events.ToList().ForEach(AddDailyEvent);
        }
        // Adds an event to the collection.
        public void AddDailyEvent(Events newEvent) {
            PriorityProperties priorityProper = new PriorityProperties(newEvent.EventsTypes.Color1, newEvent.EventsTypes.Color2, newEvent.EventsTypes.Name, 0);
            EventProperties eventProper = new EventProperties(newEvent.Name, newEvent.StartDate, newEvent.EndDate, newEvent.Note, newEvent.Id);
           
            DailyEventList.Add(new CalendarEvent(priorityProper, eventProper));
        }
        // The method clears the list of events
        public void ClearDailyEvents() {
            DailyEventList.Clear();
        }
    }

}
