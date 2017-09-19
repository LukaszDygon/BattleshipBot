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
            var targetSquare = Utilities.GetAdjacentShipSquare(bot.ShipBeingSunk, bot.AllowedTargetSquares);

            if (targetSquare == null || bot.ShipBeingSunk.Count == bot.ShipSizesToDestroy.Max())
            {
                HandleEnemyShipDestroyed(bot);
                return GetMostIsolated(bot);
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

        public static IGridSquare GetMostIsolated(MyBot bot)
        {
            var isolationLevel = new Dictionary<IGridSquare, int>();
            foreach (var square in bot.AllowedTargetSquares)
            {
                var border = bot.ShipSizesToDestroy.Max();
                var modifier = GetTargetModifier(square, border);
                isolationLevel.Add(square, Utilities.GetAdjacentSquares(square, bot.AllowedTargetSquares, border).Count + modifier);
            }

            var topHits = isolationLevel.Where(x => x.Value == isolationLevel.Values.Max())
                .ToDictionary(i => i.Key, i => i.Value);

            return Utilities.GetRandomSquare((from kvp in topHits select kvp.Key).ToList());
        }

        private static int GetTargetModifier(IGridSquare square, int shortestShipLength)
        {
            if (square.Row < 'A' + shortestShipLength || square.Row > 'I' - shortestShipLength ||
                square.Column < 1 + shortestShipLength || square.Column > 10 - shortestShipLength)
                return 1 * shortestShipLength;
            return 0;
        }
    }
}
