using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace JSONtest
{
    class Program
    {
        public string id { get; set; }
        public string userId { get; set; }
        public string title { get; set; }
        public string body { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public decimal[] geo {get; set;}

        static void Main()
        {
            string json;
            Program test = new Program();
            string myURL = $"https://jsonplaceholder.typicode.com/posts/1";

            //retireve data from a single post. 
            //json = test.GetSinglePostData(myURL);
            //dynamic results = JsonConvert.DeserializeObject<dynamic>(json);
            //var id = results.id;
            //var userId = results.userId;
            //var title = results.title;
            //var body = results.body;

            //Console.WriteLine("Results: ID " + id + ". \nuserID: " + userId + ". \nTitle: " + title + ": "+ body);
            //Console.ReadLine();

            json = test.GetSinglePostData(myURL);
            dynamic results = JsonConvert.DeserializeObject<dynamic>(json);
            var id = results.id;
            var userId = results.userId;
            var title = results.title;

            Console.WriteLine("Results: ID " + id + ". \nuserID: " + userId + ". \nTitle: " + title);
            Console.ReadLine();
        }

        string GetSinglePostData(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        void PostData(string url, string jsonContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
            }
        }

        
        //static HttpClient client = new HttpClient();

        //public class Product
        //{
        //    public int userId { get; set; }
        //    public int id { get; set; }
        //    public string title { get; set; }
        //    public string body { get; set; }
        //}

        //static void ShowProduct( Product product)
        //{
        //    Console.WriteLine($"UserID: {product.userId}\t ID: {product.id}\t Title: {product.title}\t Body: {product.body}");

        //}
        //static void Main(string[] args)
        //{
        //    RunAsync().Wait();

        //}

        //static async Task RunAsync()
        //{
        //    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts/1");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application /json"));


        //    //Create new product object
        //    product = await GetProductAsync(url);

        //    Console.ReadLine();
        //}

        //private void GetResponse(Uri uri, Action<Response> callback)
        //{

        //}
        //static async Task<Product> GetProductAsync(string path)
        //{
        //    Product product = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<Product>();
        //    }
        //    return product;
        //}

    }
}
