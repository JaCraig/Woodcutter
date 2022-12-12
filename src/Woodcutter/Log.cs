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

using Microsoft.Extensions.DependencyInjection;
using System;
using Woodcutter.Interfaces;

namespace Woodcutter
{
    /// <summary>
    /// Logging class
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// The service provider lock
        /// </summary>
        private static readonly object ServiceProviderLock = new object();

        /// <summary>
        /// The service provider
        /// </summary>
        private static IServiceProvider ServiceProvider;

        /// <summary>
        /// Gets the log specified
        /// </summary>
        /// <param name="name">Name of the log</param>
        /// <returns>The log specified</returns>
        public static ILog Get(string name = "Default")
        {
            var Manager = GetServiceProvider().GetService<Woodcutter>();
            return Manager?.GetLog(name);
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <returns></returns>
        private static IServiceProvider GetServiceProvider()
        {
            if (ServiceProvider is not null)
                return ServiceProvider;
            lock (ServiceProviderLock)
            {
                if (ServiceProvider is not null)
                    return ServiceProvider;
                ServiceProvider = new ServiceCollection().AddCanisterModules()?.BuildServiceProvider();
            }
            return ServiceProvider;
        }
    }
}