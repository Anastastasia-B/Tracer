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

        [TestMethod]
        public void TestThreadCount()
        {
            var thread = new Thread(new ThreadStart(TestMethod));
            thread.Start();
            thread.Join();
            thread = new Thread(new ThreadStart(TestMethod));
            thread.Start();
            thread.Join();

            var result = tracer.GetTraceResult();

            Assert.AreEqual(2, result.threadResults.Count);
        }

        [TestMethod]
        public void TestMethodCount()
        {
            TestMethod();
            TestMethod();

            var result = tracer.GetTraceResult();
            var methodsCount = result.threadResults[0].methodsResult.Count;
            Assert.AreEqual(2, methodsCount);
        }
    }
}
