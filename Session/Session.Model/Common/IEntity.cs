/// <summary>
/// Interface of any Entity 
/// with ID column as primary key
/// ID can take any data type { int , string , ....}
/// so that all tables will have column ID as primary key with different type of ID
/// </summary>
namespace Session.Model
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
