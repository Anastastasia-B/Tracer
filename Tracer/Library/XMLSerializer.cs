using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace Library
{
    public class XMLSerializer : ISerializer
    {
        public string Serialize(TraceResult result)
        {
            var threadsResults = result.threadResults;

            XDocument xDoc = new XDocument(new XElement("root"));

            foreach (ThreadResult threadResult in threadsResults)
            {
                var threadXElement = configureThreadXElement(threadResult);
                xDoc.Root.Add(threadXElement);
            }

            StringWriter stringWriter = new StringWriter();
            using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
            {
                xmlWriter.Formatting = Formatting.Indented;
                xDoc.WriteTo(xmlWriter);
            }
            return stringWriter.ToString();
        }

        private XElement configureThreadXElement(ThreadResult threadResult)
        {
            XElement threadXElement = new XElement("thread", new XAttribute("id", threadResult.id),
                                                            new XAttribute("time", threadResult.time)
                                                   );
            foreach (MethodResult methodResult in threadResult.methodsResult)
            {
                XElement methodXElement = configureMethodXElement(methodResult);
                threadXElement.Add(methodXElement);
            }
            return threadXElement;
        }

        private XElement configureMethodXElement(MethodResult methodResult)
        {
            XElement methodXElement = configureBaseMethodXElement(methodResult);
            foreach (MethodResult childMethodResult in methodResult.childMethodsResult)
            {
                var childMethod = configureMethodXElement(childMethodResult);
                methodXElement.Add(childMethod);
            }
            return methodXElement;
        }

        private XElement configureBaseMethodXElement(MethodResult methodResult)
        {
            return new XElement("method", new XAttribute("name", methodResult.name),
                                                           new XAttribute("className", methodResult.className),
                                                           new XAttribute("time", methodResult.time)
                                                  );
        }
    }
}
