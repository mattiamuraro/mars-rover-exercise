using System;
using System.Collections.Generic;

namespace MarsExercise.Shared.Constants
{
    /// <summary>
    /// Class that contains methods and constants releated to Cardinal directions
    /// </summary>
    public static class Direction
    {
        public const string North = "N";
        public const string East = "E";
        public const string South = "S";
        public const string West = "W";

        /// <summary>
        /// Get available directions by removing already tried and the previous one
        /// </summary>
        /// <param name="triedDirections">Already tried directions</param>
        /// <param name="previousDirection">Last movement direction</param>
        /// <returns>List of directions</returns>
        public static List<string> GetAvailableDirections(this List<string> triedDirections, string previousDirection = null)
        {
            var validDirections = new List<string> { North, East, South, West };
            if (!string.IsNullOrEmpty(previousDirection))
                validDirections.Remove(previousDirection.Opposite());

            foreach (var triedDirection in triedDirections)
                validDirections.Remove(triedDirection);

            return validDirections;
        }

        /// <summary>
        /// Get the opposite direction
        /// </summary>
        /// <param name="direction">Direction</param>
        /// <returns>Opposite direction</returns>
        public static string Opposite(this string direction)
        {
            switch (direction)
            {
                case North:
                    return South;
                case East:
                    return West;
                case South:
                    return North;
                case West:
                    return East;
                default:
                    throw new Exception("Invalid direction");
            }
        }
    }
}
