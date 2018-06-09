using Session.Model;
using Session.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Service
{
    public class SessionClassService : EntityService<SessionClass>, ISessionClassService
    {
        IUnitOfWork _unitOfWork;
        ISessionClassRepository _sessionClassRepository;

        public SessionClassService(IUnitOfWork unitOfWork, ISessionClassRepository sessionClassRepository)
          : base(unitOfWork, sessionClassRepository)
      {
            _unitOfWork = unitOfWork;
            _sessionClassRepository = sessionClassRepository;
        }


        public SessionClass GetById(int Id)
        {
            return _sessionClassRepository.GetById(Id);
        }
    }
}
