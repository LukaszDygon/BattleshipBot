using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    public class MyBot : IBattleshipsBot
    {
        private IGridSquare lastTarget;
        private List<IGridSquare> history = new List<IGridSquare>();
        private List<IGridSquare> allowedTargetSquares = new List<IGridSquare>();

        public IEnumerable<IShipPosition> GetShipPositions()
        {
            history.Clear(); // Forget all our history when we start a new game

            var shipPosition = new ShipInitializer().GetRandomShipPlacement();
            foreach (var ship in shipPosition)
            {
                Console.WriteLine(ship.StartingSquare.Row.ToString() + " " + ship.EndingSquare.Row.ToString());
            }
            return shipPosition;
        }

        private static ShipPosition GetShipPosition(char startRow, int startColumn, char endRow, int endColumn)
        {
            return new ShipPosition(new GridSquare(startRow, startColumn), new GridSquare(endRow, endColumn));
        }

        public IGridSquare SelectTarget()
        {
            var nextTarget = GetNextTarget();
            lastTarget = nextTarget;
            return nextTarget;
        }

        private IGridSquare GetNextTarget()
        {
            return Utilities.GetRandomSquare(this.allowedTargetSquares);
        }

        public void HandleShotResult(IGridSquare square, bool wasHit)
        {
            // Ignore whether we're successful
        }

        public void HandleOpponentsShot(IGridSquare square)
        {
            // Ignore what our opponent does
        }

        public string Name => "HA! Exceptional";
    }
}
