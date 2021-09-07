using MarsExercise.Shared.Constants;
using MarsExercise.PathFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MarsExercise.Shared.Models;
using MarsExercise.PathFinder.Engines;

namespace MarsExercise.PathFinder
{
    /// <summary>
    /// Class that contains the engine to calculate the path to bring the vehicle from start to finish
    /// </summary>
    public class PathFinderEngine
    {
        /// <summary>
        /// Goal location
        /// </summary>
        private Location Finish { get; set; }

        /// <summary>
        /// Simulator environment
        /// </summary>
        private Simulator Simulator { get; set; }

        /// <summary>
        /// Register of already tried movements
        /// </summary>
        private ActionRegister Register { get; set; } = new ActionRegister();

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="planetXsize">Planet axis x size</param>
        /// <param name="planetYsize">Planet axis y size</param>
        /// <param name="start">Vehicle starting location</param>
        /// <param name="finish">Vehicle goal location</param>
        /// <param name="orientation">Vehicle orientation</param>
        public PathFinderEngine(int planetXsize, int planetYsize, Location start, Location finish, string orientation)
            : this(new Planet(planetXsize, planetYsize), start, finish, orientation)
        { }

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="planet">Planet configuration</param>
        /// <param name="start">Vehicle starting location</param>
        /// <param name="finish">Vehicle goal location</param>
        /// <param name="orientation">Vehicle orientation</param>
        public PathFinderEngine(Planet planet, Location start, Location finish, string orientation)
        {
            Simulator = new Simulator(planet, start, finish, orientation);
            Finish = finish;
        }

        /// <summary>
        /// Update vehicle location and orientation in the simulator
        /// The engine also set an obstacle in the potential next location
        /// </summary>
        /// <param name="vehicleLocation">Vehicle location</param>
        /// <param name="vehicleOrientation">Vehicle orientation</param>
        public void UpdateSimulator(Location vehicleLocation, string vehicleOrientation, char lastCommand)
        {
            //Set new vehicle start
            Simulator.SetVehicle(vehicleLocation, vehicleOrientation);

            //Set a obstacle in the position reported by mars rover
            SetObstacle(vehicleLocation, vehicleOrientation, lastCommand);

            //Reset registered actions to start a new simulation
            Register = new ActionRegister();
        }

        /// <summary>
        /// Set an obstacle in the reported position
        /// </summary>
        /// <param name="vehicleLocation">Location of vehicle before he reports the obstacle</param>
        /// <param name="vehicleOrientation">Orientation of vehicle before he reports the obstacle</param>
        /// <param name="lastCommand">Last command of vehicle before he reports the obstacle</param>
        private void SetObstacle(Location vehicleLocation, string vehicleOrientation, char lastCommand)
        {
            //if the last command is GoBackward the obstacle must be placed in the opposite vehicle direction 
            var orientation = lastCommand == Command.GoBackward ? vehicleOrientation.Opposite() : vehicleOrientation;
            var obstacle = vehicleLocation.GetNextLocation(orientation);
            Simulator.SetObstacle(obstacle);
        }

        /// <summary>
        /// Get commands to bring vehicle from current location to finish
        /// </summary>
        /// <returns>Try move result</returns>
        public TryMoveResult GetMarsCommands()
        {
            Simulator.PrintCurrentSituation();

            //Get the vehicle location
            var vehicleLocation = Simulator.GetVehicleLocation();
            
            //Get the vehicle orientation
            var vehicleOrientation = Simulator.GetVehicleOrientation();

            //Get movements to reach the goal
            var movements = GetMovements(vehicleLocation);

            //If the previous method return an empty dictionary the problem is unsolvable
            if (movements == null)
                return new TryMoveResult();

            //Convert movements into commands
            var commands = vehicleOrientation.ToCommands(movements);

            return new TryMoveResult(commands);
        }

