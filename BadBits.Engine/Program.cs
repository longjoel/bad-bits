using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;

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
        static string FindName(string name, string root) {

            return System.IO.Directory.EnumerateFiles(System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(root)))
                .SingleOrDefault(x => x.EndsWith(System.IO.Path.GetFileName(name)));

        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var requireMap = new List<string>();

            Engine.ScriptHost host = new Engine.ScriptHost();
            var scriptEngine = new Jint.Engine();

            dynamic gameGlobals = new { };

            scriptEngine.SetValue("globals", gameGlobals);

            var require = new Action<string>((name) =>
            {
                if (requireMap.IndexOf(FindName(name, args[0]))<0) {

                    requireMap.Add(FindName(name, args[0]));
                    scriptEngine.Execute(System.IO.File.ReadAllText(FindName(name, args[0])));
                }
                
            });

            scriptEngine.SetValue("badBits", host);
            scriptEngine.SetValue("require", require);

            var newEngine = scriptEngine.Execute(System.IO.File.ReadAllText(args[0]));

            using (var window = new OpenTK.GameWindow(640, 480, OpenTK.Graphics.GraphicsMode.Default, 
                "Bad Bits Engine", OpenTK.GameWindowFlags.Default))
            {
                window.Bounds = new System.Drawing.Rectangle { X = 0, Y = 0, Width = 640, Height = 640 };
                window.RenderFrame += (o, e) =>
                {

                    if (host.RenderFunction != null)
                    {
                        host.RenderFunction.Invoke(e.Time);
                    }
                };

                window.Run();

            }

        }
    }
}
