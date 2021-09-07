using MarsExercise.Shared.Constants;
using System;

namespace MarsExercise.Shared.Models
{
    /// <summary>
    /// Class to manage based vehicle interactions
    /// </summary>
    public abstract class Vehicle
    {
        /// <summary>
        /// Vehicle location
        /// </summary>
        protected Location Location { get; set; }

        /// <summary>
        /// Vehicle orientation (N, S, E and W)
        /// </summary>
        protected string Orientation { get; set; }

        /// <summary>
        /// Planet object based on bidimensional grid
        /// </summary>
        protected Planet Planet { get; set; }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="planet">Planet where vehicle is</param>
        /// <param name="location">Vehicle location</param>
        public Vehicle(Planet planet, Location location) : this(location.X, location.Y, planet)
        { }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="planet">Planet where vehicle is</param>
        /// <param name="location">Vehicle location</param>
        /// <param name="orientation">vehicle orientation</param>
        public Vehicle(Planet planet, Location location, string orientation) : this(location.X, location.Y, orientation, planet)
        { }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="x">Axis x vehicle location</param>
        /// <param name="y">Axis y vehicle location</param>
        /// <param name="planet">Planet where vehicle is</param>
        private Vehicle(int x, int y, Planet planet) : this(x, y, Direction.North, planet)
        { }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="x">Axis x vehicle location</param>
        /// <param name="y">Axis y vehicle location</param>
        /// <param name="orientation">vehicle orientation</param>
        /// <param name="planet">Planet where vehicle is</param>
        private Vehicle(int x, int y, string orientation, Planet planet)
        {
            Location = new Location(x, y);
            Orientation = orientation;
            Planet = planet;
        }

        /// <summary>
        /// Get current Vehicle orientation
        /// </summary>
        /// <returns>Vehicle orientation</returns>
        public string GetOrientation()
        {
            return Orientation;
        }

        /// <summary>
        /// Get current Vehicle location
        /// </summary>
        /// <returns>Vehicle location</returns>
        public Location GetLocation()
        {
            return Location;
        }

        /// <summary>
        /// Get next location if vehicle move forward
        /// </summary>
        /// <returns>Vehicle location</returns>
        protected Location GetNextLocationForward()
        {
            switch (Orientation)
            {
                case Direction.North:
                    return new Location(Location.X, Location.Y + 1);
                case Direction.East:
                    return new Location(Location.X + 1, Location.Y);
                case Direction.South:
                    return new Location(Location.X, Location.Y - 1);
                case Direction.West:
                    return new Location(Location.X - 1, Location.Y);
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Get next location if vehicle move backward
        /// </summary>
        /// <returns>Vehicle location</returns>
        protected Location GetNextLocationBackward()
        {
            switch (Orientation)
            {
                case Direction.North:
                    return new Location(Location.X, Location.Y - 1);
                case Direction.East:
                    return new Location(Location.X - 1, Location.Y);
                case Direction.South:
                    return new Location(Location.X, Location.Y + 1);
                case Direction.West:
                    return new Location(Location.X + 1, Location.Y);
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Get Vehicle orientation if it turn right
        /// </summary>
        /// <returns>Vehicle orientation</returns>
        protected string GetOrientationAftetrTurnRight()
        {
            switch (Orientation)
            {
                case Direction.North:
                    return Direction.East;
                case Direction.East:
                    return Direction.South;
                case Direction.South:
                    return Direction.West;
                case Direction.West:
                    return Direction.North;
                default:
                    throw new Exception("Invalid orientation");
            }
        }

        /// <summary>
        /// Get Vehicle orientation if it turn left
        /// </summary>
        /// <returns>Vehicle orientation</returns>
        protected string GetOrientationAftetrTurnLeft()
        {
            switch (Orientation)
            {
                case Direction.North:
                    return Direction.West;
                case Direction.East:
                    return Direction.North;
                case Direction.South:
                    return Direction.East;
                case Direction.West:
                    return Direction.South;
                default:
                    throw new Exception("Invalid orientation");
            }
        }
    }
}
