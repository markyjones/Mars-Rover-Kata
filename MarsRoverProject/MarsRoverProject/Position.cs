using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverProject
{
    public class Position

    {
        private readonly int _bound;

        public Position(int bound)
        {
            _bound = bound;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public void Advance(Direction direction)
        {
            switch (direction)
            {
                case MarsRoverProject.Direction.North:
                    Y = AdvanceOrStay(Y);
                    break;
                case MarsRoverProject.Direction.South:
                    Y = DecrementOrStay(Y);
                    break;
                case MarsRoverProject.Direction.East:
                    X = AdvanceOrStay(X);
                    break;
                case MarsRoverProject.Direction.West:
                    X = DecrementOrStay(X);
                    break;
            }
        }

        private int AdvanceOrStay(int coordinate)
        {
            if ((coordinate + 1) > _bound)
            {
                return coordinate;
            }
            return coordinate + 1;
        }

        private int DecrementOrStay(int coordinate)
        {
            if ((coordinate - 1) < 0)
            {
                return coordinate;
            }
            return coordinate - 1;
        }
    }
}
