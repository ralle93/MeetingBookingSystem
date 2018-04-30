using BookingWebApp.ServiceReference;
using System;
using System.Web.Mvc;

namespace BookingWebApp.Controllers
{
    /// <summary>
    /// Controller class for handling GET and POST requests on the webapp.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly BookingServiceClient _serviceClient;

        /// <summary>
        /// Controller constructor the WCF client is initialized here.
        /// </summary>
        public HomeController()
        {
            _serviceClient = new BookingServiceClient();
        }

        /// <summary>
        /// Method for getting the DateTime object of the first day in the weeek.
        /// </summary>
        /// <param name="dateTime">DateTime object.</param>
        /// <returns>DateTime object of monday in the week.</returns>
        private DateTime GetMonday(DateTime dateTime)
        {
            return dateTime.Date.AddDays(-((int)dateTime.Date.DayOfWeek - (int)DayOfWeek.Monday));
        }

        /// <summary>
        /// Shows the view for the meeting list of a week.
        /// </summary>
        /// <param name="weekStartDate">First day of the week to show.</param>
        /// <returns>Main view.</returns>
        public ActionResult Index(string weekStartDate)
        {
            if (!DateTime.TryParse(weekStartDate, out var startDate))
                startDate = GetMonday(DateTime.Today);

            TempData["startDate"] = startDate;
            var meetings = _serviceClient.GetWeekMeetings(startDate);

            return View(meetings);
        }

        /// <summary>
        /// View for adding a meeting.
        /// </summary>
        /// <param name="dateTime">Date and time of the meeting to add.</param>
        /// <returns>Add meeting view.</returns>
        [HttpPost]
        public ActionResult AddMeeting(DateTime dateTime)
        {
            return View(dateTime);
        }

        /// <summary>
        /// Post request to add a meeting.
        /// </summary>
        /// <param name="dateTime">Date and time of the meeting.</param>
        /// <param name="name">Name of the meeting.</param>
        /// <returns>Main view.</returns>
        [HttpPost]
        public ActionResult AddMeetingSubmit(DateTime dateTime, string name)
        {
            MeetingDTO meeting = new MeetingDTO
            {
                DateTime = dateTime,
                Name = name
            };
            _serviceClient.AddMeeting(meeting);

            DateTime monday = GetMonday(dateTime);
            return RedirectToAction("Index", "Home", new { weekStartDate = monday.ToString() });
        }

        /// <summary>
        /// Post request for removing a meeting.
        /// </summary>
        /// <param name="dateTime">Date and time of the meeting used when redirecting back to main view.</param>
        /// <param name="meetingId">Id of meeting to remove.</param>
        /// <returns>Main view.</returns>
        [HttpPost]
        public ActionResult RemoveMeeting(DateTime dateTime, string meetingId)
        {
            if (int.TryParse(meetingId, out int id))
                _serviceClient.RemoveMeetingFromId(id);

            DateTime monday = GetMonday(dateTime);
            return RedirectToAction("Index", "Home", new { weekStartDate = monday.ToString() });
        }
    }
}