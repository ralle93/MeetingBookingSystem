using System;
using System.ComponentModel.DataAnnotations;

namespace BookingService.Models
{
    /// <summary>
    /// Class used by Entity Framework to store meetings.
    /// </summary>
    public class Meeting
    {
        /// <summary>
        /// Mandatory empty constructor required by Entity Framework.
        /// </summary>
        public Meeting() { }

        /// <summary>
        /// Constructor used for converting a MeetingDTO object into a Meeting object.
        /// Therefore ready to be saved to the database.
        /// </summary>
        /// <param name="meeting">MeetingDTO object.</param>
        public Meeting(MeetingDTO meeting)
        {
            this.Id = meeting.Id;
            this.Name = meeting.Name;
            this.DateTime = meeting.DateTime;
        }

        /// <summary>
        /// The database id of the meeting.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the meeting.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The date and time of the meeting.
        /// </summary>
        [Required]
        public DateTime DateTime { get; set; }
    }
}