using FileCurator;
using System;
using Woodcutter.Default;
using Woodcutter.Enums;
using Woodcutter.Interfaces;
using Woodcutter.Tests.BaseClasses;
using Xunit;

namespace Woodcutter.Tests
{
    public class Manager : TestingDirectoryFixture
    {
        [Fact]
        public void Creation()
        {
            using (Woodcutter Temp = new Woodcutter(new ILogger[] { new DefaultLogger() }))
            {
                Assert.NotNull(Temp);
            }
            new DirectoryInfo("~/Logs/").Delete();
        }

        [Fact]
        public void Log()
        {
            using (Woodcutter Temp = new Woodcutter(new ILogger[] { new DefaultLogger() }))
            {
                var File = (DefaultLog)Temp.GetLog();
                Assert.Equal("Default", File.Name);
                foreach (MessageType Type in Enum.GetValues(typeof(MessageType)))
                    File.LogMessage("TestMessage", Type);
                Assert.Contains("\r\nGeneral: TestMessage\r\nDebug: TestMessage\r\nTrace: TestMessage\r\nInfo: TestMessage\r\nWarn: TestMessage\r\nError: TestMessage\r\n", new FileInfo(File.FileName).Read());
            }
            new DirectoryInfo("~/Logs/").Delete();
        }
    }
}