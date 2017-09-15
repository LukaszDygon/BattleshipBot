using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;
using NUnit.Framework;

namespace BattleshipBot
{
    class Tests
    {
        [Test]
        public void Initialization_CorrectShipNumber()
        {
            var bot = new MyBot();
            var shipPositions = bot.GetShipPositions();
            var emptyField = Utilities.InitializeEmptyField();
            var randSquare = Utilities.GetRandomSquare(emptyField);

            Assert.AreEqual(5, shipPositions.Count());
        }

        [Test]
        public void Shot_Hits()
        {
            var bot = new MyBot();
            bot.GetShipPositions();
            bot.HandleShotResult(new GridSquare('B', 2), true);

            Assert.AreEqual(95, bot.AllowedTargetSquares.Count);
        }
    }
}
