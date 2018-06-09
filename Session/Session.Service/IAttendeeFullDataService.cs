using Session.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Service
{
    public interface IAttendeeFullDataService: IEntityService<AttendeeFullData>
    {
        AttendeeFullData GetById(int Id);

       
    }
}
