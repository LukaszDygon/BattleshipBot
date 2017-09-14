using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    class ShipInitializer
    {
        public List<IGridSquare> Field;

        public ShipInitializer()
        {
            this.Field = Utilities.InitializeEmptyField();
        }

        public List<ShipPosition> GetRandomShipPlacement()
        {
            var shipPosition = new List<ShipPosition>();
            this.Field = Utilities.InitializeEmptyField();
            PlaceShipRandomly(2, shipPosition);
            PlaceShipRandomly(3, shipPosition);
            PlaceShipRandomly(3, shipPosition);
            PlaceShipRandomly(4, shipPosition);
            PlaceShipRandomly(5, shipPosition);

            return shipPosition;
        }

        private void PlaceShipRandomly(int shipSize, List<ShipPosition> shipPositions)
        {
            var direction = Utilities.GetRandomDirection();
            var randomSquare = Utilities.GetRandomSquare(this.Field);

            while (!SafelyPlaceShip(shipSize, randomSquare, shipPositions, direction))
            {
                direction = Utilities.GetRandomDirection();
                randomSquare = Utilities.GetRandomSquare(this.Field);
            }
        }

        private bool SafelyPlaceShip(int shipSize, IGridSquare square, List<ShipPosition> shipPositions, Utilities.Direction direction)
        {
            var newField = new List<IGridSquare>(this.Field);
            IGridSquare placementSquare = new GridSquare(square.Row, square.Column);

            if (direction == Utilities.Direction.Horizontal)
                foreach (var column in Enumerable.Range(square.Column, shipSize))
                {
                    placementSquare = new GridSquare(square.Row, column);
                    if (!this.Field.Contains(placementSquare)) return false;
                    Utilities.RemoveAdjecent(newField, placementSquare);
                }
            else
                foreach (var row in Enumerable.Range(square.Row, shipSize))
                {
                    placementSquare = new GridSquare(Rows.GetRow(row), square.Column);
                    if (!this.Field.Contains(placementSquare)) return false;
                    Utilities.RemoveAdjecent(newField, placementSquare);
                }

            this.Field = newField;
            shipPositions.Add(new ShipPosition(square, placementSquare));
            return true;
        }
    }
}
