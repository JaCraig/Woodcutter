/*
Copyright 2016 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Woodcutter.Default;
using Woodcutter.Interfaces;

namespace Woodcutter
{
    /// <summary>
    /// Woodcutter's main class
    /// </summary>
    public class Woodcutter : IDisposable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggers">The loggers.</param>
        public Woodcutter(IEnumerable<ILogger> loggers)
        {
            LoggerUsing = loggers.FirstOrDefault(x => !x.GetType().Namespace.StartsWith("WOODCUTTER", StringComparison.OrdinalIgnoreCase)) ?? new DefaultLogger();
        }

        /// <summary>
        /// Logger that the system uses
        /// </summary>
        protected ILogger LoggerUsing { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (LoggerUsing != null)
            {
                LoggerUsing.Dispose();
                LoggerUsing = null;
            }
        }

        /// <summary>
        /// Gets a specified log
        /// </summary>
        /// <param name="name">The name of the log file</param>
        /// <returns>The log file specified</returns>
        public ILog GetLog(string name = "Default")
        {
            return LoggerUsing.GetLog(name);
        }

        /// <summary>
        /// Outputs the logging information
        /// </summary>
        /// <returns>The logger information</returns>
        public override string ToString()
        {
            return "Logger: " + LoggerUsing + "\r\n";
        }
    }
}