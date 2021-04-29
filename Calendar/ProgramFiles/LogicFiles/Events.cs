namespace Calendar {
    using System;
    using System.Collections.Generic;

    // Interaction logic for Events.
    public partial class Events {
        public int Id { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Name { get; set; }
        public int TypeID { get; set; }
        public string Note { get; set; }
        public virtual EventsTypes EventsTypes { get; set; }
    }
}
