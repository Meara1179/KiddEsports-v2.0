using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Models
{
    public class Events
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public string EventDate { get; set; }

        public Events()
        {

        }
        public Events(int eventId, string eventName)
        {
            EventId = eventId;
            EventName = eventName;
        }

        public Events(string eventName, string eventLocation, string eventDate)
        {
            EventName = eventName;
            EventLocation = eventLocation;
            EventDate = eventDate;
        }

        public Events(int eventId, string eventName, string eventLocation, string eventDate)
        {
            EventId = eventId;
            EventName = eventName;
            EventLocation = eventLocation;
            EventDate = eventDate;
        }
    }
}
