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
            // Extract the drones and locations from the input data
            List<Drone> drones = ((List<Drone>)inputData["drones"])
                .OrderByDescending(drone => drone.MaxWeight)
                .ToList();

            List<Location> locations = ((List<Location>)inputData["locations"]).ToList();

            // Sort locations by weight in descending order
            locations = locations.OrderByDescending(location => location.Weight).ToList();

            List<DroneTrip> droneTrips = new List<DroneTrip>();

            // Iterate over each drone
            foreach (Drone drone in drones)
            {
                List<Trip> trips = new List<Trip>();
                List<TripLocation> currentTrip = new List<TripLocation>();
                int currentWeight = 0;
                int tripNumber = 1;

                // Iterate over each location and assign it to the drone's trip
                for (int j = 0; j < locations.Count; j++)
                {
                    Location location = locations[j];

                    // Check if the location's weight can be accommodated by the drone
                    if (location.Weight <= drone.MaxWeight - currentWeight)
                    {
                        // Add the location to the current trip
                        currentTrip.Add(new TripLocation { LocationName = location.Name });
                        currentWeight += location.Weight;

                        // Remove the assigned location from the list
                        locations.RemoveAt(j);
                        j--;
                    }

                    // Check if the current trip is complete (reached maximum weight or last location)
                    if (currentWeight == drone.MaxWeight || j == locations.Count - 1)
                    {
                        // Add the completed trip to the list of trips for the drone
                        trips.Add(new Trip { TripNumber = tripNumber, Locations = currentTrip });
                        tripNumber++;

                        // Reset the current trip and weight for the next iteration
                        currentTrip = new List<TripLocation>();
                        currentWeight = 0;
                    }
                }

                // Add the drone's trips to the droneTrips
                droneTrips.Add(new DroneTrip { DroneName = drone.DroneName, Trips = trips });
            }

            // If there are any remaining locations, assign them to the drones starting from the first drone
            if (locations.Count > 0)
            {
                int droneIndex = 0;

                // Iterate over each remaining location
                for (int j = 0; j < locations.Count; j++)
                {
                    Location location = locations[j];
                    Drone drone = drones[droneIndex];

                    // Create a new trip for the current drone if necessary
                    if (droneIndex >= droneTrips.Count)
                        droneTrips.Add(new DroneTrip { DroneName = drone.DroneName, Trips = new List<Trip>() });

                    // Assign the location to the current drone's trip
                    droneTrips[droneIndex].Trips.Add(new Trip
                    {
                        TripNumber = droneTrips[droneIndex].Trips.Count + 1,
                        Locations = new List<TripLocation> { new TripLocation { LocationName = location.Name } }
                    });

                    // Move to the next drone or go back to the first drone if all drones have been traversed
                    droneIndex = (droneIndex + 1) % drones.Count;
                }
            }

            return droneTrips;
        }


    }
}
