using FileCurator;
using System;
using Woodcutter.Default;
using Woodcutter.Enums;
using Woodcutter.Tests.BaseClasses;
using Xunit;

namespace Woodcutter.Tests.Default
{
    public class DefaultLoggerTests : TestingDirectoryFixture
    {
        [Fact]
        public void Creation()
        {
            using (DefaultLogger Logger = new DefaultLogger())
            {
                Assert.NotNull(Logger);
                Assert.Equal(0, Logger.Logs.Count);
                Assert.Equal("Default Logger", Logger.Name);
            }
            new DirectoryInfo("~/Logs/").Delete();
        }

        [Fact]
        public void LogMessage()
        {
            using (DefaultLogger Logger = new DefaultLogger())
            {
                var File = (DefaultLog)Logger.GetLog();
                Assert.Equal("Default", File.Name);
                foreach (MessageType Type in Enum.GetValues(typeof(MessageType)))
                    File.LogMessage("TestMessage", Type);
                Assert.Contains("\r\nGeneral: TestMessage\r\nDebug: TestMessage\r\nTrace: TestMessage\r\nInfo: TestMessage\r\nWarn: TestMessage\r\nError: TestMessage\r\n", new FileInfo(File.FileName).Read());
            }
            new DirectoryInfo("~/Logs/").Delete();
        }
    }
}