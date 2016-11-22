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

using FileCurator;
using System;
using System.Globalization;
using Woodcutter.BaseClasses;
using Woodcutter.Enums;

namespace Woodcutter.Default
{
    /// <summary>
    /// Outputs messages to a file in ~/App_Data/Logs/ if a web app or ~/Logs/ if windows app with
    /// the format Name+DateTime.Now+".log"
    /// </summary>
    public class DefaultLog : LogBase<DefaultLog>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DefaultLog(string name, string directory)
            : base(name, directory)
        {
            File = new FileInfo(FileName);
            Start = x => File.Write("Logging started at " + DateTime.Now + Environment.NewLine);
            End = x => File.Write("Logging ended at " + DateTime.Now + Environment.NewLine, System.IO.FileMode.Append);
            Log.Add(MessageType.Debug, x => File.Write(x, System.IO.FileMode.Append));
            Log.Add(MessageType.Error, x => File.Write(x, System.IO.FileMode.Append));
            Log.Add(MessageType.General, x => File.Write(x, System.IO.FileMode.Append));
            Log.Add(MessageType.Info, x => File.Write(x, System.IO.FileMode.Append));
            Log.Add(MessageType.Trace, x => File.Write(x, System.IO.FileMode.Append));
            Log.Add(MessageType.Warn, x => File.Write(x, System.IO.FileMode.Append));
            FormatMessage = (Message, Type, args) => Type.ToString()
                + ": " + (args.Length > 0 ? string.Format(CultureInfo.InvariantCulture, Message, args) : Message)
                + Environment.NewLine;
            Start(this);
        }

        /// <summary>
        /// File name
        /// </summary>
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_FileName))
                {
                    _FileName = Directory + Name + "-" + DateTime.Now.ToString("yyyyMMddhhmmss", CultureInfo.CurrentCulture) + ".log";
                }
                return _FileName;
            }
        }

        /// <summary>
        /// File object that the log uses
        /// </summary>
        protected FileInfo File { get; private set; }

        private string _FileName = "";
    }
}