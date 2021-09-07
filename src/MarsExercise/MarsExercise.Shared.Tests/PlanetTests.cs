using MarsExercise.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsExercise.Shared.Tests
{
    [TestClass]
    public class PlanetTests
    {
        [TestMethod]
        public void SetObstacleTestWithCoordinates()
        {
            var planet = new Planet(3, 3);
            planet.SetObstacle(1, 1);

            Assert.IsTrue(planet.ThereIsObstacle(1, 1));
        }

        [TestMethod]
        public void SetObstacleTestWithLocation()
        {
            var planet = new Planet(3, 3);
            var location = new Location(1, 1);
            planet.SetObstacle(location);

            Assert.IsTrue(planet.ThereIsObstacle(1, 1));

        }

        [TestMethod]
        public void ThereIsObstacleTestWithCoordinates()
        {
            var planet = new Planet(3, 3);
            planet.SetObstacle(1, 1);

            Assert.IsTrue(planet.ThereIsObstacle(1, 1));
            Assert.IsFalse(planet.ThereIsObstacle(2, 2));
        }

        [TestMethod]
        public void ThereIsObstacleTestWithLocation()
        {
            var planet = new Planet(3, 3);
            planet.SetObstacle(1, 1);

            Assert.IsTrue(planet.ThereIsObstacle(new Location(1, 1)));
            Assert.IsFalse(planet.ThereIsObstacle(new Location(2, 2)));

        }
    }
}
