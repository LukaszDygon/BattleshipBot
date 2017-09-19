using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Battleships.Player.Interface;

namespace BattleshipBot
{
    public enum State
    {
        Searching,
        SinkingShip
    }

    public class MyBot : IBattleshipsBot
    {
        internal List<IGridSquare> History = new List<IGridSquare>();
        internal List<IGridSquare> AllowedTargetSquares = new List<IGridSquare>();
        internal List<int> ShipSizesToDestroy = new List<int>();
        internal State State;
        internal List<IGridSquare> ShipBeingSunk = new List<IGridSquare>();

        public MyBot()
        {
            AllowedTargetSquares = Utilities.InitializeEmptyField();
        }

        public IEnumerable<IShipPosition> GetShipPositions()
        {
            Initialize();
            
            var shipInitializer = new ShipInitializer();
            var shipPositions = shipInitializer.GetRandomShipPlacement();

            return shipPositions;
        }

        public IGridSquare SelectTarget()
        {
            return State == State.SinkingShip ? Targetting.GetNext(this) : Targetting.GetMostIsolated(this);
        }

        public void HandleShotResult(IGridSquare square, bool wasHit)
        {
            this.AllowedTargetSquares.Remove(square);

            if (wasHit)
            {
                Utilities.RemoveDiagonal(AllowedTargetSquares, square);
                ShipBeingSunk.Add(square);
                State = State.SinkingShip;
            }

            History.Add(square);
        }

        public void HandleOpponentsShot(IGridSquare square)
        {
            History.Add(square);
        }

        private void Initialize()
        {
            History.Clear(); // Forget all our History when we start a new game
            State = State.Searching;
            AllowedTargetSquares = Utilities.InitializeEmptyField();
            ShipSizesToDestroy.AddRange(new List<int>(){2,3,3,4,5});
        }

        public string Name => "V Cute Labradoodle Transport Ship";
    }
}
