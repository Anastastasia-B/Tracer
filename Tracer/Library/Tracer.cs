using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace Library
{
    public class Tracer : ITracer
    {

        private ConcurrentDictionary<int, ThreadTracer> threadsTrace;


        public Tracer()
        {
            this.threadsTrace = new ConcurrentDictionary<int, ThreadTracer>();
        }

        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            ThreadTracer threadTrace;
            if (threadsTrace.ContainsKey(threadId))
            {
                threadTrace = threadsTrace[threadId];
            }
            else
            {
                threadTrace = new ThreadTracer(threadId);
                threadsTrace[threadId] = threadTrace;

            }

            threadTrace.StartTrace();
        }

        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            var threadTrace = threadsTrace[threadId];
            if (threadTrace != null)
            {
                threadTrace.StopTrace();
            }
        }

        public TraceResult GetTraceResult()
        {
            var values = threadsTrace.Values;
            var listResults = new List<ThreadResult>();
            foreach (ThreadTracer value in values)
            {
                var threadResult = value.GetTraceResult();
                listResults.Add(threadResult);
            }

            var result = new TraceResult(listResults);
            return result;
        }

    }
}
