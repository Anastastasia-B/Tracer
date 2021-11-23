using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(TraceResult result)
        {
            var threadsResults = result.threadResults;

            JArray threadsJArray = new JArray();
            foreach (ThreadResult threadResult in threadsResults)
            {
                var threadJ = configureThreadElement(threadResult);
                threadsJArray.Add(threadJ);
            }

            JObject rootJObject = new JObject
            {
                {"threads", threadsJArray }
            };

            StringWriter stringWriter = new StringWriter();
            using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                rootJObject.WriteTo(jsonWriter);
            }

            return stringWriter.ToString();
        }

        private JObject configureThreadElement(ThreadResult threadResult)
        {

            JArray methodsArray = new JArray();

            foreach (MethodResult methodResult in threadResult.methodsResult)
            {
                JObject methodXElement = configureMethodElement(methodResult);
                methodsArray.Add(methodXElement);
            }

            JObject threadJElement = new JObject {
                {"id", threadResult.id },
                {"time", threadResult.time },
                {"methods", methodsArray }
            };

            return threadJElement;
        }

        private JObject configureMethodElement(MethodResult methodResult)
        {
            JObject methodElement = configureBaseMethodElement(methodResult);
            JArray childMethods = new JArray();
            foreach (MethodResult childMethodResult in methodResult.childMethodsResult)
            {
                var childMethod = configureMethodElement(childMethodResult);
                childMethods.Add(childMethod);
            }
            methodElement.Add("methods", childMethods);

            return methodElement;
        }

        private JObject configureBaseMethodElement(MethodResult methodResult)
        {
            return new JObject{
                {"name", methodResult.name },
                {"class", methodResult.className },
                {"time", methodResult.time },
            };
        }
    }
}
