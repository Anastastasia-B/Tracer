using System;
using Library;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        public class Foo
        {
            private Bar bar;
            private ITracer tracer;

            internal Foo(ITracer tracer)
            {
                bar = new Bar(tracer);
                this.tracer = tracer;
            }

            public void SomeMethod()
            {
                tracer.StartTrace();
                bar.InnerMethod();
                Thread.Sleep(15);
                bar.InnerMethod();
                tracer.StopTrace();
            }

            public void SomeOtherMethod()
            {
                tracer.StartTrace();
                SomeMethod();
                Thread.Sleep(10);
                tracer.StopTrace();
            }
        }

        public class Bar
        {
            private ITracer tracer;

            internal Bar(ITracer tracer)
            {
                this.tracer = tracer;
            }

            public void InnerMethod()
            {
                tracer.StartTrace();
                Thread.Sleep(300);
                OtherInnerMethod();
                tracer.StopTrace();
            }

            public void OtherInnerMethod()
            {
                tracer.StartTrace();
                Thread.Sleep(50);
                tracer.StopTrace();
            }
        }

        static void Main(string[] args)
        {
            Tracer tracer = new Tracer();
            Foo foo = new Foo(tracer);

            var thread = new Thread(foo.SomeMethod);
            thread.Start();
            thread.Join();

            thread = new Thread(foo.SomeOtherMethod);
            thread.Start();
            thread.Join();

            var res = tracer.GetTraceResult();

            var jsonSerializer = new JsonSerializer();
            var json = jsonSerializer.Serialize(res);

            var xmlSerializer = new XMLSerializer();
            var xml = xmlSerializer.Serialize(res);

            var writer = new ConsoleWriter();
            writer.Write(json);
            writer.Write(xml);

            var fileWriter = new FileWriter("example.json");    
            fileWriter.Write(json);

            fileWriter.changeFileName("example.xml");
            fileWriter.Write(xml);

            
        }
    }
}
