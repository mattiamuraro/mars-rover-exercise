using MarsExercise.PathFinder.Models;
using MarsExercise.Shared.Constants;
using MarsExercise.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsExercise.PathFinder
{
    /// <summary>
    /// Class to define some useful static methods
    /// </summary>
    internal static class ExtensionMethods
    {
        /// <summary>
        /// It updates the movement dictionary with the new item.
        /// If the reached location is already in the dictionary 
        /// it means that the vehicle has already passed through this location 
        /// then the method remove all the movements after this first pass through
        /// because they are useless.
        /// Else, if it's the first time the vehicle pass through this location,
        /// the method add the movement to the dictionary
        /// </summary>
        /// <param name="movements">Dictionary created to store the movements needed
        /// to bring the vehicle simulator from the start to the finish
        /// </param>
        /// <param name="vehicleLocation">Vehicle location</param>
        /// <param name="direction">movement direction</param>
        public static void UpdateMovements(this Dictionary<string, string> movements, Location vehicleLocation, string direction)
        {
            var key = vehicleLocation.GetKey();

            if (movements.TryGetValue(key, out string _))
            {
                while (true)
                {
                    var movement = movements.Last();
                    if (movement.Key == key)
                        break;
                    movements.Remove(movement.Key);
                }
            }
            else
                movements.Add(key, direction);
        }

        /// <summary>
        /// Cast simulator movement Dictionary into a list of commands ready to be send to the Mars
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <param name="movements">Simulator movement Dictionary</param>
        /// <returns>Listo of Mars commands</returns>
        public static List<char> ToCommands(this string orientation, Dictionary<string, string> movements)
        {
            var commands = new List<char>();

            foreach (var movement in movements)
            {
                var result = orientation.ToCommandsResult(movement.Value);
                orientation = result.Orientation;
                commands.AddRange(result.Commands);
            }

            return commands;
        }

        /// <summary>
        /// Get movement dictionary key for a specific location
        /// </summary>
        /// <param name="location">Location</param>
        /// <returns>Key</returns>
        private static string GetKey(this Location location)
        {
            return location.X + "#" + location.Y;
        }

        /// <summary>
        /// Cast a simulator movement into a MoveResult
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <param name="movement">Vehicle movement</param>
        /// <returns>MoveResult</returns>
        private static MoveResult ToCommandsResult(this string orientation, string movement)
        {
            switch (movement)
            {
                case Direction.North:
                    return orientation.GoNorth();
                case Direction.East:
                    return orientation.GoEast();
                case Direction.South:
                    return orientation.GoSouth();
                case Direction.West:
                    return orientation.GoWest();
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Get MoveResult to move the simulator vehicle north
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>MoveResult</returns>
        private static MoveResult GoNorth(this string orientation)
        {
            switch (orientation)
            {
                case Direction.North:
                    return orientation.GoForward();
                case Direction.East:
                    return orientation.GoLeft();
                case Direction.South:
                    return orientation.GoBackward();
                case Direction.West:
                    return orientation.GoRight();
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Get MoveResult to move the simulator vehicle east
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>MoveResult</returns>
        private static MoveResult GoEast(this string orientation)
        {
            switch (orientation)
            {
                case Direction.North:
                    return orientation.GoRight();
                case Direction.East:
                    return orientation.GoForward();
                case Direction.South:
                    return orientation.GoLeft();
                case Direction.West:
                    return orientation.GoBackward();
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Get MoveResult to move the simulator vehicle south
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>MoveResult</returns>
        private static MoveResult GoSouth(this string orientation)
        {
            switch (orientation)
            {
                case Direction.North:
                    return orientation.GoBackward();
                case Direction.East:
                    return orientation.GoRight();
                case Direction.South:
                    return orientation.GoForward();
                case Direction.West:
                    return orientation.GoLeft();
                default:
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Get MoveResult to move the simulator vehicle west
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>MoveResult</returns>
        private static MoveResult GoWest(this string orientation)
        {
            switch (orientation)
            {
                case Direction.North:
                    return orientation.GoLeft();
                case Direction.East:
                    return orientation.GoBackward();
                case Direction.South:
                    return orientation.GoRight();
                case Direction.West:
                    return orientation.GoForward();
                default:
                    throw new Exception("Invalid direction");
            }
        }


        /// <summary>
        /// Get MoveResult to move the simulator vehicle forward
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>MoveResult</returns>
        private static MoveResult GoForward(this string orientation)
        {
            return new MoveResult
            {
                Orientation = orientation,
                Commands = new List<char> { Command.GoForward }
            };
        }

        /// <summary>
        /// Get MoveResult to move the simulator vehicle on the right
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>MoveResult</returns>
        private static MoveResult GoRight(this string orientation)
        {
            return new MoveResult
            {
                Orientation = orientation.GetNextOrientationRight(),
                Commands = new List<char> { Command.TurnRight, Command.GoForward }
            };
        }

        /// <summary>
        /// Get MoveResult to move the simulator vehicle backward
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>MoveResult</returns>
        private static MoveResult GoBackward(this string orientation)
        {
            return new MoveResult
            {
                Orientation = orientation,
                Commands = new List<char> { Command.GoBackward }
            };
        }

        /// <summary>
        /// Get MoveResult to move the simulator vehicle on the left
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>MoveResult</returns>
        private static MoveResult GoLeft(this string orientation)
        {
            return new MoveResult
            {
                Orientation = orientation.GetNextOrientationLeft(),
                Commands = new List<char> { Command.TurnLeft, Command.GoForward }
            };
        }

        /// <summary>
        /// Get the next orientation if the vehicle turn right
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>Orientation</returns>
        private static string GetNextOrientationRight(this string orientation)
        {
            switch (orientation)
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
                    throw new Exception("Invalid direction");
            }
        }

        /// <summary>
        /// Get the next orientation if the vehicle turn left
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>Orientation</returns>
        private static string GetNextOrientationLeft(this string orientation)
        {
            switch (orientation)
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
                    throw new Exception("Invalid direction");
            }
        }
    }
}
