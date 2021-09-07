using MarsExercise.Shared.Models;

namespace MarsExercise.Rover.Models
{
    /// <summary>
    /// Class to return the result of Command execution
    /// </summary>
    public class VehicleCommandsExecutionResult
    {
        /// <summary>
        /// Success boolean
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Vehicle location
        /// </summary>
        public Location VehicleLocation { get; set; }

        /// <summary>
        /// Vehicle orientation
        /// </summary>
        public string VehicleOrientation { get; set; }

        /// <summary>
        /// Vehicle last command tried
        /// </summary>
        public char VehicleCommand { get; set; }

        /// <summary>
        /// Costructor with no parameters. It set Success as true.
        /// </summary>
        public VehicleCommandsExecutionResult() : this(true)
        { }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="success">Success boolean</param>
        public VehicleCommandsExecutionResult(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Costructor. It set Success as false.
        /// </summary>
        /// <param name="location">Vehicle location</param>
        /// <param name="orientation">Vehicle orientation</param>
        /// <param name="orientation">Vehicle last command tried</param>
        public VehicleCommandsExecutionResult(Location location, string orientation, char command)
        {
            Success = false;
            VehicleLocation = location;
            VehicleOrientation = orientation;
            VehicleCommand = command;
        }
    }
}
