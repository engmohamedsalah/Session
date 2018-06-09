using Session.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Session.Repository
{
    /// <summary>
    /// SessionTopic Repository resposible for the data 
    ///of table SessionTopic (relation between topic and session class)
    /// </summary>
    public class SessionTopicRepository : GenericRepository<SessionTopic>,ISessionTopicRepository
    {
        public SessionTopicRepository(DbContext context): base(context)
        {

        }
        /// <summary>
        /// return all records
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<SessionTopic> GetAll()
        {
            return _entities.Set<SessionTopic>().AsEnumerable();
        }

        /// <summary>
        /// return record that match id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SessionTopic GetById(int id)
        {
            return _dbset.FirstOrDefault();
        }
    }
}
