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
using Woodcutter.Enums;
using Woodcutter.HelperFunctions;
using Woodcutter.Interfaces;

namespace Woodcutter.BaseClasses
{
    /// <summary>
    /// Delegate used to format the message
    /// </summary>
    /// <param name="message">Message to format</param>
    /// <param name="type">Type of message</param>
    /// <param name="args">Args to insert into the message</param>
    /// <returns>The formatted message</returns>
    public delegate string Format(string message, MessageType type, params object[] args);

    /// <summary>
    /// Base class for logs
    /// </summary>
    /// <typeparam name="LogType">Log type</typeparam>
    public abstract class LogBase<LogType> : IDisposable, ILog
        where LogType : LogBase<LogType>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the log</param>
        /// <param name="directory">The directory.</param>
        protected LogBase(string name, string directory)
        {
            Directory = directory;
            Name = name;
            Log = new ConcurrentDictionary<MessageType, Action<string>>();
        }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        /// <value>The directory.</value>
        public string Directory { get; set; }

        /// <summary>
        /// Name of the log
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Called when the log is "closed"
        /// </summary>
        protected Action<LogType> End { get; set; }

        /// <summary>
        /// Format message function
        /// </summary>
        protected Format FormatMessage { get; set; }

        /// <summary>
        /// Called to log the current message
        /// </summary>
        protected IDictionary<MessageType, Action<string>> Log { get; }

        /// <summary>
        /// Called when the log is "opened"
        /// </summary>
        protected Action<LogType> Start { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            End((LogType)this);
        }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="type">Type of message</param>
        /// <param name="args">args to format/insert into the message</param>
        public virtual void LogMessage(string message, MessageType type, params object[] args)
        {
            message = FormatMessage(message, type, args);
            if (Log.ContainsKey(type))
                Log.GetValue(type, x => { })(message);
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