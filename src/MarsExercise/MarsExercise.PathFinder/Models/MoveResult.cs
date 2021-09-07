using System.Collections.Generic;

namespace MarsExercise.PathFinder.Models
{
    /// <summary>
    /// Class that represent movement result
    /// </summary>
    public class MoveResult
    {
        /// <summary>
        /// Orientation of Vehicle after required movement
        /// </summary>
        public string Orientation { get; set; }

        /// <summary>
        /// Commands that must be executed to perform movement required
        /// </summary>
        public List<char> Commands { get; set; }
    }
}
