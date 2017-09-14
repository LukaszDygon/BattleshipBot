using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Assert.AreEqual(5, bot.GetShipPositions().Count());
        }
    }
}
