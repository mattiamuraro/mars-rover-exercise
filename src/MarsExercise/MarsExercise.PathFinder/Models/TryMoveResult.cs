using System.Collections.Generic;

namespace MarsExercise.PathFinder.Models
{
    /// <summary>
    /// Class that represent movement try result
    /// </summary>
    public class TryMoveResult
    {
        /// <summary>
        /// Movement result
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Commands that must be executed to perform movement required
        /// </summary>
        public List<char> Commands { get; set; }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="success">Success boolean</param>
        /// <param name="commands">Commands to perform</param>
        public TryMoveResult(bool success, List<char> commands)
        {
            Success = success;
            Commands = commands;
        }

        /// <summary>
        /// Costructor without params.
        /// Set success to false
        /// </summary>
        public TryMoveResult()
        {
            Success = false;
            Commands = new List<char>();
        }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="success">Success boolean</param>
        public TryMoveResult(bool success)
        {
            Success = success;
            Commands = new List<char>();
        }

        /// <summary>
        /// Costructor.
        /// It sets success as true
        /// </summary>
        /// <param name="commands">Commands to perform</param>
        public TryMoveResult(List<char> commands)
        {
            Success = true;
            Commands = commands;
        }
    }
}
