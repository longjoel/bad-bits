using BadBits.Engine.Interfaces.Client;
using BadBits.Engine.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Client
{
    public class InputContext : IInputContext
    {
        public GamepadState pollGamepadState()
        {
            var keybState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            var gamepadState = new Microsoft.Xna.Framework.Input.GamePadState();

            if (Microsoft.Xna.Framework.Input.GamePad.MaximumGamePadCount > 0)
            {
                gamepadState = Microsoft.Xna.Framework.Input.GamePad.GetState(0);
            }

            return new Models.Client.GamepadState
            {
                up = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.DPadUp),
                down = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.DPadDown),
                left = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.DPadLeft),
                right = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.DPadRight),

                a = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.V) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.A),
                b = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.B),
                x = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.X) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.X),
                y = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Y),

                start = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Start),
                select = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Back),

                l2 = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.LeftTrigger),
                l1 = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.LeftShoulder),
                r1 = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.RightShoulder),
                r2 = keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A) || gamepadState.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.RightTrigger),

            };
        }
    }
}
