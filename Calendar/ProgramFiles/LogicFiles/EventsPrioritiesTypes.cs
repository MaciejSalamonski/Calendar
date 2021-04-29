namespace Calendar
{
    using System;
    using System.Collections.Generic;

    // Interaction logic for EventTypes.
    public partial class EventsTypes {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventsTypes() {
            Events = new HashSet<Events>();
        }
    
        public int Id { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Events> Events { get; set; }
    }
}
