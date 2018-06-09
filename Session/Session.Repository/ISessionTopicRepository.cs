using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Session.Model;
namespace Session.Repository
{
    public interface ISessionTopicRepository : IGenericRepository<SessionTopic>
    {
        SessionTopic GetById(int id);
    }
}
