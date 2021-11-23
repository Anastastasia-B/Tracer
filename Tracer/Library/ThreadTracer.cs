using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ThreadTracer
    {

        private ThreadResult curInfo;
        private Stack<MethodTracer> callStack;

        public ThreadTracer(int id)
        {
            this.curInfo = new ThreadResult(id);
            this.callStack = new Stack<MethodTracer>();
        }

        public void StartTrace()
        {
            var newMethodTracer = new MethodTracer();
            callStack.Push(newMethodTracer);

            newMethodTracer.StartTrace();
        }

        public void StopTrace()
        {
            var lastTracer = callStack.Pop();
            lastTracer.StopTrace();

            var method = lastTracer.GetTraceResult();

            if (callStack.Count > 0)
            {
                var newLastTracer = callStack.Peek();
                newLastTracer.addChildResult(method);
            }
            else
            {
                curInfo.addTime(method.time);
                curInfo.addMethod(method);
            }

        }

        public ThreadResult GetTraceResult()
        {
            return curInfo;
        }

    }
}
