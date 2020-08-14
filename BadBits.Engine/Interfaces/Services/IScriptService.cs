using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Services
{
    /// <summary>
    /// This launches the script.
    /// </summary>
    public interface IScriptService
    {

        /// <summary>
        /// Initialize the script service
        /// </summary>
        void Initialize();

        /// <summary>
        /// Execute the script.
        /// </summary>
        void Execute();
    }
}
