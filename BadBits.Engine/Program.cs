using CommandLine;
using Microsoft.ClearScript.V8;
using System;

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
            string entryPath = default(string);
            Engine.ScriptHost host = new Engine.ScriptHost();

            Parser.Default.ParseArguments<ArgumentOptions>(args).WithParsed<ArgumentOptions>(o =>
            {

                if (string.IsNullOrWhiteSpace(o.Path))
                {
                    Console.WriteLine("No path specified. Please specifiy a path to your main js file.");
                    System.Environment.Exit(1);
                }

                entryPath = o.Path;
            });

            using (var scriptEngine = new V8ScriptEngine())
            {
                scriptEngine.AddHostObject("badBits", host);
                scriptEngine.Execute(System.IO.File.ReadAllText(entryPath));

                

                using (var window = new OpenTK.GameWindow())
                {
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
}
