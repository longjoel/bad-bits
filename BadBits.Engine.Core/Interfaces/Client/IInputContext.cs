using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Client
{
    public interface IInputContext
    {
        Models.Client.GamepadState pollGamepadState();
    }
}
