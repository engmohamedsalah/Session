using AutoMapper;
using Session.Model;
using Session.Service;
using Session.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Session.Controllers
{
    /// <summary>
    /// controller work with the attendee and filter them based on 
    /// filteration criteria
    /// </summary>
    /// <remarks></remarks>
    public class AttendeeController : Controller
    {
        ISessionClassService _SessionClassService;
        ISessionAttendeeService _SessionAttendeeService;
        public AttendeeController(ISessionClassService sessionClassService, ISessionAttendeeService sessionAttendeeService)
        {
            _SessionClassService = sessionClassService;
            _SessionAttendeeService = sessionAttendeeService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public ActionResult Index()
        {
            //get all session from database
            var classs = _SessionClassService.GetAll();
            var model = new AttendeeFilterViewModel();
            model.AttendeeSessionFilter = new List<SessionFilterViewModel>();

            foreach (var c in classs.Select(a => new { a.SessionTopicID, a.SessionTopic.Name }).Distinct())
            {
                var times = classs.Where(a => a.SessionTopicID == c.SessionTopicID);
                var sessionRow = new SessionFilterViewModel();

                //default value of the checkbox not selected
                sessionRow.IsSelected = false;
                sessionRow.SessionTopicName = c.Name;
                sessionRow.SessionTopicID = c.SessionTopicID;
                sessionRow.TimeSlots = new List<TimeSlot>();

                foreach (var time in times)
                    sessionRow.TimeSlots.Add(new TimeSlot() { SessionClassID = time.Id, IsSelected = false, Date = time.StartDate, From = time.StartDate, To = time.EndDate });

                model.AttendeeSessionFilter.Add(sessionRow);

            }

            return View(model);
        }

        /// <summary>
        /// this action is called after select filters {topics and times }
        /// and click search
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [HttpPost]
        public ActionResult Index(AttendeeFilterViewModel model)
        {
            if (ModelState.IsValid)
            {

                //get selected session Class Ids
                var selectedFilter = model.AttendeeSessionFilter.Where(a => a.IsSelected)
                    .Select(a =>
                    new { SessionTopicID = a.SessionTopicID, TimeSlots = a.TimeSlots.Where(b => b.IsSelected) }

                    ).ToList();

                //create filters ids
                //where each row is topic id with times slots in that topic
                List<Tuple<int, List<int>>> filterIds = new List<Tuple<int, List<int>>>();

                foreach (var item in selectedFilter)
                {
                    var tuple = new Tuple<int, List<int>>(item.SessionTopicID, item.TimeSlots.Select(a => a.SessionClassID).ToList());
                    filterIds.Add(tuple);
                }
                //get attendeeSession data that match the criteria
                var attendeeSessions = _SessionAttendeeService.
                    GetAllFilterByTopicAndTime(filterIds.ToArray(), model.AndingSession, model.AndingTime).ToList();
                //var attendeeSessions = _SessionAttendeeService.GetAll();

                //search the attend that meet the criteria
                var attendees = _SessionAttendeeService.GetAllToPivotTable(attendeeSessions.Select(a => a.Id));

                var result = Mapper.Map<DataTable, SessionAttendeeViewModel>(attendees);

                //add mandatory fields
                result.DataHeader.Add("Session");
                result.DataHeader.Add("StartDate");
                result.DataHeader.Add("EndDate");
                result.DataHeader[0] = "Attendee ID";


                foreach (var item in result.DataValue)
                {

                    var attendeeSession = attendeeSessions.SingleOrDefault(a => a.Id == int.Parse(item.First()));
                    if (attendeeSession == null)
                        continue;
                    item.Add(attendeeSession.SessionClass.SessionTopic.Name);
                    item.Add(attendeeSession.SessionClass.StartDate.ToString("dd/MM/yyyy h:mm tt"));
                    item.Add(attendeeSession.SessionClass.EndDate.ToString("dd/MM/yyyy h:mm tt"));
                    //replace the db id with Attendee id
                    item[0] = attendeeSession.AttendeeID.ToString();
                }
                model.SessionAttendeeResultViewModel = result;
            }
            return View(model);
        }

    }
}