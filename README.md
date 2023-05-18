# Drone Delivery Service API

The Drone Delivery Service API is a .NET 6 project that allows you to assign locations to drones in the most efficient way possible. The API exposes an endpoint that accepts a JSON payload containing drone and location data. It then processes the data and assigns the locations to the drones, optimizing the trips to maximize efficiency. The API is built with .NET 6 and can be launched locally at http://localhost:5188.

## Prerequisites

To run the Drone Delivery Service API locally, make sure you have the following dependencies installed:

- .NET 6 SDK: [Download .NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

## Getting Started

Follow these steps to get the Drone Delivery Service API up and running:

1. Clone the repository:

git clone https://github.com/your-username/your-repo.git

2. Navigate to the project directory:

cd DroneDeliveryServiceServer

3. Build the project:

dotnet build

4. Run the project:

dotnet run

5. The API will be available at:

http://localhost:5188

## API Endpoints

### Assign Locations to Drones

This endpoint accepts a POST request to assign locations to drones.

- URL: `/api/assign-location-to-drones`
- Method: POST
- Request Body: JSON payload containing drone and location data
- Response: JSON object representing the assigned trips for each drone

Example Request:

POST http://localhost:5188/api/assign-location-to-drones
Content-Type: application/json
{
    "drones": [
        {
            "droneName": "DroneA",
            "maxWeight": 200
        },
        {
            "droneName": "DroneB",
            "maxWeight": 250
        },
        {
            "droneName": "DroneC",
            "maxWeight": 100
        }
    ],
    "locations": [
        {
            "name": "LocationA",
            "weight": 200
        },
        {
            "name": "LocationB",
            "weight": 150
        },
        {
            "name": "LocationC",
            "weight": 50
        }
    ]
}

Example Response:
{
  "drones": [
    {
      "droneName": "DroneA",
      "trips": [
        {
          "tripNumber": 1,
          "locations": [
            {
              "locationName": "LocationA"
            }
          ]
        }
      ]
    },
    {
      "droneName": "DroneB",
      "trips": [
        {
          "tripNumber": 1,
          "locations": [
            {
              "locationName": "LocationB"
            },
            {
              "locationName": "LocationC"
            }
          ]
        }
      ]
    },
    {
      "droneName": "DroneC",
      "trips": []
    }
  ]
}


## Running Tests

To run the tests, execute the following command:

dotnet test ./Tests


## Credits

Development: Your Name