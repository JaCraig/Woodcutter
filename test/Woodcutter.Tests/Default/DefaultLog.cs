using FileCurator;
using System;
using Woodcutter.Default;
using Woodcutter.Enums;
using Woodcutter.Tests.BaseClasses;
using Xunit;

namespace Woodcutter.Tests.Default
{
    public class DefaultLogTests : TestingDirectoryFixture
    {
        [Fact]
        public void Creation()
        {
            using (DefaultLog File = new DefaultLog("Test", "~/Logs/"))
            {
                Assert.NotNull(File);
                Assert.True(new FileInfo(File.FileName).Exists);
            }
            new DirectoryInfo("~/Logs/").Delete();
        }

        [Fact]
        public void LogMessage()
        {
            using (DefaultLog File = new DefaultLog("Test", "~/Logs/"))
            {
                foreach (MessageType Type in Enum.GetValues(typeof(MessageType)))
                    File.LogMessage("TestMessage", Type);
                Assert.Contains("\r\nGeneral: TestMessage\r\nDebug: TestMessage\r\nTrace: TestMessage\r\nInfo: TestMessage\r\nWarn: TestMessage\r\nError: TestMessage\r\n", new FileInfo(File.FileName).Read());
            }
            new DirectoryInfo("~/Logs/").Delete();
        }
    }
}