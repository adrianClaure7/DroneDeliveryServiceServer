using System.Collections.Generic;
using NUnit.Framework;
using DroneDeliveryServiceServer.Services;
using DroneDeliveryServiceServer.Models;

namespace DroneDeliveryServiceServer.Tests
{
    [TestFixture]
    public class DroneTripServiceTests
    {
        [Test]
        public void AssignLocationsToDrones_ShouldAssignLocationsToDrones()
        {
            // Arrange
            var droneTripService = new DroneTripService<Drone>();

            var drones = new List<Drone>
            {
                new Drone { DroneName = "DroneA", MaxWeight = 200 },
                new Drone { DroneName = "DroneB", MaxWeight = 250 },
                new Drone { DroneName = "DroneC", MaxWeight = 100 }
            };

            var locations = new List<Location>
            {
                new Location { Name = "LocationA", Weight = 200 },
                new Location { Name = "LocationB", Weight = 150 },
                new Location { Name = "LocationC", Weight = 50 },
                new Location { Name = "LocationD", Weight = 150 },
                new Location { Name = "LocationE", Weight = 100 },
                new Location { Name = "LocationF", Weight = 200 },
                new Location { Name = "LocationG", Weight = 50 },
                new Location { Name = "LocationH", Weight = 80 },
                new Location { Name = "LocationI", Weight = 70 },
                new Location { Name = "LocationJ", Weight = 50 },
                new Location { Name = "LocationK", Weight = 30 },
                new Location { Name = "LocationL", Weight = 20 },
                new Location { Name = "LocationM", Weight = 50 },
                new Location { Name = "LocationN", Weight = 30 },
                new Location { Name = "LocationO", Weight = 20 },
                new Location { Name = "LocationP", Weight = 90 }
            };

            var inputData = new Dictionary<string, object>
            {
                ["drones"] = drones,
                ["locations"] = locations
            };

            // Act
            var result = droneTripService.AssignLocationsToDrones(inputData);

            // Assert for Drones
            Assert.AreEqual(3, result.Count);

            // Assert for DroneB
            Assert.AreEqual("DroneB", result[0].DroneName);
            Assert.AreEqual(3, result[0].Trips.Count);
            Assert.AreEqual(2, result[0].Trips[0].Locations.Count);
            Assert.AreEqual(7, result[0].Trips[1].Locations.Count);
            Assert.AreEqual(1, result[0].Trips[2].Locations.Count);

            // Assert for DroneA
            Assert.AreEqual("DroneA", result[1].DroneName);
            Assert.AreEqual(3, result[1].Trips.Count);
            Assert.AreEqual(1, result[1].Trips[0].Locations.Count);
            Assert.AreEqual(1, result[1].Trips[1].Locations.Count);
            Assert.AreEqual(1, result[1].Trips[1].Locations.Count);

            // Assert for DroneC
            Assert.AreEqual("DroneC", result[2].DroneName);
            Assert.AreEqual(3, result[2].Trips.Count);
            Assert.AreEqual(1, result[2].Trips[0].Locations.Count);
            Assert.AreEqual(1, result[2].Trips[1].Locations.Count);
            Assert.AreEqual(1, result[2].Trips[1].Locations.Count);

            // Assert for sum of locations in all trips
            int totalLocations = locations.Count;
            int sumOfLocations = result.Sum(drone => drone.Trips.Sum(trip => trip.Locations.Count));
            Assert.AreEqual(totalLocations, sumOfLocations);
        }
    }
}
