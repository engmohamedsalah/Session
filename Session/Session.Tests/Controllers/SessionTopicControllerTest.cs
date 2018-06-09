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
    public class SessionTopicControllerTest
    {
        private Mock<ISessionTopicService> _sessionTopicServiceMock;
        SessionTopicController objController;
        List<SessionTopic> listSessionTopics;

        public SessionTopicControllerTest()
        {

        }
        [TestInitialize()]
        public void Initialize()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<AutomapperProfile>());
           
            _sessionTopicServiceMock = new Mock<ISessionTopicService>();
            objController = new SessionTopicController(_sessionTopicServiceMock.Object);
            
            listSessionTopics = new List<SessionTopic>() {
           new SessionTopic() {  Name = "XML" },
           new SessionTopic() {  Name = "HTML" },
           new SessionTopic() {  Name = "JavaScript" }
          };

           
        }
        //////////
        [TestMethod]
        public void SessionTopic_Get_All()
        {
            //Arrange
            _sessionTopicServiceMock.Setup(x => x.GetAll()).Returns(listSessionTopics);

            //Act
            var result = ((objController.Index() as ViewResult).Model) as List<SessionTopicViewModel>;

            //Assert
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual("XML", result[0].Name);
            Assert.AreEqual("HTML", result[1].Name);
            Assert.AreEqual("JavaScript", result[2].Name);

        }

        [TestMethod]
        public void Valid_SessionTopic_Create()
        {
            //Arrange
            SessionTopic newSessionTopic = new SessionTopic() { Name = "test1" };

            //convert with automapper
            var newSessionTopicViewModel = Mapper.Map<SessionTopic, SessionTopicViewModel>(newSessionTopic);

            //Act
            var result = (RedirectToRouteResult)objController.Create(newSessionTopicViewModel);


            //convert with automapper
            newSessionTopic = Mapper.Map<SessionTopicViewModel, SessionTopic>(newSessionTopicViewModel);


            //Assert 
            _sessionTopicServiceMock.Verify(m => m.Create(newSessionTopic), Times.Never);
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        
        /////////
    }
}
