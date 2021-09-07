using MarsExercise.Rover.Models;
using MarsExercise.Shared.Constants;
using MarsExercise.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MarsExercise.Rover
{
    /// <summary>
    /// Class that represent the Mars Rover movement manager
    /// </summary>
    public class RoverManager : Vehicle
    {
        private int tryCounter = 0;

        private int MovementTimeout { get; set; }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="planet">Planet where vehicle is</param>
        /// <param name="location">Vehicle location</param>
        public RoverManager(Planet planet, Location location, int movementTimeout = 0) : base(planet, location)
        {
            MovementTimeout = movementTimeout;
        }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="planet">Planet where vehicle is</param>
        /// <param name="location">Vehicle location</param>
        /// <param name="orientation">vehicle orientation</param>
        public RoverManager(Planet planet, Location location, string orientation, int movementTimeout = 0) : base(planet, location, orientation)
        {
            MovementTimeout = movementTimeout;
        }

        /// <summary>
        /// Execute a list of movement commands
        /// </summary>
        /// <param name="commands">List of commands</param>
        /// <returns>Commands Exceution Result</returns>
        public VehicleCommandsExecutionResult ExceuteCommands(List<char> commands)
        {
            Console.WriteLine($"Commands comunication {++tryCounter}: " + GetCommandsString(commands));

            foreach (var command in commands)
            {
                var result = ExceuteCommand(command);
                
                //added to make command line output readable
                Thread.Sleep(MovementTimeout); 
                
                Planet.PrintCurrentSituation(vehicle: this);
                if (!result.Success)
                {
                    Console.WriteLine("command: " + command + " - failed");
                    return result;
                }
                else
                    Console.WriteLine("command: " + command + " - success");
            }

            return new VehicleCommandsExecutionResult();
        }

        /// <summary>
        /// Execute a movement command
        /// </summary>
        /// <param name="command">Movement command</param>
        /// <returns>Commands Exceution Result</returns>
        private VehicleCommandsExecutionResult ExceuteCommand(char command)
        {
            switch (command)
            {
                case Command.TurnLeft:
                    TurnLeft();
                    break;
                case Command.TurnRight:
                    TurnRight();
                    break;
                case Command.GoForward:
                    if (!TryGoForward())
                        return new VehicleCommandsExecutionResult(Location, Orientation, Command.GoForward);
                    break;
                case Command.GoBackward:
                    if (!TryGoBackward())
                        return new VehicleCommandsExecutionResult(Location, Orientation, Command.GoBackward);
                    break;
            }
            return new VehicleCommandsExecutionResult();
        }

        /// <summary>
        /// Turn Mars Rover right
        /// </summary>
        private void TurnRight()
        {
            Orientation = GetOrientationAftetrTurnRight();
        }

        /// <summary>
        /// Turn Mars Rover left
        /// </summary>
        private void TurnLeft()
        {
            Orientation = GetOrientationAftetrTurnLeft();
        }

        /// <summary>
        /// Move Mars Rover forward
        /// </summary>
        /// <returns></returns>
        private bool TryGoForward()
        {
            var nextLocation = GetNextLocationForward();
            return TryMove(nextLocation);
        }

        /// <summary>
        /// Move Mars Rover Backward
        /// </summary>
        /// <returns></returns>
        private bool TryGoBackward()
        {
            var nextLocation = GetNextLocationBackward();
            return TryMove(nextLocation);
        }

        /// <summary>
        /// Try to execute a movement.
        /// Return false if there is an obstacle on next location
        /// </summary>
        /// <param name="nextLocation">Next location</param>
        /// <returns>booleand</returns>
        private bool TryMove(Location nextLocation)
        {
            if (Planet.ThereIsObstacle(nextLocation))
                return false;

            Location = nextLocation;
            return true;
        }

        /// <summary>
        /// Get the string of all commands
        /// </summary>
        /// <param name="commands">List of commands</param>
        /// <returns>string of all commands</returns>
        private string GetCommandsString(List<char> commands)
        {
            var commandsString = "";
            foreach (var command in commands)
            {
                if (!string.IsNullOrEmpty(commandsString))
                    commandsString = commandsString + " > ";
                commandsString = commandsString + command;
            }

            return commandsString;
        }
    }
}
