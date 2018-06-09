namespace Session.Model
{
    public abstract class BaseEntity
    {

    }
    //abstract class for every entity
    // that containt the ID as primary key
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
