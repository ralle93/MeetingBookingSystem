using System;
using System.Runtime.Serialization;

namespace BookingService.Models
{
    /// <summary>
    /// Class used by WCF Service to transfer meeting information.
    /// </summary>
    [DataContract]
    public class MeetingDTO
    {
        /// <summary>
        /// Mandatory empty constructor required by WCF.
        /// </summary>
        public MeetingDTO() { }

        /// <summary>
        /// Constructor used for converting a Meeting object into a MeetingDTO object.
        /// Therefore ready to be transfered with WCF and used in frontend.
        /// </summary>
        /// <param name="meeting">Meeting object.</param>
        public MeetingDTO(Meeting meeting)
        {
            this.Id = meeting.Id;
            this.Name = meeting.Name;
            this.DateTime = meeting.DateTime;
        }

        /// <summary>
        /// The database id of the meeting.
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Name of the meeting.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The date and time of the meeting.
        /// </summary>
        [DataMember]
        public DateTime DateTime { get; set; }
    }
}