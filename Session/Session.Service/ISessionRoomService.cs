using Session.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Service
{
    public interface ISessionRoomService : IEntityService<SessionRoom>
    {
        SessionRoom GetById(int Id);
    }
}
