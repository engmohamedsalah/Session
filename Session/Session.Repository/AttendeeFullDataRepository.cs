using Session.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Repository
{
    /// <summary>
    /// Repository of AttendeeFullData table 
    /// resposible for return the full data of the attendee
    /// </summary>
    /// <remarks></remarks>
    public class AttendeeFullDataRepository : GenericRepository<AttendeeFullData>, IAttendeeFullDataRepository
    {
       
        public AttendeeFullDataRepository(DbContext context) : base(context)
        {

        }
        /// <summary>
        /// return all records
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public override IEnumerable<AttendeeFullData> GetAll()
        {
            return _entities.Set<AttendeeFullData>().AsEnumerable();
        }



        /// <summary>
        /// return on record that has a certain id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AttendeeFullData GetById(int id)
        {
            return _dbset.FirstOrDefault();
        }
    }
}
