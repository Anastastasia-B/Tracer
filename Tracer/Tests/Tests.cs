using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        private Tracer tracer;

        public Tests()
        {
            tracer = new Tracer();
        }

        private void TestMethod()
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }

        [TestMethod]
        public void TestThreadId()
        {
            tracer.StartTrace();
            tracer.StopTrace();
            var result = tracer.GetTraceResult();
            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, result.threadResults[0].id);
        }
    }
}