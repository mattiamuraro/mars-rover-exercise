using MarsExercise.Shared.Constants;
using MarsExercise.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MarsExercise.PathFinder.Tests
{
    [TestClass]
    public class PathFinderEngineTests
    {
        [TestMethod]
        public void UpdateSimulatorTestWithLastCommandGoForward()
        {
            var start = new Location(0, 0);
            var finish = new Location(4, 1);
            var pathFinderEngine = new PathFinderEngine(5, 5, start, finish, Direction.North);

            var newVehicleLocation = new Location(4, 2);
            var newVehicleOrientation = Direction.North;
            var lastCommand = Command.GoForward;

            pathFinderEngine.UpdateSimulator(newVehicleLocation, newVehicleOrientation, lastCommand);
            var result = pathFinderEngine.GetMarsCommands();

            Assert.IsTrue(result.Success && result.Commands.Count == 1 && result.Commands.Contains(Command.GoBackward));
        }


        [TestMethod]
        public void UpdateSimulatorTestWithLastCommandGoBackward()
        {
            var start = new Location(0, 0);
            var finish = new Location(4, 1);
            var pathFinderEngine = new PathFinderEngine(5, 5, start, finish, Direction.North);

            var newVehicleLocation = new Location(4, 2);
            var newVehicleOrientation = Direction.South;
            var lastCommand = Command.GoBackward;

            pathFinderEngine.UpdateSimulator(newVehicleLocation, newVehicleOrientation, lastCommand);
            var result = pathFinderEngine.GetMarsCommands();

            Assert.IsTrue(result.Success && result.Commands.Count == 1 && result.Commands.Contains(Command.GoForward));
        }

        [TestMethod]
        public void GetMarsCommandsTestEmptyPlanet()
        {
            var start = new Location(0, 0);
            var finish = new Location(4, 1);

            var expectedCommands = new List<char> {
                Command.TurnRight,
                Command.GoForward,
                Command.GoForward,
                Command.GoForward,
                Command.GoForward,
                Command.TurnLeft,
                Command.GoForward
            };

            var pathFinderEngine = new PathFinderEngine(5, 5, start, finish, Direction.North);
            var tryMoveResult = pathFinderEngine.GetMarsCommands();

            Assert.IsTrue(tryMoveResult.Success && MatchCommands(tryMoveResult.Commands, expectedCommands));
        }

        [TestMethod]
        public void GetMarsCommandsTestSingleObstaclePlanet()
        {
            var start = new Location(0, 0);
            var finish = new Location(4, 1);

            var planet = new Planet(5, 5);
            planet.SetObstacle(1, 0);

            var expectedCommands = new List<char> {
                Command.GoForward,
                Command.TurnRight,
                Command.GoForward,
                Command.GoForward,
                Command.GoForward,
                Command.GoForward
            };

            var pathFinderEngine = new PathFinderEngine(planet, start, finish, Direction.North);
            var tryMoveResult = pathFinderEngine.GetMarsCommands();

            Assert.IsTrue(tryMoveResult.Success && MatchCommands(tryMoveResult.Commands, expectedCommands));
        }

        [TestMethod]
        public void GetMarsCommandsTestMultipleObstaclesPlanet()
        {
            var start = new Location(0, 0);
            var finish = new Location(4, 1);

            var planet = new Planet(5, 5);
            planet.SetObstacle(1, 0);
            planet.SetObstacle(1, 1);

            var expectedCommands = new List<char> {
                Command.GoForward,
                Command.GoForward,
                Command.TurnRight,
                Command.GoForward,
                Command.GoForward,
                Command.GoForward,
                Command.GoForward,
                Command.TurnRight,
                Command.GoForward
            };

            var pathFinderEngine = new PathFinderEngine(planet, start, finish, Direction.North);
            var tryMoveResult = pathFinderEngine.GetMarsCommands();

            Assert.IsTrue(tryMoveResult.Success && MatchCommands(tryMoveResult.Commands, expectedCommands));
        }

        [TestMethod]
        public void GetMarsCommandsTesNoSolutionPlanet()
        {
            var start = new Location(0, 0);
            var finish = new Location(4, 1);

            var planet = new Planet(5, 5);
            planet.SetObstacle(2, 0);
            planet.SetObstacle(2, 1);
            planet.SetObstacle(2, 2);
            planet.SetObstacle(2, 3);
            planet.SetObstacle(2, 4);

            var pathFinderEngine = new PathFinderEngine(planet, start, finish, Direction.North);
            var tryMoveResult = pathFinderEngine.GetMarsCommands();

            Assert.IsFalse(tryMoveResult.Success);
        }

        private bool MatchCommands(List<char> first, List<char> second)
        {
            if (first.Count != second.Count)
                return false;

            for (int i = 0; i < first.Count; i++)
            {
                if (first[i] != second[i])
                    return false;
            }

            return true;
        }
    }
}

