using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Session.Model;
namespace Session.Repository
{
    /// <summary>
    /// interface IAttendeeFullDataRepository
    /// </summary>
    public interface IAttendeeFullDataRepository : IGenericRepository<AttendeeFullData>
    {
        AttendeeFullData GetById(int id);
       
    }
}
