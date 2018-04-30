using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BookingService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookingService.Tests
{
    /// <summary>
    /// Class for unit testing the WCF service.
    /// </summary>
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Methods for setting up the mock DataContext used in each of the unit testing methods.
        /// </summary>
        /// <returns>Mock of Datacontext.</returns>
        private Mock<DataContext> SetupMockContext()
        {
            var data = new List<Meeting>
            {
                new Meeting { Id = 1, Name = "Customer meeting", DateTime = new DateTime(2018, 4, 30) },
                new Meeting { Id = 2, Name = "Sales meeting", DateTime = new DateTime(2018, 5, 1) }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Meeting>>();
            mockSet.As<IQueryable<Meeting>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Meeting>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Meeting>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Meeting>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Meetings).Returns(mockSet.Object);

            return mockContext;
        }

        /// <summary>
        /// Unit testing method of the GetWeekMeetings in Bookingservice.
        /// </summary>
        [TestMethod]
        public void GetWeekMeetings()
        {
            var mockContext = SetupMockContext();
            var service = new BookingService(mockContext.Object);

            List<MeetingDTO> meetings = service.GetWeekMeetings(new DateTime(2018, 4, 30));

            Assert.IsNotNull(meetings);
            Assert.AreEqual(2, meetings.Count);
            Assert.AreEqual(1, meetings[0].Id);
            Assert.AreEqual(2, meetings[1].Id);
            Assert.AreEqual("Customer meeting", meetings[0].Name);
            Assert.AreEqual("Sales meeting", meetings[1].Name);
            Assert.AreEqual(new DateTime(2018, 4, 30), meetings[0].DateTime);
            Assert.AreEqual(new DateTime(2018, 5, 1), meetings[1].DateTime);
        }

        /// <summary>
        /// Unit testing method of the AddMeeting in Bookingservice.
        /// </summary>
        [TestMethod]
        public void AddMeeting()
        {
            var mockContext = SetupMockContext();
            var service = new BookingService(mockContext.Object);

            MeetingDTO meeting = new MeetingDTO
            {
                Name = "Customer meeting",
                DateTime = DateTime.Now
            };

            bool result = service.AddMeeting(meeting);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Unit testing method of the RemoveMeetingFromId in Bookingservice.
        /// </summary>
        [TestMethod]
        public void RemoveMeetingFromId()
        {
            var mockContext = SetupMockContext();
            var service = new BookingService(mockContext.Object);

            bool result = service.RemoveMeetingFromId(2);
            Assert.IsTrue(result);
        }
    }
}
