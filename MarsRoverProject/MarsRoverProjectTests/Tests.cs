using MarsRoverProject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverProjectTests
{
    [TestFixture]
    public class Tests
    {
        private const int Bound = 5;

        [Test]
        public void RoverInitallyFacingNorth()
        {
            var rover = new Rover(Bound);
            Assert.That(rover.Orientation, Is.EqualTo(Direction.North));
        }

        [Test]
        public void RoverInitallyAtCoordinates00()
        {
            var rover = new Rover(Bound);
            Assert.That(rover.Position.X, Is.EqualTo(0));
            Assert.That(rover.Position.Y, Is.EqualTo(0));
        }

        [TestCase(Direction.North, Command.L, Result = Direction.West)]
        [TestCase(Direction.West, Command.L, Result = Direction.South)]
        [TestCase(Direction.East, Command.L, Result = Direction.North)]
        [TestCase(Direction.South, Command.L, Result = Direction.East)]
        [TestCase(Direction.North, Command.R, Result = Direction.East)]
        [TestCase(Direction.West, Command.R, Result = Direction.North)]
        [TestCase(Direction.East, Command.R, Result = Direction.South)]
        [TestCase(Direction.South, Command.R, Result = Direction.West)]
        public Direction RoverRotateFromExpect(Direction from, Command command)
        {
            var rover = new Rover(Bound);
            rover.Orientation = from;
            rover.Rotate(command);
            return rover.Orientation;
        }

        [TestCase(Direction.North, Result = 1)]
        [TestCase(Direction.West, Result = 0)]
        [TestCase(Direction.East, Result = 2)]
        [TestCase(Direction.South, Result = 1)]
        public int PositionMoveFromExpectX(Direction from)
        {
            var position = new Position(Bound) { X = 1, Y = 1 };
            position.Advance(from);
            return position.X;
        }

        [TestCase(Direction.North, Result = 2)]
        [TestCase(Direction.West, Result = 1)]
        [TestCase(Direction.East, Result = 1)]
        [TestCase(Direction.South, Result = 0)]
        public int PositionMoveFromExpectY(Direction from)
        {
            var position = new Position(Bound) { X = 1, Y = 1 };
            position.Advance(from);
            return position.Y;
        }

        [TestCase(Direction.North, 1, 1, Result = 1)]   
        [TestCase(Direction.South, 1, 0, Result = 0)]
        public int PositionCannotMoveBeyondYBounds(Direction from, int bound, int start)
        {
            var position = new Position(bound) { X = 0, Y = start };
            position.Advance(from);
            return position.Y;
        }

        [TestCase(Direction.East, 1, 1, Result = 1)]
        [TestCase(Direction.West, 1, 0, Result = 0)]
        public int PositionCannotMoveBeyondXBounds(Direction from, int bound, int start)
        {
            var position = new Position(bound) { X = start, Y = 0 };
            position.Advance(from);
            return position.X;
        }

        [TestCase("LRRRRL", Result = new Command[] { Command.L, Command.R, Command.R, Command.R, Command.R, Command.L })]
        [TestCase("L", Result = new Command[] { Command.L })]
        [TestCase("RM", Result = new Command[] { Command.R, Command.M })]
        [TestCase("RRM", Result = new Command[] { Command.R, Command.R, Command.M })]
        public IEnumerable<Command> CommandParseGivesCorrectCommands(string input)
        {
            var commandParser = new CommandParser();
            var results = commandParser.Parse(input);
            return results;
        }

        [TestCase("RMM", Direction.East, 2, 0)]
        [TestCase("RRRRMM", Direction.North, 0, 2)]
        [TestCase("RMLMRM", Direction.East, 2, 1)]
        [TestCase("LM", Direction.West, 0, 0)]
        [TestCase("MMMMMMMMMM", Direction.North, 0, 5)]
        [TestCase("RMMMMMMMMMM", Direction.East, 5, 0)]
        [TestCase("RRMMMMMMMMMM", Direction.South, 0, 0)]
        public void GameCanProcessMoves(string command, Direction finalDirection, int x, int y)
        {
            var rover = new Rover(Bound);
            var commandParser = new CommandParser();
            var grid = new Grid(commandParser, rover);
            grid.ExecuteCommand(command);
            Assert.That(grid.Rover.Position.X, Is.EqualTo(x));
            Assert.That(grid.Rover.Position.Y, Is.EqualTo(y));
            Assert.That(grid.Rover.Orientation, Is.EqualTo(finalDirection));
        }

       
    }
}
