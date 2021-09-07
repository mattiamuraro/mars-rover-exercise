using MarsExercise.Shared.Helpers;
using System;

namespace MarsExercise.Shared.Models
{
    /// <summary>
    /// Class to rappresent planet as a bidimensional array
    /// </summary>
    public class Planet
    {
        /// <summary>
        /// Bidimensional array that rappresent the planet map.
        /// Where the value is true it means there is an obstacle
        /// </summary>
        private bool[,] Map { get; set; }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="x">Axis x grid size</param>
        /// <param name="y">Axis y grid size</param>
        public Planet(int x, int y)
        {
            if (x < 0 || y < 0)
                throw new Exception("Invalid grid size");

                Map = new bool[y, x];
        }

        /// <summary>
        /// Set an obstacle on the map
        /// </summary>
        /// <param name="x">Axis x obstacle location</param>
        /// <param name="y">Axis y obstacle location</param>
        public void SetObstacle(int x, int y)
        {
            if (!IsOutOfBounds(x, y))
                Map[y, x] = true;
        }

        /// <summary>
        /// Set an obstacle on the map
        /// </summary>
        /// <param name="location">Obstacle locaiton</param>
        public void SetObstacle(Location location)
        {
            SetObstacle(location.X, location.Y);
        }

        /// <summary>
        /// Verify if there is an obstacle in a defined location.
        /// Return true if obstacle is present
        /// </summary>
        /// <param name="location"></param>
        /// <returns>boolean</returns>
        public bool ThereIsObstacle(Location location) => ThereIsObstacle(location.X, location.Y);

        /// <summary>
        /// Verify if there is an obstacle in a defined location.
        /// </summary>
        /// <param name="x">Axis x location</param>
        /// <param name="y">Axis y location</param>
        /// <returns>boolean</returns>
        public bool ThereIsObstacle(int x, int y)
        {
            return IsOutOfBounds(x, y) || Map[y, x];
        }

        /// <summary>
        /// Verify if the location is out of gird bounds
        /// </summary>
        /// <param name="x">Axis x location</param>
        /// <param name="y">Axis y location</param>
        /// <returns>boolean</returns>
        private bool IsOutOfBounds(int x, int y)
        {
            var xLength = Map.GetLength(1);
            var yLength = Map.GetLength(0);

            return y < 0 || x < 0 || y >= yLength || x >= xLength;
        }

        /// <summary>
        /// Print on console a rappresentation of current situation based on:
        /// - planet obstacles and size;
        /// - start location;
        /// - finish location;
        /// - vehicle location ad orientation
        /// </summary>
        /// <param name="start">Start path location</param>
        /// <param name="finish">Finish path location</param>
        /// <param name="vehicle">Vehicle current location</param>
        public void PrintCurrentSituation(Location start = null, Location finish = null, Vehicle vehicle = null)
        {
            var xLength = GetXLength();
            var yLength = GetYLength();

            Map.Print(xLength, yLength, start, finish, vehicle);
        }

        /// <summary>
        /// Get axis x length
        /// </summary>
        /// <returns>Integer</returns>
        private int GetXLength()
        {
            return Map.GetLength(1);
        }

        /// <summary>
        /// Get axis y length
        /// </summary>
        /// <returns>Integer</returns>
        private int GetYLength()
        {
            return Map.GetLength(0);
        }
    }
}
