using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Models.Client
{
    public class GamepadState
    {
        public bool up { get; set; }
        public bool down { get; set; }
        public bool left { get; set; }
        public bool right { get; set; }

        public bool a { get; set; }
        public bool b { get; set; }
        public bool x { get; set; }
        public bool y { get; set; }

        public bool l1 { get; set; }
        public bool l2 { get; set; }

        public bool r1 { get; set; }
        public bool r2 { get; set; }

        public bool start { get; set; }
        public bool select { get; set; }
    }
}
