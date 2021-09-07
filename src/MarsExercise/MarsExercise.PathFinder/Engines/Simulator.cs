using MarsExercise.PathFinder.Models;
using MarsExercise.Shared.Models;
using System;

namespace MarsExercise.PathFinder.Engines
{
    /// <summary>
    /// Class created to permit to Path finder engine to perform simulations
    /// to return the solution to the problem
    /// </summary>
    internal class Simulator
    {
        /// <summary>
        /// Planet
        /// </summary>
        private Planet Planet { get; set; }

        /// <summary>
        /// Vehicle simulator instance
        /// </summary>
        private VehicleSimulator Vehicle { get; set; }

        /// <summary>
        /// Goal location
        /// </summary>
        private Location Finish { get; set; }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="planet">Planet where vehicle is</param>
        /// <param name="start">Starting location</param>
        /// <param name="finish">Goal location</param>
        /// <param name="orientation">Vehicle orientation</param>
        public Simulator(Planet planet, Location start, Location finish, string orientation)
        {
            Planet = planet;
            Vehicle = new VehicleSimulator(planet, start, orientation);
            Finish = finish;
        }

        /// <summary>
        /// Set obstacle on virtual planet on vehicle current location
        /// </summary>
        public void SetObstacle()
        {
            Planet.SetObstacle(Vehicle.GetLocation());
        }

        /// <summary>
        /// Set obstacle on virtual planet on specific location
        /// </summary>
        /// <param name="location">Obstacle location</param>
        public void SetObstacle(Location location)
        {
            Planet.SetObstacle(location);
        }

        /// <summary>
        /// Configure vehicle by setting location and orientation
        /// </summary>
        /// <param name="location">Vehicle location</param>
        /// <param name="orientation">Vehicle orientation</param>
        public void SetVehicle(Location location, string orientation)
        {
            Vehicle.SetVehicle(location, orientation);
        }

        /// <summary>
        /// Try move vehicle on a direction
        /// </summary>
        /// <param name="direction">movement direction</param>
        /// <returns>Try move result</returns>
        public TryMoveResult TryGoPosition(string direction)
        {
            return Vehicle.TryGoPosition(direction);
        }

        /// <summary>
        /// Verify if vehicle reached the goal
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsGoalReached()
        {
            return Vehicle.IsInLocation(Finish);
        }

        /// <summary>
        /// Get current vehicle location
        /// </summary>
        /// <returns>Vehicle location</returns>
        public Location GetVehicleLocation()
        {
            return Vehicle.GetLocation();
        }

        /// <summary>
        /// Print on console a rappresentation of current simulator situation based on:
        /// - planet obstacles and size;
        /// - finish location;
        /// - vehicle location ad orientation
        /// </summary>
        public void PrintCurrentSituation()
        {
            Console.WriteLine();
            Console.WriteLine("#####SIMULATOR MAP#####");
            Planet.PrintCurrentSituation(finish: Finish, vehicle: Vehicle);
            Console.WriteLine("#####SIMULATOR MAP#####");
            Console.WriteLine();
        }
    }
}
