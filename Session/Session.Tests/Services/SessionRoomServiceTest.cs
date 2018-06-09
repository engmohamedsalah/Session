using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Session.Model;
using Session.Repository;
using Session.Service;
using System.Collections.Generic;

namespace Session.Tests.Services
{
    [TestClass]
    public class SessionRoomServiceTest
    {
        private Mock<ISessionRoomRepository> _mockRepository;
        private ISessionRoomService _service;
        Mock<IUnitOfWork> _mockUnitWork;
        List<SessionRoom> listSessionRooms;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<ISessionRoomRepository>();
            _mockUnitWork = new Mock<IUnitOfWork>();
            _service = new SessionRoomService(_mockUnitWork.Object, _mockRepository.Object);
            listSessionRooms = new List<SessionRoom>() {
           new SessionRoom() { Name = "Room 101" },
           new SessionRoom() { Name = "Hall" },
           new SessionRoom() { Name = "Room 502" }
          };
        }

        [TestMethod]
        public void SessionRoomGetAll()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(listSessionRooms);

            //Act
            List<SessionRoom> results = _service.GetAll() as List<SessionRoom>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("Room 101", results[0].Name);
        }


        [TestMethod]
        public void CanAddSessionRoom()
        {
            //Arrange
            int id = 1;
            SessionRoom emp = new SessionRoom() { Name = "Room A1" };
            _mockRepository.Setup(m => m.Add(emp)).Returns((SessionRoom e) =>
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
