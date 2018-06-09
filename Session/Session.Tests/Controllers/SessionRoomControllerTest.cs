using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Session.Service;
using System.Collections.Generic;
using Session.Controllers;
using Session.Model;
using System.Web.Mvc;
using Session.ViewModels;
using AutoMapper;
using System.Web;
using Session.Models;

namespace Session.Tests.Controllers
{
    [TestClass]
    public class SessionRoomControllerTest
    {
        private Mock<ISessionRoomService> _sessionRoomServiceMock;
        SessionRoomController objController;
        List<SessionRoom> listSessionRooms;

      
        [TestInitialize()]
        public void Initialize()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<AutomapperProfile>());
           
            _sessionRoomServiceMock = new Mock<ISessionRoomService>();
            objController = new SessionRoomController(_sessionRoomServiceMock.Object);
            
            listSessionRooms = new List<SessionRoom>() {
           new SessionRoom() {  Name = "XML" },
           new SessionRoom() {  Name = "HTML" },
           new SessionRoom() {  Name = "JavaScript" }
          };

           
        }
        //////////
        [TestMethod]
        public void SessionRoom_Get_All()
        {
            //Arrange
            _sessionRoomServiceMock.Setup(x => x.GetAll()).Returns(listSessionRooms);

            //Act
            var result = ((objController.Index() as ViewResult).Model) as List<SessionRoomViewModel>;

            //Assert
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual("XML", result[0].Name);
            Assert.AreEqual("HTML", result[1].Name);
            Assert.AreEqual("JavaScript", result[2].Name);

        }

        [TestMethod]
        public void Valid_SessionRoom_Create()
        {
            //Arrange
            SessionRoom newSessionRoom = new SessionRoom() { Name = "test1" };

            //convert with automapper
            var newSessionRoomViewModel = Mapper.Map<SessionRoom, SessionRoomViewModel>(newSessionRoom);

            //Act
            var result = (RedirectToRouteResult)objController.Create(newSessionRoomViewModel);


            //convert with automapper
            newSessionRoom = Mapper.Map<SessionRoomViewModel, SessionRoom>(newSessionRoomViewModel);


            //Assert 
            _sessionRoomServiceMock.Verify(m => m.Create(newSessionRoom), Times.Never);
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

       

        /////////
    }
}
