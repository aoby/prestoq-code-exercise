using System;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PrestoQServicesEncoding;

namespace PrestoQServicesEncodingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new ProductCatalogReader("input-sample.txt");
            var products = reader.ReadLines();

            string json = JsonConvert.SerializeObject(products, new StringEnumConverter() { AllowIntegerValues = false });
            File.WriteAllText("output-sample.json", json);
        }
    }
}
