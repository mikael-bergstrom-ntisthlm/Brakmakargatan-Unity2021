using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public enum InputCommand
    {
        left, right,
        duck, jump,
        punch1, punch2,
        kick1, kick2
    }

    class PlayerInput
    {
        private InputCommand command;
        private bool hasExecuted;

        public PlayerInput(InputCommand cmd)
        {
            command = cmd;
        }
    }
}
