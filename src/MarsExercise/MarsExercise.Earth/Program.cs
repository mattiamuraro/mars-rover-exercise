using MarsExercise.Orchestrator;
using MarsExercise.Shared.Constants;
using MarsExercise.Shared.Models;
using System;

namespace MarsExercise.Earth
{
    class Program
    {
        //Set movement timeout to see mars path in the console
        private const int movementTimeout = 200;

        static void Main(string[] args)
        {
            // Planet size configuration
            var planetXsize = 10;
            var planetYsize = 10;
            
            var planet = GetPlanet(planetXsize, planetYsize);

            // Vehicle start location
            var start = new Location(0, 3);

            // Vehicle orientation
            var orientation = Direction.East;

            // Vehicle goal location
            var finish = new Location(9, 1);



            Console.WriteLine("#####PLANET MAP#####");
            planet.PrintCurrentSituation(start, finish);
            Console.WriteLine("#####PLANET MAP#####");

            var orchestrator = new OrchestratorManager(planetXsize, planetYsize, planet, start, finish, orientation, movementTimeout);
            var result = orchestrator.DriveMars();
            Console.WriteLine(result ? "Goal reached" : "Goal not reachable");
        }

        /// <summary>
        /// Get Planet configuration with setted obstacles
        /// </summary>
        /// <param name="planetXsize">Axis x size</param>
        /// <param name="planetYsize">Axis y size</param>
        /// <returns>Planet</returns>
        private static Planet GetPlanet(int planetXsize, int planetYsize)
        {
            var planet = new Planet(planetXsize, planetYsize);

            planet.SetObstacle(0, 4);
            planet.SetObstacle(1, 3);

            //planet.SetObstacle(2, 3); //decomment to make the problem unsolvable

            planet.SetObstacle(3, 3);
            planet.SetObstacle(3, 4);
            planet.SetObstacle(3, 5);
            planet.SetObstacle(3, 6);
            planet.SetObstacle(3, 7);
            planet.SetObstacle(3, 8);
            planet.SetObstacle(4, 3);
            planet.SetObstacle(4, 8);
            planet.SetObstacle(5, 3);
            planet.SetObstacle(5, 5);
            planet.SetObstacle(6, 1);
            planet.SetObstacle(6, 2);
            planet.SetObstacle(6, 3);
            planet.SetObstacle(6, 8);
            planet.SetObstacle(7, 0);
            planet.SetObstacle(7, 4);
            planet.SetObstacle(7, 8);
            planet.SetObstacle(8, 4);
            planet.SetObstacle(8, 5);
            planet.SetObstacle(8, 6);
            planet.SetObstacle(8, 7);
            planet.SetObstacle(8, 8);

            return planet;
        }
    }
}
