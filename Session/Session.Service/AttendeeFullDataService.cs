using Session.Model;
using Session.Repository;

/// <summary>
/// AttendeeFullDataService that handel working with AttendeeFullData table
/// </summary>
namespace Session.Service
{
    public class AttendeeFullDataService : EntityService<AttendeeFullData>, IAttendeeFullDataService
    {
        IUnitOfWork _unitOfWork;
        IAttendeeFullDataRepository _attendeeFullDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Session.Service.AttendeeFullDataService" /> class. 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="attendeeFullDataRepository"></param>
        /// <remarks></remarks>
        public AttendeeFullDataService(IUnitOfWork unitOfWork, IAttendeeFullDataRepository attendeeFullDataRepository)
                  : base(unitOfWork, attendeeFullDataRepository)
        {
            _unitOfWork = unitOfWork;
            _attendeeFullDataRepository = attendeeFullDataRepository;
        }


        /// <summary>
        /// return all record that match with id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public AttendeeFullData GetById(int Id)
        {
            return _attendeeFullDataRepository.GetById(Id);
        }


    }
}
