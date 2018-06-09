using Session.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Service
{
    public interface ISessionAttendeeService: IEntityService<SessionAttendee>
    {
        SessionAttendee GetById(int id);

        string UploadAttendee(string filePath);

        DataTable GetAllToPivotTable(IEnumerable<int> ids);
        DataTable GetAllToPivotTable();

        IEnumerable<SessionAttendee> GetAllFilterByTopicAndTime(Tuple<int, List<int>>[] filters, bool andSession, bool andTime);



    }
}
