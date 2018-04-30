using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BookingService.Models;

namespace BookingService
{
    /// <summary>
    /// Class for WCF service.
    /// Uses IBookingService interface.
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly DataContext _db;

        /// <summary>
        /// Constructor used when running the service normally.
        /// </summary>
        public BookingService()
        {
            this._db = new DataContext();
        }

        /// <summary>
        /// ConStructor used for unit testing.
        /// </summary>
        /// <param name="db">Datacontext for use with service.</param>
        public BookingService(DataContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Saves all changes to database.
        /// </summary>
        /// <returns>Returns true if successfull.</returns>
        private bool SaveChanges()
        {
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Get a Meeting from id.
        /// </summary>
        /// <param name="id">Meeting id.</param>
        /// <returns>Meeting object.</returns>
        private Meeting GetMeetingFromId(int id)
        {
            return _db.Meetings.SingleOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// Get a list of meetings from the specified date and a week forward.
        /// </summary>
        /// <param name="startDate">Date and time to get meetings from.</param>
        /// <returns>List of MeetingDTO objects.</returns>
        public List<MeetingDTO> GetWeekMeetings(DateTime startDate)
        {
            DateTime endDate = startDate.AddDays(7);

            var meetings = _db.Meetings
                .Where(m => m.DateTime >= startDate && m.DateTime <= endDate)
                .Select(m => new MeetingDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    DateTime = m.DateTime
                }).ToList();

            return meetings;
        }

        /// <summary>
        /// Add a meeting to database.
        /// </summary>
        /// <param name="meeting">MeetingDTO object.</param>
        /// <returns>Returns true if successfull.</returns>
        public bool AddMeeting(MeetingDTO meeting)
        {
            if (_db.Meetings.Any(m => m.DateTime == meeting.DateTime))
                return false;

            _db.Meetings.Add(new Meeting(meeting));

            return SaveChanges();
        }

        /// <summary>
        /// Remove a meeting from the database.
        /// </summary>
        /// <param name="id">Meeting id.</param>
        /// <returns>Returns true if successfull.</returns>
        public bool RemoveMeetingFromId(int id)
        {
            Meeting meeting = GetMeetingFromId(id);

            if (meeting == null)
                return false;

            _db.Meetings.Remove(meeting);

            return SaveChanges();
        }
    }
}
