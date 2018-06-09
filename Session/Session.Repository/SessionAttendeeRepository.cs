using Session.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Repository
{
    /// <summary>
    /// Repository of SessionAttendee table 
    /// resposible for return  data of the attendee's sessions
    /// </summary>
    /// <remarks></remarks>
    public class SessionAttendeeRepository : GenericRepository<SessionAttendee>, ISessionAttendeeRepository
    {
        public SessionAttendeeRepository(DbContext context) : base(context)
        {

        }
        public override IEnumerable<SessionAttendee> GetAll()
        {
            return _entities.Set<SessionAttendee>().AsEnumerable();
        }

        /// <summary>
        /// return the full anttendee data and convert it from key valu rows 
        /// to columns
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataTable GetAllToPivotTable()
        {
            var table = _entities.Set<AttendeeFullData>().AsEnumerable().ToPivotTable(
                item => item.Key,
               item => item.SessionAttendeeID,
               items => items.Any() ? items.First().Value : null);


            return table;
        }


        /// <summary>
        /// return the full anttendee data and convert it from key valu rows 
        /// to columns based on some anttendee ids
        /// </summary>
        /// <param name="ids"> attnedee ids</param>
        /// <remarks></remarks>
        public DataTable GetAllToPivotTable(IEnumerable<int> ids)
        {
            var table = _entities.Set<AttendeeFullData>().Where(a=> ids.Contains(a.SessionAttendeeID)).AsEnumerable().ToPivotTable(
                item => item.Key,
               item => item.SessionAttendeeID,
               items => items.Any() ? items.First().Value : null);


            return table;
        }


        /// <summary>
        /// return record based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SessionAttendee GetById(int id)
        {
            return _dbset.FirstOrDefault();
        }
    }
}
