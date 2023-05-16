using Microsoft.AspNetCore.Mvc;
using DroneDeliveryServiceServer.Models;
using DroneDeliveryServiceServer.Services;

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

        [HttpGet]
        public IActionResult GetAllItems()
        {
            var inputData = new Dictionary<string, object>
            {
                ["drones"] = new List<Drone>
                {
                    new Drone { DroneName = "DroneA", MaxWeight = 300 },
                    new Drone { DroneName = "DroneB", MaxWeight = 350 },
                    new Drone { DroneName = "DroneC", MaxWeight = 200 }
                },
                ["locations"] = new List<Location>
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
                    new Location { Name = "LocationJ", Weight = 50 }
                }
            };
            // Call the function to assign locations to drones
            List<DroneTrip> items = _dronTipService.AssignLocationsToDrones(inputData);
            return Ok(items);
        }
    }
}