        /// <summary>
        /// Get movements needed to reach the goal location
        /// </summary>
        /// <param name="vehicleLocation">Starting vehicle location</param>
        /// <returns>
        /// Dictionary with:
        /// - key composed by x + # + y of reached location
        /// - value movement executed
        /// </returns>
        private Dictionary<string, string> GetMovements(Location vehicleLocation)
        {
            // Dictionary created to store the movements needed to bring the vehicle simulator
            // from the start to the finish
            var movements = new Dictionary<string, string>();

            string previousDirection = null;

            var success = false;

            while (!success)
            {
                //Calculate next movement
                var direction = GetNextDirection(vehicleLocation, previousDirection);

                //Add direction to register and, if this is the fourth tried direction
                //It set a virtual obstacle to avoid pass on thi location again
                var full = Register.Add(vehicleLocation, direction);
                if (full)
                    Simulator.SetObstacle();

                //Try to move in the calculated direction
                var tryMoveResult = Simulator.TryGoPosition(direction);

                //If the movement has done correctly the engine:
                //- add commands to result list
                //- set new previous direction
                //- verify if goal is reached
                if (tryMoveResult.Success)
                {
                    previousDirection = direction;
                    success = Simulator.IsGoalReached();
                    vehicleLocation = Simulator.GetVehicleLocation();
                    movements.UpdateMovements(vehicleLocation, direction);
                }
                //If the problem is unsolvable the engine break the cicle
                //and communicate the result
                else if (IsUnsolvable(vehicleLocation))
                    break;
            }

            return success ? movements : null;
        }

        /// <summary>
        /// Get next vehicle direction based in particular on the number of already tried directions
        /// </summary>
        /// <param name="vehicleLocation">Current vehicle location</param>
        /// <param name="previousDirection">direction of previous movement</param>
        /// <returns>Direction</returns>
        private string GetNextDirection(Location vehicleLocation, string previousDirection)
        {
            var triedDirections = Register.GetActions(vehicleLocation);

            switch (triedDirections.Count)
            {
                case 0:
                    return GetDirectionWithNoBannedDirections(vehicleLocation, previousDirection);
                case 1:
                    return GetDirectionWitOneBannedDirection(vehicleLocation, previousDirection, triedDirections);
                case 2:
                    return GetDirectionWitTwoBannedDirections(vehicleLocation, previousDirection, triedDirections);
                case 3:
                    return GetDirectionWitThreeBannedDirections(triedDirections);
                default:
                    return previousDirection.Opposite();
            }
        }

        /// <summary>
        /// Get best direction with no direction already tried
        /// </summary>
        /// <param name="vehicleLocation">Current vehicle location</param>
        /// <param name="previousDirection">direction of previous movement</param>
        /// <returns>Direction</returns>
        private string GetDirectionWithNoBannedDirections(Location vehicleLocation, string previousDirection)
        {
            //The engine try to move on the axis with max distance from the goal
            //but if the sugested direction is tho opposite of previous direction
            //the engine abort the movement and try to move on the other axis

            if (Math.Abs(Finish.Y - vehicleLocation.Y) > Math.Abs(Finish.X - vehicleLocation.X))
            {
                if (vehicleLocation.Y > Finish.Y)
                {
                    if (previousDirection != Direction.North)
                        return Direction.South;
                }
                else
                {
                    if (previousDirection != Direction.South)
                        return Direction.North;
                }
            }

            if (vehicleLocation.X > Finish.X)
            {
                if (previousDirection != Direction.East)
                    return Direction.West;
            }
            else
            {
                if (previousDirection != Direction.West)
                    return Direction.East;
            }

            if (vehicleLocation.Y > Finish.Y)
                return Direction.South;
            else
                return Direction.North;
        }

