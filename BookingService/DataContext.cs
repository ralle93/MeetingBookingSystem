using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookingService
{
    public class DataContext : DbContext
    {
        public DbSet<Meeting> Meetings { get; set; }
    }

    public class Meeting
    {
        public Meeting() {}

        public Meeting(MeetingDTO meeting)
        {
            this.Id = meeting.Id;
            this.Name = meeting.Name;
            this.DateTime = meeting.DateTime;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}