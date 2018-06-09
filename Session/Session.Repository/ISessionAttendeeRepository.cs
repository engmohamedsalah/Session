using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Session.Model;
using System.Data;

namespace Session.Repository
{
    public interface ISessionAttendeeRepository : IGenericRepository<SessionAttendee>
    {
        SessionAttendee GetById(int id);

        DataTable GetAllToPivotTable();
        DataTable GetAllToPivotTable(IEnumerable<int> ids);

    }
}
