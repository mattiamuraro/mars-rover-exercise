using MarsExercise.Shared.Models;
using System.Collections.Generic;

namespace MarsExercise.PathFinder.Engines
{
    /// <summary>
    /// Class created to manage the register of already tried movements
    /// </summary>
    internal class ActionRegister
    {
        /// <summary>
        /// Dictionary to register already tried movements
        /// </summary>
        private Dictionary<string, List<string>> Actions { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Add a new tried movement.
        /// Of we have already tried all 4 movement it return true
        /// </summary>
        /// <param name="location">Vehicle location</param>
        /// <param name="direction">Movement direction</param>
        /// <returns>True if we have already tried all movement</returns>
        public bool Add(Location location, string direction) => Add(location.X, location.Y, direction);

        /// <summary>
        /// Add a new tried movement.
        /// Of we have already tried all 4 movement it return true
        /// </summary>
        /// <param name="x">Axis x vehicle location</param>
        /// <param name="y">Axis y vehicle location</param>
        /// <param name="direction">Movement direction</param>
        /// <returns>True if we have already tried all movement</returns>
        public bool Add(int x, int y, string direction)
        {
            var key = GetKey(x, y);

            if (Actions.TryGetValue(key, out List<string> directions))
            {
                directions.Add(direction);
                Actions.Remove(key);
                Actions.Add(key, directions);

                return directions.Count >= 4;
            }
            else
            {
                Actions.Add(key, new List<string> { direction });
                return false;
            }
        }

        /// <summary>
        /// Get already tried actions for a specific location
        /// </summary>
        /// <param name="location">Vehicle location</param>
        /// <returns>List of tried actions</returns>
        public List<string> GetActions(Location location)
        {
            if (Actions.TryGetValue(GetKey(location), out List<string> values))
                return values;

            return new List<string>();
        }

        /// <summary>
        /// Get dictionary key based on vehicle location
        /// </summary>
        /// <param name="location">Vehicle location</param>
        /// <returns>Dictionary key</returns>
        private string GetKey(Location location)
        {
            return GetKey(location.X, location.Y);
        }

        /// <summary>
        /// Get dictionary key based on vehicle x and y axis location
        /// </summary>
        /// <param name="x">Vehicle axis x location</param>
        /// <param name="y">Vehicle axis y location</param>
        /// <returns>Dictionary key</returns>
        private string GetKey(int x, int y)
        {
            return $"{x}#{y}";
        }
    }
}
