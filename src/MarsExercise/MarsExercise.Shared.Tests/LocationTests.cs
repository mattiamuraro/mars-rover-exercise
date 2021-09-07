using MarsExercise.Shared.Constants;
using MarsExercise.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsExercise.Shared.Tests
{
    [TestClass]
    public class LocationTests
    {
        [TestMethod]
        public void GetNextLocationTestWithDirectionNorth()
        {
            var location = new Location(1, 1);
            var newLocation = location.GetNextLocation(Direction.North);

            Assert.IsTrue(newLocation.X == 1 && newLocation.Y == 2);
        }

        [TestMethod]
        public void GetNextLocationTestWithDirectionEast()
        {
            var location = new Location(1, 1);
            var newLocation = location.GetNextLocation(Direction.East);

            Assert.IsTrue(newLocation.X == 2 && newLocation.Y == 1);
        }

        [TestMethod]
        public void GetNextLocationTestWithDirectionSouth()
        {
            var location = new Location(1, 1);
            var newLocation = location.GetNextLocation(Direction.South);

            Assert.IsTrue(newLocation.X == 1 && newLocation.Y == 0);
        }

        [TestMethod]
        public void GetNextLocationTestWithDirectionWest()
        {
            var location = new Location(1, 1);
            var newLocation = location.GetNextLocation(Direction.West);

            Assert.IsTrue(newLocation.X == 0 && newLocation.Y == 1);
        }
    }
}