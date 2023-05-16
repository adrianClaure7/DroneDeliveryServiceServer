using System;

namespace DroneDeliveryServiceServer.Models
{
    public class DroneTrip
    {
        public string DroneName { get; set; }
        public List<Trip> Trips { get; set; }
    }
}