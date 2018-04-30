using System.Data.Entity;
using BookingService.Models;

namespace BookingService
{
    /// <summary>
    /// Class that defines the objects to save to tables in the database.
    /// Is inherited from Entity Frameworks DbContext class.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Meetings table that stores Meeting objects.
        /// </summary>
        public virtual DbSet<Meeting> Meetings { get; set; }
    }
}