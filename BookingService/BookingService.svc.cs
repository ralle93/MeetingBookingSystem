using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BookingService
{
    public class BookingService : IBookingService
    {
        private DataContext db;

        public BookingService()
        {
            db = new DataContext();
        }

        private bool SaveChanges()
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        private Meeting GetMeetingFromId(int id)
        {
            return db.Meetings.SingleOrDefault(m => m.Id == id);
        } 

        public List<MeetingDTO> GetWeekMeetings(DateTime startDate)
        {
            DateTime endDate = startDate.AddDays(7);

            var query = db.Meetings.Where(m => m.DateTime >= startDate && m.DateTime <= endDate).ToList();

            List<MeetingDTO> list = new List<MeetingDTO>();
            foreach (var meeting in query)
            {
                list.Add(new MeetingDTO(meeting));
            }

            return list;
        }

        public bool AddMeeting(MeetingDTO meeting)
        {
            db.Meetings.Add(new Meeting(meeting));

            return SaveChanges();
        }

        public bool RemoveMeetingFromId(int id)
        {
            Meeting meeting = GetMeetingFromId(id);
            db.Meetings.Remove(meeting);

            return SaveChanges();
        }
    }
}
