using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Library
{
    class MethodTracer
    {
        private MethodResult curInfo;
        private Stopwatch stopwatch;

        public MethodTracer()
        {
            this.stopwatch = new Stopwatch();

            var traceTrace = new StackTrace();
            var stackFrame = traceTrace.GetFrame(3);
            string methodName = stackFrame.GetMethod().Name;
            string className = stackFrame.GetMethod().ReflectedType.Name;

            this.curInfo = new MethodResult(methodName, className);
        }

        public void StartTrace()
        {
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
            var time = stopwatch.ElapsedMilliseconds;
            curInfo.setTime(time);
        }

        public MethodResult GetTraceResult()
        {
            return curInfo;
        }

        public void addChildResult(MethodResult methodResult)
        {
            this.curInfo.addChildMethod(methodResult);
        }

    }
}
