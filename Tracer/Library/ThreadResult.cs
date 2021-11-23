using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public struct ThreadResult
    {
        public readonly int id;
        public double time { get; private set; }

        public readonly List<MethodResult> methodsResult;
        public ThreadResult(int id)
        {
            this.id = id;
            this.time = 0;
            this.methodsResult = new List<MethodResult>();
        }

        public void addMethod(MethodResult methodResult)
        {
            methodsResult.Add(methodResult);
        }

        public void addTime(double time)
        {
            this.time += time;
        }
    }
}
