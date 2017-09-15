using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    public class ShipInitializer
    {
        public List<IGridSquare> Field { get; set; }

        public List<ShipPosition> GetRandomShipPlacement()
        {
            var shipPositions = new List<ShipPosition>();
            this.Field = Utilities.InitializeEmptyField();

            PlaceShipRandomly(5, shipPositions);
            PlaceShipRandomly(4, shipPositions);
            PlaceShipRandomly(3, shipPositions);
            PlaceShipRandomly(3, shipPositions);
            PlaceShipRandomly(2, shipPositions);
            

            return shipPositions;
        }

        private void PlaceShipRandomly(int shipSize, List<ShipPosition> shipPositions)
        {
            var direction = Utilities.GetRandomDirection();
            var randomSquare = Utilities.GetRandomSquare(Field);

            while (!SafelyPlaceShip(shipSize, randomSquare, shipPositions, direction))
            {
                direction = Utilities.GetRandomDirection();
                randomSquare = Utilities.GetRandomSquare(Field);
            }
        }

        private bool SafelyPlaceShip(int shipSize, IGridSquare square, List<ShipPosition> shipPositions, Utilities.Direction direction)
        {
            var newField = Utilities.DeepCopyField(Field);
            IGridSquare placementSquare = new GridSquare(square.Row, square.Column);

            if (direction == Utilities.Direction.Horizontal)
                foreach (var column in Enumerable.Range(square.Column, shipSize))
                {
                    placementSquare = new GridSquare(square.Row, column);
                    if (!Field.Contains(placementSquare)) return false;
                    Utilities.RemoveAdjecent(newField, placementSquare);
                }
            else
                foreach (var row in Enumerable.Range(Rows.GetInt(square.Row), shipSize))
                {
                    placementSquare = new GridSquare(Rows.GetRow(row), square.Column);
                    if (!Field.Contains(placementSquare)) return false;
                    Utilities.RemoveAdjecent(newField, placementSquare);
                }

            Field = newField;
            shipPositions.Add(new ShipPosition(square, placementSquare));
            return true;
        }
    }
}
