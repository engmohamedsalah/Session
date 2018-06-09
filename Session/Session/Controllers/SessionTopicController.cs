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
    public class SessionTopicController : Controller
    {


        //initialize service object
        ISessionTopicService _SessionTopicService;

        public SessionTopicController(ISessionTopicService SessionTopicService)
        {
            _SessionTopicService = SessionTopicService;
           
        }
       

        // GET: SessionTopic
        public ActionResult Index()
        {
            var topics = _SessionTopicService.GetAll();

         
            var result = Mapper.Map<IEnumerable<SessionTopic>, IEnumerable<SessionTopicViewModel>>(topics);


            return View(result);
        }

        // GET: SessionTopic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SessionTopic =_SessionTopicService.GetById(id.Value);
           
            if (SessionTopic == null)
            {
                return HttpNotFound();
            }
            var result = Mapper.Map<SessionTopic,SessionTopicViewModel>(SessionTopic);

            return View(result);
        }

        // GET: SessionTopic/Create
        public ActionResult Create()
        {
            return View();
        }
       
        // POST: SessionTopic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SessionTopicViewModel sessionTopicViewModel)
        {
            if (ModelState.IsValid)
            {


                var result = Mapper.Map<SessionTopicViewModel, SessionTopic>(sessionTopicViewModel);
                _SessionTopicService.Create(result);

                return RedirectToAction("Index");
            }

            return View(sessionTopicViewModel);
        }

        // GET: SessionTopic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SessionTopic = _SessionTopicService.GetById(id.Value);
            if (SessionTopic == null)
            {
                return HttpNotFound();
            }
            var result = Mapper.Map<SessionTopic, SessionTopicViewModel>(SessionTopic);

            return View(result);
        }

        // POST: SessionTopic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SessionTopicViewModel sessionTopicViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = Mapper.Map<SessionTopicViewModel, SessionTopic>(sessionTopicViewModel);
                _SessionTopicService.Update(result);
                return RedirectToAction("Index");
            }
            return View(sessionTopicViewModel);
        }

        // GET: SessionTopic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SessionTopic = _SessionTopicService.GetById(id.Value);
            if (SessionTopic == null)
            {
                return HttpNotFound();
            }
            var result = Mapper.Map<SessionTopic, SessionTopicViewModel>(SessionTopic);

            return View(result);
        }

        // POST: SessionTopic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var SessionTopic = _SessionTopicService.GetById(id);
            if (SessionTopic == null)
            {
                return HttpNotFound();
            }
            _SessionTopicService.Delete(SessionTopic);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
