using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outlook_Calendar_Test.Models
{
    public class CalendarEvent
    {
        public CalendarEvent()
        {
            this.Body = new Body()
            {
                ContentType = "html"
            };
            this.Start = new EventDateTime()
            {
                TimeZone = "Eastern Standard Time"
            };
            this.End = new EventDateTime()
            {
                TimeZone = "Eastern Standard Time"
            };
        }
        public string Subject { get; set; }

        public Body Body { get; set; }

        public EventDateTime Start { get; set; }

        public EventDateTime End { get; set; }


    }

    public class Body
    {
        public string ContentType { get; set; }

        public string Content { get; set; }
    }

    public class EventDateTime
    {
        public DateTime Datetime { get; set; }
        public string TimeZone { get; set; }
    }

}