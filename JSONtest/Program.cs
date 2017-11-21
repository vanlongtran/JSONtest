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
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace JSONtest
{
    class Program
    {
        //* http://dotnetbyexample.blogspot.nl/2012/02/json-deserialization-with-jsonnet-class.html */

        public class Rootobject
        {
            public User[] user { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public Address address { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public Company company { get; set; }
        }

        public class Address
        {
            public string street { get; set; }
            public string suite { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }
            public Geo geo { get; set; }

            public override string ToString()
            {
                return base.ToString();

            }

        }

        public class Geo
        {
            public string lat { get; set; }
            public string lng { get; set; }

            public override string ToString()
            {
                return base.ToString();
            
            }
        }

        public class Company
        {
            public string name { get; set; }
            public string catchPhrase { get; set; }
            public string bs { get; set; }
        }


        static void Main()
        {
            Program test = new Program();
            //UserRootObject User = new UserRootObject();

            string myPostURL = $"https://jsonplaceholder.typicode.com/posts/1";
            string myUserURL = $"https://jsonplaceholder.typicode.com/users";


            //Retireve data from a single post. 
            //string JsonResult = test.GetData(myPostURL);
            //ShowPostData(JsonResult);


            //Retrieve data about multiple user data object
            
            var JsonResult = test.GetData(myUserURL);
            ShowUsersData(JsonResult);
            
            //var User = JsonConvert.DeserializeObject<List<Rootobject[]>>(result);
           // var user = users.First();
            //Console.WriteLine("test " + user);
            //ShowUserData(json = test.GetUserData(myUserURL);


            Console.ReadLine();
        }

        string GetData(string url)
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

        static void ShowPostData(string jsonData)
        {
            dynamic results = JsonConvert.DeserializeObject<dynamic>(jsonData);
            var id = results.id;
            var userId = results.userId;
            var title = results.title;
            var body = results.body;

            Console.WriteLine("Results: ID " + id + ". \nuserID: " + userId + ". \nTitle: " + title + ": " + body);
            //Without reassigning value
            Console.WriteLine("Results: ID " + results.id + ". \nuserID: " + results.userId + ". \nTitle: " + results.title + ": " + results.body);


            Console.ReadLine();
        }

        static void ShowUsersData(string jsonData)
        {
            IList<User> UsersList = new List<User>();
            UsersList = JsonConvert.DeserializeObject<List<User>>(jsonData);
            for (var i = 0; i < UsersList.Count; i++)
            {
                //Console.WriteLine(UsersList.Count);
                Console.WriteLine("ID: {0}\nName: {1}", UsersList[i].id,  UsersList[i].name);
                Console.WriteLine("Email: {0}\nAddress: {1}", UsersList[i].email, UsersList[i].address.ToString());

                Console.WriteLine("Phone: {0}\nWebsite: {1}", UsersList[i].phone, UsersList[i].website);
                Console.WriteLine("Company: {0}", UsersList[i].company.ToString());

            }
            Console.ReadLine();

            //Use Reflection to display all properties in User objects
            object obj = new object();
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach(var p in properties)
            {
                var myVal = p.GetValue(obj);
                Console.WriteLine("Test" + myVal);
            }
        }


        

        



        //void PostData(string url, string jsonContent)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Method = "POST";

        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        //    Byte[] byteArray = encoding.GetBytes(jsonContent);

        //    request.ContentLength = byteArray.Length;
        //    request.ContentType = @"application/json";

        //    using (Stream dataStream = request.GetRequestStream())
        //    {
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //    }
        //    long length = 0;
        //    try
        //    {
        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            length = response.ContentLength;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        // Log exception and throw as for GET example above
        //    }
        //}


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
