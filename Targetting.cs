using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    public class Targetting
    {
        public static IGridSquare GetNext(MyBot bot)
        {
            var targetSquare = Utilities.GetAdjacentSquares(bot.ShipBeingSunk, bot.AllowedTargetSquares);

            if (targetSquare == null || bot.ShipBeingSunk.Count == bot.ShipSizesToDestroy.Max())
            {
                HandleEnemyShipDestroyed(bot);
                return Utilities.GetRandomSquare(bot.AllowedTargetSquares);
            }

            return targetSquare;
        }

        public static void HandleEnemyShipDestroyed(MyBot bot)
        {
            bot.State = State.Searching;

            foreach (var square in bot.ShipBeingSunk)
            {
                Utilities.RemoveAdjecent(bot.AllowedTargetSquares, square);
            }

            bot.ShipSizesToDestroy.Remove(bot.ShipBeingSunk.Count);
            bot.ShipBeingSunk.Clear();
        }

        public static void GetMostIsolated(MyBot bot)
        {
            var isolationLevel = new Dictionary<IGridSquare, int>();
            foreach (var square in bot.AllowedTargetSquares)
            {
                Utilities.GetAdjacentSquares()
            }
        }
    }
}
