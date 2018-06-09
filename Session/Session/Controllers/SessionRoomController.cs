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
    public class SessionRoomController : Controller
    {


        //initialize service object
        ISessionRoomService _SessionRoomService;

        public SessionRoomController(ISessionRoomService SessionRoomService)
        {
            _SessionRoomService = SessionRoomService;

        }


        // GET: SessionRoom
        public ActionResult Index()
        {
            var Rooms = _SessionRoomService.GetAll();


            var result = Mapper.Map<IEnumerable<SessionRoom>, IEnumerable<SessionRoomViewModel>>(Rooms);


            return View(result);
        }

        // GET: SessionRoom/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SessionRoom = _SessionRoomService.GetById(id.Value);

            if (SessionRoom == null)
            {
                return HttpNotFound();
            }
            var result = Mapper.Map<SessionRoom, SessionRoomViewModel>(SessionRoom);

            return View(result);
        }

        // GET: SessionRoom/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SessionRoom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SessionRoomViewModel sessionRoomViewModel)
        {
            if (ModelState.IsValid)
            {


                var result = Mapper.Map<SessionRoomViewModel, SessionRoom>(sessionRoomViewModel);
                _SessionRoomService.Create(result);

                return RedirectToAction("Index");
            }

            return View(sessionRoomViewModel);
        }

        // GET: SessionRoom/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SessionRoom = _SessionRoomService.GetById(id.Value);
            if (SessionRoom == null)
            {
                return HttpNotFound();
            }
            var result = Mapper.Map<SessionRoom, SessionRoomViewModel>(SessionRoom);

            return View(result);
        }

        // POST: SessionRoom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SessionRoomViewModel sessionRoomViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = Mapper.Map<SessionRoomViewModel, SessionRoom>(sessionRoomViewModel);
                _SessionRoomService.Update(result);
                return RedirectToAction("Index");
            }
            return View(sessionRoomViewModel);
        }

        // GET: SessionRoom/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SessionRoom = _SessionRoomService.GetById(id.Value);
            if (SessionRoom == null)
            {
                return HttpNotFound();
            }
            var result = Mapper.Map<SessionRoom, SessionRoomViewModel>(SessionRoom);

            return View(result);
        }

        // POST: SessionRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var SessionRoom = _SessionRoomService.GetById(id);
            if (SessionRoom == null)
            {
                return HttpNotFound();
            }
            _SessionRoomService.Delete(SessionRoom);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
