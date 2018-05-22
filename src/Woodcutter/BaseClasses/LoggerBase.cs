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
using System.Collections.Concurrent;
using System.Collections.Generic;
using Woodcutter.HelperFunctions;
using Woodcutter.Interfaces;

namespace Woodcutter.BaseClasses
{
    /// <summary>
    /// Logger base
    /// </summary>
    public abstract class LoggerBase : IDisposable, ILogger
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="directory">The directory.</param>
        protected LoggerBase(string directory)
        {
            Directory = directory;
            Logs = new ConcurrentDictionary<string, ILog>();
        }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        /// <value>The directory.</value>
        public string Directory { get; set; }

        /// <summary>
        /// Called to log the current message
        /// </summary>
        public IDictionary<string, ILog> Logs { get; }

        /// <summary>
        /// Name of the logger
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Adds a log object or replaces one already in use
        /// </summary>
        /// <param name="name">The name of the log file</param>
        public abstract void AddLog(string name = "Default");

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Logs != null)
            {
                foreach (KeyValuePair<string, ILog> Log in Logs)
                {
                    Log.Value.Dispose();
                }
                Logs.Clear();
            }
        }

        /// <summary>
        /// Gets a specified log
        /// </summary>
        /// <param name="name">The name of the log file</param>
        /// <returns>The log file specified</returns>
        public ILog GetLog(string name = "Default")
        {
            if (!Logs.ContainsKey(name))
                AddLog(name);
            return Logs.GetValue(name);
        }

        /// <summary>
        /// String representation of the logger
        /// </summary>
        /// <returns>The name of the logger</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}