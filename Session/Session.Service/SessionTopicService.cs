using Session.Model;
using Session.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Service
{
    public class SessionTopicService : EntityService<SessionTopic>, ISessionTopicService
    {
        IUnitOfWork _unitOfWork;
        ISessionTopicRepository _sessionTopicRepository;

        public SessionTopicService(IUnitOfWork unitOfWork, ISessionTopicRepository sessionTopicRepository)
          : base(unitOfWork, sessionTopicRepository)
      {
            _unitOfWork = unitOfWork;
            _sessionTopicRepository = sessionTopicRepository;
        }


        public SessionTopic GetById(int Id)
        {
          
            return _sessionTopicRepository.GetById(Id);
        }
    }
}
