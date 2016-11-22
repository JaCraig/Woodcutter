using FileCurator;
using System;
using Woodcutter.Default;
using Woodcutter.Enums;
using Woodcutter.Tests.BaseClasses;
using Xunit;

namespace Woodcutter.Tests
{
    public class LogTests : TestingDirectoryFixture
    {
        [Fact]
        public void LogTest()
        {
            var File = (DefaultLog)Log.Get();
            Assert.Equal("Default", File.Name);
            foreach (MessageType Type in Enum.GetValues(typeof(MessageType)))
                File.LogMessage("TestMessage", Type);
            Assert.Contains("\r\nGeneral: TestMessage\r\nDebug: TestMessage\r\nTrace: TestMessage\r\nInfo: TestMessage\r\nWarn: TestMessage\r\nError: TestMessage\r\n", new FileInfo(File.FileName).Read());
            new DirectoryInfo("~/Logs/").Delete();
        }
    }
}