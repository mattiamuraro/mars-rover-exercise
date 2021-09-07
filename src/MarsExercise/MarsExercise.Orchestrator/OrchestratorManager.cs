using MarsExercise.PathFinder;
using MarsExercise.Rover;
using MarsExercise.Shared.Constants;
using MarsExercise.Shared.Models;

namespace MarsExercise.Orchestrator
{
    /// <summary>
    /// Class created to orchestrate communication between Earth, Mars and Pathfinder engine
    /// </summary>
    public class OrchestratorManager
    {
        /// <summary>
        /// Connector with Mars Rover
        /// </summary>
        private RoverManager Mars { get; set; }

        /// <summary>
        /// Connector with Pathfinder engine
        /// </summary>
        private PathFinderEngine PathFinderEngine { get; set; }


        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="planetXsize">Planet axis x size</param>
        /// <param name="planetYsize">Planet axis y size</param>
        /// <param name="planet">Planet</param>
        /// <param name="start">Vehicle starting location</param>
        /// <param name="finish">Vehicle goal location</param>
        /// <param name="orientation">Vehicle orientation</param>
        public OrchestratorManager(int planetXsize, int planetYsize, Planet planet, Location start, Location finish, string orientation = Direction.North, int movementTimeout = 0)
        {
            PathFinderEngine = new PathFinderEngine(planetXsize, planetYsize, start, finish, orientation);
            Mars = new RoverManager(planet, start, orientation, movementTimeout);
        }

        /// <summary>
        /// Manage communication between actor involved until mars reach the goal or the path finder engine proof that the problem is unsolvable
        /// </summary>
        /// <returns>Success boolean</returns>
        public bool DriveMars()
        {
            while (true)
            {
                var result = PathFinderEngine.GetMarsCommands();
                if (!result.Success)
                    return false;

                var executionResult = Mars.ExceuteCommands(result.Commands);
                if (executionResult.Success)
                    return true;

                PathFinderEngine.UpdateSimulator(executionResult.VehicleLocation, executionResult.VehicleOrientation, executionResult.VehicleCommand);
            }
        }
    }
}
