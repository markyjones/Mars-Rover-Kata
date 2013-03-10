using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverProject
{
    public class Grid
    {
        private readonly CommandParser _parser;
        private readonly Rover _rover;

        public Grid(CommandParser parser, Rover rover){
            _parser = parser;
            _rover = rover;
        }

        public Rover Rover
        {
            get
            {
                return _rover;
            }
        }

        public void ExecuteCommand(string command){

            var parsedCommands = _parser.Parse(command);
            foreach (var parsedCommand in parsedCommands)
            {
                if (parsedCommand == Command.M)
                {
                    _rover.Move();
                    continue;
                }

                _rover.Rotate(parsedCommand);
            }

        }
    }
}
