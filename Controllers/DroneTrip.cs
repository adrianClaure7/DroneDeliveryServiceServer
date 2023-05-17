using Microsoft.AspNetCore.Mvc;
using DroneDeliveryServiceServer.Models;
using DroneDeliveryServiceServer.Services;
using System.Text.Json;

namespace DroneDeliveryServiceServer.Controllers
{
    [ApiController]
    [Route("api/assign-location-to-drones")]
    public class DroneTripController : ControllerBase
    {
        private readonly DroneTripService<DroneTrip> _dronTipService;

        public DroneTripController()
        {
            _dronTipService = new DroneTripService<DroneTrip>();
        }

        [HttpPost]
        public IActionResult GetAllItems(JsonDocument inputData)
        {
            var drones = new List<Drone>();
            var locations = new List<Location>();

            foreach (var droneElement in inputData.RootElement.GetProperty("drones").EnumerateArray())
            {
                var drone = new Drone
                {
                    DroneName = droneElement.GetProperty("droneName").GetString(),
                    MaxWeight = droneElement.GetProperty("maxWeight").GetInt32()
                };
                drones.Add(drone);
            }

            foreach (var locationElement in inputData.RootElement.GetProperty("locations").EnumerateArray())
            {
                var location = new Location
                {
                    Name = locationElement.GetProperty("name").GetString(),
                    Weight = locationElement.GetProperty("weight").GetInt32()
                };
                locations.Add(location);
            }

            var inputData2 = new Dictionary<string, object>
            {
                ["drones"] = drones,
                ["locations"] = locations
            };

            // Call the service method to assign locations to drones
            List<DroneTrip> items = _dronTipService.AssignLocationsToDrones(inputData2);

            return Ok(items);
        }
    }
}