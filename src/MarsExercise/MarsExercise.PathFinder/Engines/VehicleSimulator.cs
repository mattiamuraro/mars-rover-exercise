using MarsExercise.PathFinder.Models;
using MarsExercise.Shared.Constants;
using MarsExercise.Shared.Models;
using System;
using System.Collections.Generic;

namespace MarsExercise.PathFinder.Engines
{
    /// <summary>
    /// Class that represent the vehicle simulator used by Simulator
    /// </summary>
    internal class VehicleSimulator : Vehicle
    {
        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="planet">Planet entity</param>
        /// <param name="location">Vehicle location</param>
        /// <param name="orientation">Vehicle orientation</param>
        public VehicleSimulator(Planet planet, Location location, string orientation) : base(planet, location, orientation)
        { }

        /// <summary>
        /// Set vehicle location and orientation
        /// </summary>
        /// <param name="location">Vehicle location</param>
        /// <param name="orientation">Vehicle orientation</param>
        public void SetVehicle(Location location, string orientation)
        {
            Location = location;
            Orientation = orientation;
        }

        /// <summary>
        /// Try to move vehicle in a specific direction
        /// </summary>
        /// <param name="direction">Movement direction</param>
        /// <returns>Try move result</returns>
        public TryMoveResult TryGoPosition(string direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return TryGoNorth();
                case Direction.East:
                    return TryGoEst();
                case Direction.South:
                    return TryGoSouth();
                case Direction.West:
                    return TryGoWest();
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Verify if vehicle is in a specific location
        /// </summary>
        /// <param name="location">Check location</param>
        /// <returns>True if match exist</returns>
        public bool IsInLocation(Location location)
        {
            return Location.X == location.X && Location.Y == location.Y;
        }

        /// <summary>
        /// Try to move vehicle to north
        /// </summary>
        /// <returns>Try Move Result</returns>
        private TryMoveResult TryGoNorth()
        {
            switch (GetOrientation())
            {
                case Direction.North:
                    return TryGoForward();
                case Direction.East:
                    return TryGoLeft();
                case Direction.South:
                    return TryGoBackward();
                case Direction.West:
                    return TryGoRight();
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Try to move vehicle to est
        /// </summary>
        /// <returns>Try Move Result</returns>
        private TryMoveResult TryGoEst()
        {
            switch (GetOrientation())
            {
                case Direction.North:
                    return TryGoRight();
                case Direction.East:
                    return TryGoForward();
                case Direction.South:
                    return TryGoLeft();
                case Direction.West:
                    return TryGoBackward();
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Try to move vehicle to south
        /// </summary>
        /// <returns>Try Move Result</returns>
        private TryMoveResult TryGoSouth()
        {
            switch (GetOrientation())
            {
                case Direction.North:
                    return TryGoBackward();
                case Direction.East:
                    return TryGoRight();
                case Direction.South:
                    return TryGoForward();
                case Direction.West:
                    return TryGoLeft();
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Try to move vehicle to west
        /// </summary>
        /// <returns>Try Move Result</returns>
        private TryMoveResult TryGoWest()
        {
            switch (GetOrientation())
            {
                case Direction.North:
                    return TryGoLeft();
                case Direction.East:
                    return TryGoBackward();
                case Direction.South:
                    return TryGoRight();
                case Direction.West:
                    return TryGoForward();
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Try to move vehicle to forward
        /// </summary>
        /// <returns>Try Move Result</returns>
        private TryMoveResult TryGoForward()
        {
            var nextLocation = GetNextLocationForward();
            return TryMove(nextLocation, Orientation, new List<char> { Command.GoForward });
        }

        /// <summary>
        /// Try to move vehicle to right
        /// </summary>
        /// <returns>Try Move Result</returns>
        private TryMoveResult TryGoRight()
        {
            var nextLocation = GetNextLocationRight(out string nextOrientation);
            return TryMove(nextLocation, nextOrientation, new List<char> { Command.TurnRight, Command.GoForward });
        }

        /// <summary>
        /// Try to move vehicle to backward
        /// </summary>
        /// <returns>Try Move Result</returns>
        private TryMoveResult TryGoBackward()
        {
            var nextLocation = GetNextLocationBackward();
            return TryMove(nextLocation, Orientation, new List<char> { Command.GoBackward });
        }

        /// <summary>
        /// Try to move vehicle to left
        /// </summary>
        /// <returns>Try Move Result</returns>
        private TryMoveResult TryGoLeft()
        {
            var nextLocation = GetNextLocationLeft(out string nextOrientation);
            return TryMove(nextLocation, nextOrientation, new List<char> { Command.TurnLeft, Command.GoForward });
        }

        /// <summary>
        /// Try to move vehicle to a specific location.
        /// If location it's empty it return the commands related to the movement
        /// </summary>
        /// <param name="nextLocation">Next possible location</param>
        /// <param name="nextOrientation">Next possible orientation</param>
        /// <param name="commands">Commands releated</param>
        /// <returns>Try move result</returns>
        private TryMoveResult TryMove(Location nextLocation, string nextOrientation, List<char> commands)
        {
            if (Planet.ThereIsObstacle(nextLocation))
                return new TryMoveResult();

            Location = nextLocation;
            Orientation = nextOrientation;

            return new TryMoveResult(commands);
        }

        /// <summary>
        /// Get next location if vehicle goes right
        /// </summary>
        /// <param name="nextOrientation">Out paramenter to know next orientation</param>
        /// <returns>Location</returns>
        private Location GetNextLocationRight(out string nextOrientation)
        {
            switch (Orientation)
            {
                case Direction.North:
                    nextOrientation = Direction.East;
                    return new Location(Location.X + 1, Location.Y);
                case Direction.East:
                    nextOrientation = Direction.South;
                    return new Location(Location.X, Location.Y - 1);
                case Direction.South:
                    nextOrientation = Direction.West;
                    return new Location(Location.X - 1, Location.Y);
                case Direction.West:
                    nextOrientation = Direction.North;
                    return new Location(Location.X, Location.Y + 1);
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Get next location if vehicle goes left
        /// </summary>
        /// <param name="nextOrientation">Out paramenter to know next orientation</param>
        /// <returns>Location</returns>
        private Location GetNextLocationLeft(out string nextOrientation)
        {
            switch (Orientation)
            {
                case Direction.North:
                    nextOrientation = Direction.West;
                    return new Location(Location.X - 1, Location.Y);
                case Direction.East:
                    nextOrientation = Direction.North;
                    return new Location(Location.X, Location.Y + 1);
                case Direction.South:
                    nextOrientation = Direction.East;
                    return new Location(Location.X + 1, Location.Y);
                case Direction.West:
                    nextOrientation = Direction.South;
                    return new Location(Location.X, Location.Y - 1);
                default:
                    throw new Exception("Invalid direction");
            }
        }
    }
}
