using MarsExercise.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsExercise.Orchestrator.Tests
{
    [TestClass]
    public class OrchestratorManagerTests
    {
        [TestMethod]
        public void DriveMarsTestSolvable()
        {
            var planet = new Planet(5, 5);
            var start = new Location(0, 0);
            var finish = new Location(4, 2);

            var orchestratorManager = new OrchestratorManager(5, 5, planet, start, finish);

            var success = orchestratorManager.DriveMars();

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DriveMarsTestSUnsolvable()
        {
            var planet = new Planet(5, 5);
            planet.SetObstacle(3, 0);
            planet.SetObstacle(3, 1);
            planet.SetObstacle(3, 2);
            planet.SetObstacle(3, 3);
            planet.SetObstacle(3, 4);

            var start = new Location(0, 0);
            var finish = new Location(4, 2);

            var orchestratorManager = new OrchestratorManager(5, 5, planet, start, finish);

            var success = orchestratorManager.DriveMars();

            Assert.IsFalse(success);
        }
    }
}