        /// <summary>
        /// Get best direction with one direction already tried
        /// </summary>
        /// <param name="vehicleLocation">Current vehicle location</param>
        /// <param name="previousDirection">direction of previous movement</param>
        /// <param name="triedDirections">already tried directions</param>
        /// <returns>Direction</returns>
        private string GetDirectionWitOneBannedDirection(Location vehicleLocation, string previousDirection, List<string> triedDirections)
        {
            var directions = GetBannedDirections(triedDirections, previousDirection);

            //If there is only one banned direction the engine go on the other axis and on the direction that minimize the total distance
            if (directions.Count == 1)
            {
                if (triedDirections.Contains(Direction.East) || triedDirections.Contains(Direction.West))
                {
                    if (vehicleLocation.Y > Finish.Y)
                        return Direction.South;
                    else
                        return Direction.North;
                }
                else
                {
                    if (vehicleLocation.X > Finish.X)
                        return Direction.West;
                    else
                        return Direction.East;
                }
            }
            //If there are two banned directions and are on the same axis the engine move on the other axis and on the direction that minimize the total distance
            else if (directions.Contains(Direction.North) && directions.Contains(Direction.South))
            {
                if (vehicleLocation.X > Finish.X)
                    return Direction.West;
                else
                    return Direction.East;
            }
            else if (directions.Contains(Direction.West) && directions.Contains(Direction.East))
            {
                if (vehicleLocation.Y > Finish.Y)
                    return Direction.South;
                else
                    return Direction.North;
            }
            //If there are two banned directions and they are not on the same axis the engine move following the previous movement direction
            else
            {
                return previousDirection;
            }
        }

        /// <summary>
        /// Get the banned directionsconsidering already tried and the opposite of last movement to avoid go back if not really needed
        /// </summary>
        /// <param name="triedDirections">already tried directions</param>
        /// <param name="previousDirection">direction of previous movement</param>
        /// <returns>Banned directions</returns>
        private List<string> GetBannedDirections(List<string> triedDirections, string previousDirection)
        {
            var directions = new List<string>();

            foreach (var direction in triedDirections)
                directions.Add(direction);

            if (!string.IsNullOrEmpty(previousDirection))
            {
                var opposite = previousDirection.Opposite();

                if (!directions.Contains(opposite))
                    directions.Add(opposite);
            }

            return directions;
        }

        /// <summary>
        /// Get best direction with two directions already tried
        /// </summary>
        /// <param name="vehicleLocation">Current vehicle location</param>
        /// <param name="previousDirection">direction of previous movement</param>
        /// <param name="triedDirections">already tried directions</param>
        /// <returns>Direction</returns>
        private string GetDirectionWitTwoBannedDirections(Location vehicleLocation, string previousDirection, List<string> triedDirections)
        {
            //Get the available directions considering already tried and the opposite of last movement to avoid go back if not really needed
            var directions = triedDirections.GetAvailableDirections(previousDirection);

            //If there is only one available direction this is the answer
            if (directions.Count == 1)
                return directions.First();

            //If the two banned direction are north and south engine selects the direction that bring vehicle more near the goal between west and est
            if (triedDirections.Contains(Direction.North) && triedDirections.Contains(Direction.South))
            {
                if (vehicleLocation.X > Finish.X)
                    return Direction.West;
                else
                    return Direction.East;
            }
            //If the two banned directions are west and est engine selects the direction that bring vehicle more near the goal between south and north
            else if (triedDirections.Contains(Direction.West) && triedDirections.Contains(Direction.East))
            {
                if (vehicleLocation.Y > Finish.Y)
                    return Direction.South;
                else
                    return Direction.North;
            }
            //If the two banned directions are not on the same axis the engine decide to move on the axis with less distance from the goal
            else if (Math.Abs(Finish.Y - vehicleLocation.Y) > Math.Abs(Finish.X - vehicleLocation.X))
            {
                if (triedDirections.Contains(Direction.West))
                    return Direction.East;
                else
                    return Direction.West;
            }
            else if (triedDirections.Contains(Direction.North))
                return Direction.South;
            else
                return Direction.North;
        }

        /// <summary>
        /// Get best direction with three directions already tried
        /// </summary>
        /// <param name="triedDirections"></param>
        /// <returns>Direction</returns>
        private string GetDirectionWitThreeBannedDirections(List<string> triedDirections)
        {
            // if there are 3 already tried direction engine can only try the last one
            return triedDirections.GetAvailableDirections().First();
        }

        /// <summary>
        /// Verify if the problem is unsolvable.
        /// </summary>
        /// <param name="vehicleLocation"></param>
        /// <returns>Boolean</returns>
        private bool IsUnsolvable(Location vehicleLocation)
        {
            /// If the register contains more than 3 tried actions for current location means that
            /// the engine has tried all possibility and there is no solution
            return Register.GetActions(vehicleLocation).Count > 3;
        }
    }
}