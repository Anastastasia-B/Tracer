using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public struct TraceResult
    {
        public IReadOnlyList<ThreadResult> threadResults { get; private set; }

        public TraceResult(List<ThreadResult> threadResults)
        {
            this.threadResults = threadResults;
        }
    }
}
