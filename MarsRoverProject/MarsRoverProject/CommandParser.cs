using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverProject
{
    public class CommandParser
    {
        public IEnumerable<Command> Parse(string commands)
        {
            foreach(var item in commands.ToCharArray())
            {
                yield return (Command)Enum.Parse(typeof(Command), item.ToString());
            }
        }
    }
}
