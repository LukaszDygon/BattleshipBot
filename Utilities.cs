using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    public class Utilities
    {
        public enum Direction
        {
            Vertical,
            Horizontal
        };

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        public static IGridSquare GetRandomSquare(List<IGridSquare> availableGridSquares)
        {
            lock (syncLock)
            {
                var randomId = (random.Next(0, availableGridSquares.Count));
                return availableGridSquares[randomId];
            }
        }

        public static Direction GetRandomDirection()
        {
            lock (syncLock)
            {
                if (random.Next(2) == 0)
                    return Direction.Vertical;
                return Direction.Horizontal;
            }
        }

        public static IGridSquare GetAdjacentShipSquare(List<IGridSquare> shipSquares, List<IGridSquare> availableSquares)
        {

            var nextSquares = new List<IGridSquare>();

            foreach (var square in shipSquares)
            {
                try
                {
                    nextSquares.AddRange(GetAdjacentSquares(square, availableSquares, 1));
                }
                catch {
                }
            }

            if (nextSquares.Count == 0) return null;
            return nextSquares[0];

        }

        public static List<IGridSquare> GetAdjacentSquares(IGridSquare square, List<IGridSquare> availableSquares, int border)
        {
            var adjacentSquares = new List<IGridSquare>();
            adjacentSquares.AddRange(availableSquares.FindAll(x => x.Row >= square.Row - border &&
                                                                x.Row <= square.Row + border &&
                                                                x.Column == square.Column));
            adjacentSquares.AddRange(availableSquares.FindAll(x => x.Column >= square.Column - border &&
                                                                x.Column <= square.Column + border &&
                                                                x.Row == square.Row));
            if (adjacentSquares.Count == 0) return null;
            return adjacentSquares;

        }

        public static void RemoveAdjecent(List<IGridSquare> field, IGridSquare square)
        {
            for (var row = square.Row - 1; row <= square.Row + 1; row++)
            {
                for (var column = square.Column - 1; column <= square.Column + 1; column++)
                {
                    field.RemoveAll(x => x.Row == (char)row && x.Column == column);
                }
            }
        }

        public static void RemoveDiagonal(List<IGridSquare> field, IGridSquare square)
        {
            field.RemoveAll(x => x.Row == (char)(square.Row - 1) && x.Column == square.Column + 1);
            field.RemoveAll(x => x.Row == (char)(square.Row + 1) && x.Column == square.Column + 1);
            field.RemoveAll(x => x.Row == (char)(square.Row - 1) && x.Column == square.Column - 1);
            field.RemoveAll(x => x.Row == (char)(square.Row + 1) && x.Column == square.Column - 1);
        }

        public static List<IGridSquare> InitializeEmptyField()
        {
            var field = new List<IGridSquare>();

            for (var row = 1; row <= 10; row++)
            {
                for (var column = 1; column <= 10; column++)
                {
                    field.Add(new GridSquare(Rows.GetRow(row), column));
                }
            }

            return field;
        }

        public static List<IGridSquare> DeepCopyField(List<IGridSquare> originalField)
        {
            var newField = new List<IGridSquare>();

            foreach (var square in originalField)
            {
                newField.Add(new GridSquare(square.Row, square.Column));
            }

            return newField;
        }
    }
}
