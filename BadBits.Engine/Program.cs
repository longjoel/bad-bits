using CommandLine;
using Jint.CommonJS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using gl = OpenTK.Graphics.OpenGL.GL;

namespace BadBits.Engine
{
    /// <summary>
    /// OK - Notes
    /// 
    /// Step 1 - make sure the file is real and it's there. If it's not there, then bail.
    /// 
    /// Step 2 - initialize the engine. 
    ///     Create the window with a loading screen
    ///     setup the open gl settings
    ///     setup the bindings
    ///     
    /// Step 3 - invoke the entrypoint script.
    /// 
    /// the script should call register() with an object that looks like this
    ///     { onRender : (dt, ctx)=> void,
    ///         onFrame: (dt) => void,
    ///         onClose: ()=> void,
    ///         onInit: ()=> void}
    ///         
    /// </summary>
    /// 

    class ArgumentOptions
    {
        [Option()]
        public string Path { get; set; }
    }

    static class Program
    {

     
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Engine.ScriptHost host = new Engine.ScriptHost();
            var scriptEngine = new Jint.Engine();

            dynamic globals= new { };

            scriptEngine.SetValue("globals", globals);
            scriptEngine.SetValue("badBits", host);

            // Creates a new Jint instance and runs the myModule.js file in the program's
            // current working directory.
            Jint.Native.JsValue exports = scriptEngine.CommonJS().RunMain(args[0]);

            Graphics2D gfx2d = default(Graphics2D);
          
   
            using (var window = new OpenTK.GameWindow(640, 480, OpenTK.Graphics.GraphicsMode.Default, 
                "Bad Bits Engine", OpenTK.GameWindowFlags.Default, 
                OpenTK.DisplayDevice.Default, 2,1, 
                OpenTK.Graphics.GraphicsContextFlags.Default))
            {

                window.Load += (o, e) =>
                {
                    if (!window.Context.IsCurrent)
                    {
                        window.Context.MakeCurrent(window.WindowInfo);
                    }

                    gfx2d = new Graphics2D();

                };


                window.Bounds = new Rectangle { X = 0, Y = 0, Width = 640, Height = 480 };
                window.RenderFrame += (o, e) =>
                {
                    gl.Viewport(new Rectangle(0, 0, window.Width, window.Height));
                    gl.ClearColor(Color.Blue);
                    gl.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit 
                        | OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit);
                    

                    if (host.Render3dFunction != null)
                    {
                        host.Render3dFunction.Invoke(e.Time);
                    }

                    if (host.Render2dFunction != null)
                    {
                      
                    
                        host.Render2dFunction.Invoke(e.Time,gfx2d);
                    }

                    window.SwapBuffers();
                };

                window.UpdateFrame += (o, e) =>
                {
                    if (host.ProcessFunction != null)
                    {
                        host.ProcessFunction.Invoke(e.Time);
                    }
                };
                window.Run();

            }

        }
    }
}
