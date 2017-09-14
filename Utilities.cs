using System;
using System.Collections.Generic;
using System.Linq;
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

        public static IGridSquare GetRandomSquare(List<IGridSquare> availableGridSquares)
        {
            return availableGridSquares[new Random().Next(availableGridSquares.Count)];
        }

        public static Direction GetRandomDirection()
        {
            if (new Random().Next(0, 2) == 0)
                return Direction.Vertical;
            return Direction.Horizontal;
        }

        public static void RemoveAdjecent(List<IGridSquare> field, IGridSquare square)
        {
            for (var row = square.Row - 1; row <= square.Row + 1; row++)
            {
                for (var column = square.Column - 1; column <= square.Column + 1; column++)
                {
                    try
                    {
                        var squareToRemove = new GridSquare(Rows.R[row], column);
                        field.Remove(squareToRemove);
                    }
                    catch { }
                }
            }
        }

        public static List<IGridSquare> InitializeEmptyField()
        {
            var field = new List<IGridSquare>();

            for (var row = 1; row <= 8; row++)
            {
                for (var column = 1; column <= 8; column++)
                {
                    field.Add(new GridSquare(Rows.GetRow(row), column));
                }
            }

            return field;
        }
    }
}
