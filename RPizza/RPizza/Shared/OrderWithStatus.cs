using RPizza.ComponentsLibrary.Map;
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
            = TimeSpan.FromMinutes(1);

        public Order Order { get; set; }

        public string StatusText { get; set; }

        public List<Marker> MapMarkers { get; set; }

        public static OrderWithStatus FromOrder(Order order)
        {
            string Message;
            List<Marker> Markers;
            var DispatchTime = order.CreatedTime.Add(PreparationDuration);
            if (DateTime.Now < DispatchTime)
            {
                Message = "Preparando";
                Markers = new List<Marker>
                {
                    ToMapMarker("Usted está aquí",
                    order.DeliveryLocation,showPopup:true)
                };
            }
            else if (DateTime.Now < DispatchTime + DeliveryDuration)
            {
                Message = "En Camino";
                var StartPosition = ComputeStartPosition(order);
                var ProportionOfDeliveryCompleted =
                    Math.Min(1,
                    (DateTime.Now - DispatchTime).TotalMilliseconds
                    / DeliveryDuration.TotalMilliseconds);

                var DriverPosition = LatLong.Interpolate(
                    StartPosition,
                    order.DeliveryLocation,
                    ProportionOfDeliveryCompleted);
                Markers = new List<Marker>
                {
                    ToMapMarker("Usted",order.DeliveryLocation),
                    ToMapMarker("Repartidor",DriverPosition,showPopup:true)
                };
            }
            else
            {
                Message = "Entregado";
                Markers = new List<Marker>
                {
                    ToMapMarker("Ubicación de entrega",
                    order.DeliveryLocation,showPopup:true)
                };
            }
            return new OrderWithStatus
            {
                Order = order,
                StatusText = Message,
                MapMarkers = Markers
            };
        }

        static Marker ToMapMarker(string description, LatLong coords,
            bool showPopup = false) => new Marker
            {

                Description = description,
                X = coords.Longitude,
                Y = coords.Latitude,
                ShowPopup = showPopup
            };

        static LatLong ComputeStartPosition(Order order) 
        {
            var Random = new Random(order.OrderId);
            var Distance = 0.01 + Random.NextDouble() * 0.02;
            var Angle = Random.NextDouble() * Math.PI * 2;
            var Offset =
                (Distance * Math.Cos(Angle),
                Distance * Math.Sin(Angle));
            return new LatLong(
                order.DeliveryLocation.Latitude + Offset.Item1,
                order.DeliveryLocation.Longitude + Offset.Item2);
        }

    }
}
