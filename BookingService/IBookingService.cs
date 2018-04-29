using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BookingService
{
    [ServiceContract]
    public interface IBookingService
    {
        [OperationContract]
        List<MeetingDTO> GetWeekMeetings(DateTime startDate);

        [OperationContract]
        bool AddMeeting(MeetingDTO meeting);

        [OperationContract]
        bool RemoveMeetingFromId(int id);
    }

    [DataContract]
    public class MeetingDTO
    {
        public MeetingDTO() {}

        public MeetingDTO(Meeting meeting)
        {
            this.Id = meeting.Id;
            this.Name = meeting.Name;
            this.DateTime = meeting.DateTime;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
    }
}
