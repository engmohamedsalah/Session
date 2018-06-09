using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Session.Model;
namespace Session.Repository
{
    public interface ISessionRoomRepository : IGenericRepository<SessionRoom>
    {
        SessionRoom GetById(int id);
    }
}
