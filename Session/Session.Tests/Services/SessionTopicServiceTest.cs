using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Session.Model;
using Session.Repository;
using Session.Service;
using System.Collections.Generic;

namespace Session.Tests.Services
{
    [TestClass]
    public class SessionTopicServiceTest
    {
        private Mock<ISessionTopicRepository> _mockRepository;
        private ISessionTopicService _service;
        Mock<IUnitOfWork> _mockUnitWork;
        List<SessionTopic> listSessionTopics;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<ISessionTopicRepository>();
            _mockUnitWork = new Mock<IUnitOfWork>();
            _service = new SessionTopicService(_mockUnitWork.Object, _mockRepository.Object);
            listSessionTopics = new List<SessionTopic>() {
           new SessionTopic() { Name = "XML" },
           new SessionTopic() { Name = "HTML" },
           new SessionTopic() { Name = "JavaScript" }
          };
        }

        [TestMethod]
        public void SessionTopicGetAll()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(listSessionTopics);

            //Act
            List<SessionTopic> results = _service.GetAll() as List<SessionTopic>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("XML", results[0].Name);
        }


        [TestMethod]
        public void CanAddSessionTopic()
        {
            //Arrange
            int id = 1;
            SessionTopic emp = new SessionTopic() { Name = "UK" };
            _mockRepository.Setup(m => m.Add(emp)).Returns((SessionTopic e) =>
            {
                e.Id = id;
                return e;
            });


            //Act
            _service.Create(emp);

            //Assert
            Assert.AreEqual(id, emp.Id);
            _mockUnitWork.Verify(m => m.Commit(), Times.Once);
        }


    }
}
