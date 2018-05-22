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

using Woodcutter.BaseClasses;

namespace Woodcutter.Default
{
    /// <summary>
    /// Default logger if one isn't found
    /// </summary>
    public class DefaultLogger : LoggerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLogger"/> class.
        /// </summary>
        public DefaultLogger()
            : base("~/App_Data/Logs")
        {
        }

        /// <summary>
        /// Name of the logger
        /// </summary>
        public override string Name
        {
            get { return "Default Logger"; }
        }

        /// <summary>
        /// Adds a log to the logger
        /// </summary>
        /// <param name="name">Name of the log</param>
        public override void AddLog(string name = "Default")
        {
            if (!Logs.ContainsKey(name))
            {
                Logs.Add(name, new DefaultLog(name, Directory));
            }
            else
            {
                Logs[name].Dispose();
                Logs[name] = new DefaultLog(name, Directory);
            }
        }
    }
}