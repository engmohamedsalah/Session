using Session.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Repository
{
    public class SessionClassRepository : GenericRepository<SessionClass>, ISessionClassRepository
    {
        public SessionClassRepository(DbContext context): base(context)
        {

        }
        public override IEnumerable<SessionClass> GetAll()
        {
            return _entities.Set<SessionClass>().AsEnumerable();
        }



        public SessionClass GetById(int id)
        {
            return _dbset.FirstOrDefault();
        }
    }
}
