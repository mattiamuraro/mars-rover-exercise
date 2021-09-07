using MarsExercise.Shared.Constants;
using MarsExercise.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MarsExercise.Rover.Tests
{
    [TestClass]
    public class RoverManagerTests
    {
        [TestMethod]
        public void GetLocationTest()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location);

            var roverLocation = roverManager.GetLocation();

            Assert.IsTrue(location.X == roverLocation.X && location.Y == location.Y);
        }

        [TestMethod]
        public void GetOrientation()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.South);

            var roverOrientation = roverManager.GetOrientation();

            Assert.IsTrue(roverOrientation == Direction.South);
        }

        [TestMethod]
        public void ExceuteCommandsTestTurnLeftStartingNorth()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.North);
            var commands = new List<char> { Command.TurnLeft };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.West);
        }

        [TestMethod]
        public void ExceuteCommandsTestTurnLeftStartingEst()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.East);
            var commands = new List<char> { Command.TurnLeft };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.North);
        }

        [TestMethod]
        public void ExceuteCommandsTestTurnLeftStartinSouth()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.South);
            var commands = new List<char> { Command.TurnLeft };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.East);
        }

        [TestMethod]
        public void ExceuteCommandsTestTurnLeftStartingWest()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.West);
            var commands = new List<char> { Command.TurnLeft };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.South);
        }

        [TestMethod]
        public void ExceuteCommandsTestTurnRightStartingNorth()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.North);
            var commands = new List<char> { Command.TurnRight };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.East);
        }

        [TestMethod]
        public void ExceuteCommandsTestTurnRightStartingEst()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.East);
            var commands = new List<char> { Command.TurnRight };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.South);
        }

        [TestMethod]
        public void ExceuteCommandsTestTurnRightStartinSouth()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.South);
            var commands = new List<char> { Command.TurnRight };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.West);
        }

        [TestMethod]
        public void ExceuteCommandsTestTurnRightStartingWest()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.West);
            var commands = new List<char> { Command.TurnRight };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.North);
        }


        [TestMethod]
        public void ExceuteCommandsTestGoForwardStartingNorth()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.North);
            var commands = new List<char> { Command.GoForward };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 2 && vehicleOrientation == Direction.North);
        }

        [TestMethod]
        public void ExceuteCommandsTestGoForwardStartingEst()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.East);
            var commands = new List<char> { Command.GoForward };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 2 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.East);
        }

        [TestMethod]
        public void ExceuteCommandsTestGoForwardStartinSouth()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.South);
            var commands = new List<char> { Command.GoForward };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 0 && vehicleOrientation == Direction.South);
        }

        [TestMethod]
        public void ExceuteCommandsTestGoForwardStartingWest()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.West);
            var commands = new List<char> { Command.GoForward };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 0 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.West);
        }

        [TestMethod]
        public void ExceuteCommandsTestGoBackwardStartingNorth()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.North);
            var commands = new List<char> { Command.GoBackward };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 0 && vehicleOrientation == Direction.North);
        }

        [TestMethod]
        public void ExceuteCommandsTestGoBackwardStartingEst()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.East);
            var commands = new List<char> { Command.GoBackward };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 0 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.East);
        }

        [TestMethod]
        public void ExceuteCommandsTestGoBackwardStartinSouth()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.South);
            var commands = new List<char> { Command.GoBackward };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 2 && vehicleOrientation == Direction.South);
        }

        [TestMethod]
        public void ExceuteCommandsTestGoBackwardStartingWest()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.West);
            var commands = new List<char> { Command.GoBackward };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 2 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.West);
        }


        [TestMethod]
        public void ExceuteCommandsTestGoOutOfBound()
        {
            var planet = new Planet(3, 3);
            var location = new Location(0, 0);
            var roverManager = new RoverManager(planet, location, Direction.North);
            var commands = new List<char> { Command.GoBackward };

            var result = roverManager.ExceuteCommands(commands);

            Assert.IsTrue(!result.Success && result.VehicleLocation.X == 0 && result.VehicleLocation.Y == 0 && result.VehicleOrientation == Direction.North && result.VehicleCommand == Command.GoBackward);
        }

        [TestMethod]
        public void ExceuteCommandsTestGoOnObstacle()
        {
            var planet = new Planet(3, 3);
            planet.SetObstacle(0, 1);
            var location = new Location(0, 0);
            var roverManager = new RoverManager(planet, location, Direction.North);
            var commands = new List<char> { Command.GoForward };

            var result = roverManager.ExceuteCommands(commands);

            Assert.IsTrue(!result.Success && result.VehicleLocation.X == 0 && result.VehicleLocation.Y == 0 && result.VehicleOrientation == Direction.North && result.VehicleCommand == Command.GoForward);
        }

        [TestMethod]
        public void ExceuteCommandsTestWithMultipleComands()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.North);
            var commands = new List<char> { Command.GoForward, Command.TurnRight, Command.GoForward };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 2 && vehicleLocation.Y == 2 && vehicleOrientation == Direction.East);
        }

        [TestMethod]
        public void ExceuteCommandsTestWithInvalidDirection()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            var roverManager = new RoverManager(planet, location, Direction.North);
            var commands = new List<char> { 'i' };

            var result = roverManager.ExceuteCommands(commands);
            var vehicleLocation = roverManager.GetLocation();
            var vehicleOrientation = roverManager.GetOrientation();

            Assert.IsTrue(result.Success && vehicleLocation.X == 1 && vehicleLocation.Y == 1 && vehicleOrientation == Direction.North);
        }
    }
}
