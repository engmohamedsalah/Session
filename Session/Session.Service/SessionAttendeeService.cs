using Session.Model;
using Session.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq.Expressions;

namespace Session.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionAttendeeService : EntityService<SessionAttendee>, ISessionAttendeeService
    {
        IUnitOfWork _unitOfWork;
        ISessionAttendeeRepository _sessionAttendeeRepository;

        public SessionAttendeeService(IUnitOfWork unitOfWork, ISessionAttendeeRepository sessionAttendeeRepository)
          : base(unitOfWork, sessionAttendeeRepository)
        {
            _unitOfWork = unitOfWork;
            _sessionAttendeeRepository = sessionAttendeeRepository;
        }


        /// <summary>
        /// return record that match id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SessionAttendee GetById(int id)
        {
            return _sessionAttendeeRepository.GetById(id);
        }

        /// <summary>
        /// upload csv file and save it to database
        /// check the file contains at least two column {AttendeeID,SessionTimeID}
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>string message for log</returns>
        public string UploadAttendee(string filePath)
        {
            StringBuilder logMessage = new StringBuilder();

            //Read the contents of CSV file.
            string[] csvData = System.IO.File.ReadAllLines(filePath);

            //Execute a loop over the rows.
            var header = csvData[0].Split(',');
            if (header[0].Replace(" ", string.Empty) != "AttendeeID" || header[1].Replace(" ", string.Empty) != "SessionTimeID")
            {
                logMessage.AppendLine("AttendeeID or SessionTimeID not exist");
                logMessage.AppendLine("Process Fail");


            }
            else
            {
                //clear old data before insert new records
                _sessionAttendeeRepository.RemoveAll();

                //loop on each row in the file and insert it one by one
                for (int i = 1; i < csvData.Length; i++)
                {
                    if (!string.IsNullOrEmpty(csvData[i]))
                    {
                        var row = csvData[i].Split(',');
                        var newSessionAttendee = new SessionAttendee();
                        //Add Mandatory Date
                        newSessionAttendee.AttendeeID = int.Parse(row[0]);
                        newSessionAttendee.SessionTimeID = int.Parse(row[1]);

                        //add the reset of data in extension table AttendeeFullData
                        newSessionAttendee.AttendeeFullData = new List<AttendeeFullData>();
                        //for other columns in csv file save the data as key & value in table
                        //AttendeeFullData
                        for (int j = 2; j < header.Length; j++)
                        {
                            var newFullData = new AttendeeFullData();

                            newFullData.Key = header[j];
                            newFullData.Value = row[j];
                            newFullData.SessionAttendee = newSessionAttendee;
                            newSessionAttendee.AttendeeFullData.Add(newFullData);


                        }

                        _sessionAttendeeRepository.Add(newSessionAttendee);

                        logMessage.AppendLine("Row of index " + i + " done");
                    }
                    else
                        logMessage.AppendLine("Row of index " + i + " Empty");
                }
            }

            //commit to database after adding the records
            _sessionAttendeeRepository.Save();

            //retunr the log message
            return logMessage.ToString();
        }

        /// <summary>
        /// convert data that come from AttendeeFullData as key value as columns
        /// and group by attendee id
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllToPivotTable()
        {
            var attendeeData = _sessionAttendeeRepository.GetAllToPivotTable();
            return attendeeData;
        }
        /// <summary>
        /// convert data that come from AttendeeFullData as key value as columns
        /// that match the sent ids 
        /// </summary>
        /// <param name="ids">return data only that match the ids</param>
        /// <returns></returns>
        public DataTable GetAllToPivotTable(IEnumerable<int> ids)
        {
            var attendeeData = _sessionAttendeeRepository.GetAllToPivotTable(ids);
            return attendeeData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="andSession"></param>
        /// <param name="andTime"></param>
        /// <returns></returns>
        public IEnumerable<SessionAttendee> GetAllFilterByTopicAndTime(Tuple<int, List<int>>[] filters, bool andSession, bool andTime)
        {
          
            var sessionsIds = filters.Select(b => b.Item1).ToList();
            var timesIds = filters.SelectMany(a => a.Item2.Select(b => b));

            var attendee = _sessionAttendeeRepository
                .Where(a => sessionsIds.Contains(a.SessionClass.SessionTopicID)
                && timesIds.Contains(a.SessionTimeID)).GroupBy(p => new { p.AttendeeID, p.SessionClass.SessionTopicID })
                 .Select(g => new
                 {
                     //attendee ID
                     //AttendeeID = g.Key.AttendeeID,
                     //session classes
                     SessionAttendees = g,
                     //count of time slots
                     timeStlotsCountForAttendee = g.Select(a => a.SessionTimeID).Count(),
                     timeStlotsCountNeedByAnd = filters.FirstOrDefault(a => a.Item1 == g.Key.SessionTopicID).Item2.Count,
                     //TopicID
                     //topicID = g.Key.SessionTopicID,
                     
                 }).ToList();


            //filter related to topic
            var attendeeAfterFilter = (andSession) ? attendee.Where(a => a.SessionAttendees.Count() >= sessionsIds.Count)
                .Select(a => a)
                : attendee.Where(a => a.SessionAttendees.Count() > 0).Select(a => a).AsQueryable();


            //filter related to times
            attendeeAfterFilter = (andTime) ?
               attendeeAfterFilter.Where(a => a.timeStlotsCountForAttendee >= a.timeStlotsCountNeedByAnd)
               .Select(a => a)
               : attendeeAfterFilter.Where(a => a.timeStlotsCountForAttendee > 0)
               .Select(a => a);


            //return result

            return attendeeAfterFilter.SelectMany(a => a.SessionAttendees);
        }
    }
}
