using System;
using System.Collections.Generic;
using DroneDeliveryServiceServer.Models;

namespace DroneDeliveryServiceServer.Services
{
    public class DroneTripService<DronTripModel>
    {
        private readonly List<DronTripModel> _items;

        public DroneTripService()
        {
            _items = new List<DronTripModel>();
        }

        public List<DroneTrip> AssignLocationsToDrones(Dictionary<string, object> inputData)
        {
            List<Drone> drones = ((List<Drone>)inputData["drones"])
                .OrderByDescending(drone => drone.MaxWeight)
                .ToList();

            List<Location> locations = ((List<Location>)inputData["locations"]).ToList();

            // Sort locations by weight in descending order
            locations = locations.OrderByDescending(location => location.Weight).ToList();

            List<DroneTrip> output = new List<DroneTrip>();

            foreach (Drone drone in drones)
            {
                List<Trip> trips = new List<Trip>();
                List<TripLocation> currentTrip = new List<TripLocation>();
                int currentWeight = 0;
                int tripNumber = 1;

                for (int j = 0; j < locations.Count; j++)
                {
                    Location location = locations[j];

                    if (location.Weight <= drone.MaxWeight - currentWeight)
                    {
                        currentTrip.Add(new TripLocation { LocationName = location.Name });
                        currentWeight += location.Weight;

                        locations.RemoveAt(j);
                        j--;
                    }

                    if (currentWeight == drone.MaxWeight || j == locations.Count - 1)
                    {
                        trips.Add(new Trip { TripNumber = tripNumber, Locations = currentTrip });
                        tripNumber++;

                        currentTrip = new List<TripLocation>();
                        currentWeight = 0;
                    }
                }

                output.Add(new DroneTrip { DroneName = drone.DroneName, Trips = trips });
            }

            return output;
        }
    }
}