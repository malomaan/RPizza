using System;
using System.Collections.Generic;
using System.Text;

namespace RPizza.Shared
{
    public class OrderWithStatus
    {
        public readonly static TimeSpan PreparationDuration
            = TimeSpan.FromSeconds(20);

        public readonly static TimeSpan DeliveryDuration
            = TimeSpan.FromSeconds(20);

        public Order Order { get; set; }

        public string StatusText { get; set; }

        //public List<Marker> MapMarkers { get; set; }


    }
}
