using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public struct MethodResult
    {
        public readonly string name;
        public readonly string className;

        public double time { get; private set; }

        public readonly List<MethodResult> childMethodsResult;

        public MethodResult(string name, string className)
        {
            this.name = name;
            this.className = className;
            this.time = 0;
            this.childMethodsResult = new List<MethodResult>();
        }

        public void addChildMethod(MethodResult childMethod)
        {
            this.childMethodsResult.Add(childMethod);
        }

        public void setTime(double time)
        {
            this.time = time;
        }
    }
}
