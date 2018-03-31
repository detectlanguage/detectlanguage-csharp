using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Detection
    {
        public string language { get; set; }
        public bool isReliable { get; set; }
        public float confidence { get; set; }
    }

    public class BatchResultData
    {
        public List<List<Detection>> detections { get; set; }
    }

    public class BatchResult
    {
        public BatchResultData data { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://ws.detectlanguage.com");

            // replace "demo" with your API key
            client.Authenticator = new HttpBasicAuthenticator("demo", "");

            string[] texts = new String[2];

            texts[0] = "Hello world";
            texts[1] = "Buenos dias, señor";

            var batchRequest = new RestRequest("/0.2/detect", Method.POST);
            
            batchRequest.RequestFormat = DataFormat.Json;
            batchRequest.AddBody(new { q = texts });

            IRestResponse batchResponse = client.Execute(batchRequest);

            RestSharp.Deserializers.JsonDeserializer deserializer = new RestSharp.Deserializers.JsonDeserializer();
            
            var batchResult = deserializer.Deserialize<BatchResult>(batchResponse);

            Detection batchDetection0 = batchResult.data.detections[0][0];
            
            Console.WriteLine("Language: {0}", batchDetection0.language);
            Console.WriteLine("Reliable: {0}", batchDetection0.isReliable);
            Console.WriteLine("Confidence: {0}", batchDetection0.confidence);

            Detection batchDetection1 = batchResult.data.detections[1][0];
            
            Console.WriteLine("Language: {0}", batchDetection1.language);
            Console.WriteLine("Reliable: {0}", batchDetection1.isReliable);
            Console.WriteLine("Confidence: {0}", batchDetection1.confidence);

        }
    }
}
