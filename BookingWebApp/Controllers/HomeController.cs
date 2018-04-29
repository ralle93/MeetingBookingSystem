using BookingWebApp.ServiceReference;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingWebApp.Controllers
{
    public class HomeController : Controller
    {
        private BookingServiceClient serviceClient;

        public HomeController()
        {
            serviceClient = new BookingServiceClient();

            MeetingDTO meeting = new MeetingDTO();
            meeting.Name = "Customer meeting";
            meeting.DateTime = new DateTime(2018, 4, 26, 12, 0, 0);
            //serviceClient.AddMeeting(meeting);
        }

        // TODO: Fix magic numbers
        public ActionResult Index(string weekStartDate)
        {
            DateTime startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday - 7);

            if (weekStartDate != "" && weekStartDate != null)
            {
                startDate = DateTime.Parse(weekStartDate);
            }

            TempData["startDate"] = startDate;
            var meetings = serviceClient.GetWeekMeetings(startDate);

            return View(meetings);
        }

        [HttpPost]
        public ActionResult AddMeeting(DateTime dateTime)
        {
            return View(dateTime);
        }

        [HttpPost]
        public ActionResult AddMeetingSubmit(DateTime dateTime, string name)
        {
            MeetingDTO meeting = new MeetingDTO();
            meeting.DateTime = dateTime;
            meeting.Name = name;
            serviceClient.AddMeeting(meeting);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RemoveMeeting(string meetingId)
        {
            serviceClient.RemoveMeetingFromId(int.Parse(meetingId));

            return RedirectToAction("Index", "Home");
        }
    }
}