using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyInvites.Models
{
    public class IndexModel
    {
        public string Greeting { get; set; }
        public DateTime CurrentTime { get; set; }

        public int TimeUntilTheParty
        {
            get
            {
                var timespan = (new DateTime(2017, 1,17, 20, 0, 0) - CurrentTime);
                return (int)Math.Round(timespan.TotalHours);
            }
        }
    }
}