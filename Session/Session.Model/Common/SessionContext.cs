using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;

/// <summary>
/// this class in the Context contians the DbSet of tables that will be used and
/// Also common behaviour at saving on database to automatic save the creation data
/// or edit data
/// </summary>
namespace Session.Model
{
    public class SessionContext : DbContext
    {
       
      
        //Contais Room names {Hall, Conference Court, Room C, …}
        public DbSet<SessionRoom> SessionRooms { get; set; }
        //Contais topics names {Oracel, WebApi, ...}
        public DbSet<SessionTopic> SessionTopics { get; set; }
        //Contains the session data { Topic , Room , from data , to date}
        public DbSet<SessionClass> SessionClasses { get; set; }
        //Contains the data between the attendee and classes
        public DbSet<SessionAttendee> SessionAttendees { get; set; }
        //Key value table contains the dynamic data for full attendee details 
        public DbSet<AttendeeFullData> AttendeeFullData { get; set; }

        public SessionContext()
        : base("SessionContext")
        {

        }
        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// and save the tracking data in creation and edit  columns( 4 columns)
        /// </summary>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        ///             state entries for entities and/or relationships. Relationship state
        /// entries are created for 
        ///             many-to-many relationships and relationships where there is no
        /// foreign key property
        ///             included in the entity class (often referred to as independent
        /// associations).
        /// </returns>
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }


    }
}

