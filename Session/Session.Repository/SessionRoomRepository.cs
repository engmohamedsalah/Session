using Session.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Repository
{ /// <summary>
  /// Repository of SessionRoom table 
  /// resposible for return the  data of the Session and Room
  /// </summary>
  /// <remarks></remarks>
    public class SessionRoomRepository : GenericRepository<SessionRoom>, ISessionRoomRepository
    {
        public SessionRoomRepository(DbContext context): base(context)
        {

        }
        /// <summary>
        /// return all records
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<SessionRoom> GetAll()
        {
            return _entities.Set<SessionRoom>().AsEnumerable();
        }

        /// <summary>
        /// return record that match id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SessionRoom GetById(int id)
        {
            return _dbset.FirstOrDefault();
        }
    }
}
