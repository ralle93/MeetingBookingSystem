using System;
using System.Collections.Generic;
using System.ServiceModel;
using BookingService.Models;

namespace BookingService
{
    /// <summary>
    /// Interface that defines the methods accessible for the WCF client.
    /// </summary>
    [ServiceContract]
    public interface IBookingService
    {
        /// <summary>
        /// Get a list of meetings from the specified date and a week forward.
        /// </summary>
        /// <param name="startDate">Date and time to get meetings from.</param>
        /// <returns>List of MeetingDTO objects.</returns>
        [OperationContract]
        List<MeetingDTO> GetWeekMeetings(DateTime startDate);

        /// <summary>
        /// Add a meeting to database.
        /// </summary>
        /// <param name="meeting">MeetingDTO object.</param>
        /// <returns>Returns true if successfull.</returns>
        [OperationContract]
        bool AddMeeting(MeetingDTO meeting);

        /// <summary>
        /// Remove a meeting from the database.
        /// </summary>
        /// <param name="id">Meeting id.</param>
        /// <returns>Returns true if successfull.</returns>
        [OperationContract]
        bool RemoveMeetingFromId(int id);
    }
}
