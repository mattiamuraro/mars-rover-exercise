using MarsExercise.Shared.Constants;
using MarsExercise.Shared.Models;
using System;

namespace MarsExercise.Shared.Helpers
{
    /// <summary>
    /// Helper class to print on Console grid reppresentation
    /// </summary>
    public static class Printer
    {
        /// <summary>
        /// Print on console a rappresentation of current situation
        /// </summary>
        /// <param name="map">Grid map</param>
        /// <param name="xLength">Axis x length</param>
        /// <param name="yLength">Axis y length</param>
        /// <param name="start">Start path location</param>
        /// <param name="finish">Finish path location</param>
        /// <param name="vehicle">Vehicle location and orientation</param>
        internal static void Print(this bool[,] map, int xLength, int yLength, Location start, Location finish, Vehicle vehicle)
        {
            xLength.PrintFirstLine();

            for (var y = yLength; y > 0; y--)
            {
                var line = map.GetLine(y, xLength, start, finish, vehicle);
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Print head line 
        /// </summary>
        /// <param name="xLength">Axis x length</param>
        private static void PrintFirstLine(this int xLength)
        {
            var headLine = "  ";
            for (var x = 0; x < xLength; x++)
            {
                var printValue = "_ ";
                headLine = headLine + printValue;
            }
            Console.WriteLine(headLine);
        }

        /// <summary>
        /// Get line representation
        /// </summary>
        /// <param name="map">Grid map</param>
        /// <param name="y">Axis y</param>
        /// <param name="xLength">Axis x length</param>
        /// <param name="start">Start path location</param>
        /// <param name="finish">Finish path location</param>
        /// <param name="vehicle">Vehicle location and orientation</param>
        /// <returns>Line representation</returns>
        private static string GetLine(this bool[,] map, int y, int xLength, Location start, Location finish, Vehicle vehicle)
        {
            var line = " |";
            for (var x = 0; x < xLength; x++)
                line = line + map.GetCell(x, y, start, finish, vehicle) + "|";

            return line;
        }

        /// <summary>
        /// Get Cell representation
        /// </summary>
        /// <param name="map">Grid map</param>
        /// <param name="x">Axis x</param>
        /// <param name="y">Axis y</param>
        /// <param name="start">Start path location</param>
        /// <param name="finish">Finish path location</param>
        /// <param name="vehicle">Vehicle location and orientation</param>
        /// <returns>Ceòò representation</returns>
        private static string GetCell(this bool[,] map, int x, int y, Location start, Location finish, Vehicle vehicle)
        {
            if (vehicle?.GetLocation().CheckLocation(x, y - 1) ?? false)
                return vehicle.GetOrientation().GetStringOrientation();
            else if (start?.CheckLocation(x, y - 1) ?? false)
                return "S";
            else if (finish?.CheckLocation(x, y - 1) ?? false)
                return "F";
            else
                return DrawCell(map[y - 1, x]);
        }

        /// <summary>
        /// Get character to draw cell:
        /// - X if there is an obstacle
        /// - _ if it's an empty cell
        /// </summary>
        /// <param name="value">cell value</param>
        /// <returns>Cell representation</returns>
        private static string DrawCell(bool? value)
        {
            switch (value)
            {
                case true:
                    return "X";
                case false:
                    return "_";
                default:
                    return "*";
            }
        }

        /// <summary>
        /// Get character to draw vehicle orientation
        /// </summary>
        /// <param name="orientation">Vehicle orientation</param>
        /// <returns>Vehicle Orientation representation</returns>
        private static string GetStringOrientation(this string orientation)
        {
            switch (orientation)
            {
                case Direction.North:
                    return "^";
                case Direction.East:
                    return ">";
                case Direction.South:
                    return "V";
                case Direction.West:
                    return "<";
                default:
                    throw new Exception("Invalid orientation");
            }
        }

        /// <summary>
        /// Check if the location match with x and y axis value
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="x">Axis x</param>
        /// <param name="y">Axis y</param>
        /// <returns>boolean</returns>
        private static bool CheckLocation(this Location location, int x, int y)
        {
            return location.X == x && location.Y == y;
        }
    }
}
