using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Detection
    {
        public string language { get; set; }
        public bool isReliable { get; set; }
        public float confidence { get; set; }
    }

    public class ResultData
    {
        public List<Detection> detections { get; set; }
    }

    public class Result
    {
        public ResultData data { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://ws.detectlanguage.com");
            var request = new RestRequest("/0.2/detect", Method.POST);
            
            request.AddParameter("key", "demo"); // replace "demo" with your API key
            request.AddParameter("q", "Some text to detect language");

            IRestResponse response = client.Execute(request);

            RestSharp.Deserializers.JsonDeserializer deserializer = new RestSharp.Deserializers.JsonDeserializer();

            var result = deserializer.Deserialize<Result>(response);

            Detection detection = result.data.detections[0];
            
            Console.WriteLine("Language: {0}", detection.language);
            Console.WriteLine("Reliable: {0}", detection.isReliable);
            Console.WriteLine("Confidence: {0}", detection.confidence);
        }
    }
}