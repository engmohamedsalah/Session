using Session.Model;
using Session.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Service
{
    public class SessionRoomService : EntityService<SessionRoom>, ISessionRoomService
    {
        IUnitOfWork _unitOfWork;
        ISessionRoomRepository _sessionRoomRepository;

        public SessionRoomService(IUnitOfWork unitOfWork, ISessionRoomRepository sessionRoomRepository)
          : base(unitOfWork, sessionRoomRepository)
      {
            _unitOfWork = unitOfWork;
            _sessionRoomRepository = sessionRoomRepository;
        }


        public SessionRoom GetById(int Id)
        {
            return _sessionRoomRepository.GetById(Id);
        }
    }
}
