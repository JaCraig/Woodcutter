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

namespace Woodcutter.Interfaces
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// Logs held by the logger
        /// </summary>
        IDictionary<string, ILog> Logs { get; }

        /// <summary>
        /// Name of the logger
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Adds a log object or replaces one already in use
        /// </summary>
        /// <param name="name">The name of the log file</param>
        void AddLog(string name = "Default");

        /// <summary>
        /// Gets a specified log
        /// </summary>
        /// <param name="name">The name of the log file</param>
        /// <returns>The log file specified</returns>
        ILog GetLog(string name = "Default");
    }
}