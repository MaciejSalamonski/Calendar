namespace Calendar {

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

// Interaction logic for DataBase.
public partial class BazaDanychEntities : DbContext {
    public BazaDanychEntities()
        : base("name=BazaDanychEntities") {}

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        throw new UnintentionalCodeFirstException();
    }

    public virtual DbSet<Events> Events { get; set; }

    public virtual DbSet<EventsTypes> EventsTypes { get; set; }
}

}

