using MarsExercise.Shared.Constants;
using System;
namespace MarsExercise.Shared.Models
{
    /// <summary>
    /// Class to rappresent a position in a bidimensional grid
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Axis x value
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Axis y value
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="x">Axis x location</param>
        /// <param name="y">Axis y location</param>
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Get lotaion after moving in a specific direction
        /// </summary>
        /// <param name="direction">movement direction</param>
        /// <returns>Location</returns>
        public Location GetNextLocation(string direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Location(X, Y + 1);
                case Direction.East:
                    return new Location(X + 1, Y);
                case Direction.South:
                    return new Location(X, Y - 1);
                case Direction.West:
                    return new Location(X - 1, Y);
                default:
                    throw new Exception("Invalid direction");
            }
        }
    }
}
