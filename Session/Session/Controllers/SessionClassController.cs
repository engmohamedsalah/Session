using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Session.Model;
using Session.ViewModels;
using Session.Service;
using AutoMapper;

namespace Session.Controllers
{
    public class SessionClassController : Controller
    {


        //initialize service object
        ISessionClassService _SessionClassService;
        ISessionTopicService _SessionTopicService;
        ISessionRoomService _SessionRoomService;

        List<SelectListItem> topicItems = new List<SelectListItem>();
        List<SelectListItem> roomItems = new List<SelectListItem>();

        public SessionClassController(ISessionClassService SessionClassService, ISessionTopicService SessionTopicService, ISessionRoomService SessionRoomService)
        {
            _SessionClassService = SessionClassService;
            _SessionTopicService = SessionTopicService;
            _SessionRoomService = SessionRoomService;

            var topics = _SessionTopicService.GetAll();

            foreach (var t in topics)
                topicItems.Add(new SelectListItem() { Text = t.Name, Value = t.Id.ToString() });

            ViewBag.topicItems = topicItems;

            var rooms = _SessionRoomService.GetAll();

            foreach (var t in rooms)
                roomItems.Add(new SelectListItem() { Text = t.Name, Value = t.Id.ToString() });

            ViewBag.roomItems = roomItems;

        }


        // GET: SessionClass
        public ActionResult Index()
        {
            var Classs = _SessionClassService.GetAll();


            var result = Mapper.Map<IEnumerable<SessionClass>, IEnumerable<SessionClassViewModel>>(Classs);


            return View(result);
        }

        // GET: SessionClass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SessionClass = _SessionClassService.GetById(id.Value);

            if (SessionClass == null)
            {
                return HttpNotFound();
            }
            var result = Mapper.Map<SessionClass, SessionClassViewModel>(SessionClass);

            return View(result);
        }

        // GET: SessionClass/Create
        public ActionResult Create()
        {
           

            return View();
        }

        // POST: SessionClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SessionClassViewModel sessionClassViewModel)
        {
            if (ModelState.IsValid)
            {


                var result = Mapper.Map<SessionClassViewModel, SessionClass>(sessionClassViewModel);
                _SessionClassService.Create(result);

                return RedirectToAction("Index");
            }

            return View(sessionClassViewModel);
        }

        // GET: SessionClass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SessionClass = _SessionClassService.GetById(id.Value);
            if (SessionClass == null)
            {
                return HttpNotFound();
            }
            var result = Mapper.Map<SessionClass, SessionClassViewModel>(SessionClass);

            return View(result);
        }

        // POST: SessionClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SessionClassViewModel sessionClassViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = Mapper.Map<SessionClassViewModel, SessionClass>(sessionClassViewModel);
                _SessionClassService.Update(result);
                return RedirectToAction("Index");
            }
            return View(sessionClassViewModel);
        }

        // GET: SessionClass/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SessionClass = _SessionClassService.GetById(id.Value);
            if (SessionClass == null)
            {
                return HttpNotFound();
            }
            var result = Mapper.Map<SessionClass, SessionClassViewModel>(SessionClass);

            return View(result);
        }

        // POST: SessionClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var SessionClass = _SessionClassService.GetById(id);
            if (SessionClass == null)
            {
                return HttpNotFound();
            }
            _SessionClassService.Delete(SessionClass);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
