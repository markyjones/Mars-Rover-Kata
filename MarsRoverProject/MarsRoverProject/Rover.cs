using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverProject
{
    public class Rover
    {
        public Rover(int bound)
        {
            Orientation = MarsRoverProject.Direction.North;
            Position = new Position(bound) { X = 0, Y = 0 };
        }

        public Direction Orientation { get; set; }
        public Position Position { get; set; }

        public void Rotate(Command command)
        {
            switch (Orientation)
            {
                case MarsRoverProject.Direction.North:
                    Orientation = command == Command.L ? Direction.West : Direction.East;
                    break;
                case MarsRoverProject.Direction.South:
                    Orientation = command == Command.L ? Direction.East : Direction.West;
                    break;
                case MarsRoverProject.Direction.East:
                    Orientation = command == Command.L ? Direction.North : Direction.South;
                    break;
                case MarsRoverProject.Direction.West:
                    Orientation = command == Command.L ? Direction.South : Direction.North;
                    break;
            }
        }

        public void Move()
        {
            Position.Advance(Orientation);
        }
    }
}
