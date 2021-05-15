using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Account User = new Account("191.96.60.224", 45785);
            var url = "https://www.w3schools.com/";
            using var client = new HttpClient();
            WebProxy myproxy = new WebProxy(User.Adress, User.Port);
            myproxy.Credentials = new NetworkCredential(User.Login, User.Password);
            myproxy.BypassProxyOnLocal = true;

            GET(url, myproxy);
            HEAD(url, myproxy);
            OPTIONS(url, myproxy);
            POST(url, myproxy);
            Console.ReadLine();
        }

        static void GET(string Url, WebProxy proxy)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            Console.WriteLine("GET request");
            request.Proxy = proxy;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            int verify;
            verify = (int)response.StatusCode;
            if (verify == 200)
                Console.WriteLine("Request succeeds");
            else
                Console.WriteLine("Request failed");
        }

        static void HEAD(string Url, WebProxy proxy)
        {
            Console.WriteLine("\nHEAD request");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = proxy;
            request.Method = "HEAD";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            int verify;
            verify = (int)response.StatusCode;
            if (verify == 200)
                Console.WriteLine("Request succeeds");
            else
                Console.WriteLine("Request failed");

        }

        static void OPTIONS(string Url, WebProxy proxy)
        {
            Console.WriteLine("\nOPTIONS request");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = proxy;
            request.Method = "OPTIONS";
            request.Headers.Add("Origin", Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            int verify;
            verify = (int)response.StatusCode;
            if (verify == 200)
                Console.WriteLine("Request succeeds");
            else
                Console.WriteLine("Request failed");
        }

        static void POST(string Url, WebProxy proxy)
        {
            Console.WriteLine("\nPOST request");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = proxy;
            request.Method = "POST";
            string postData = "This is a test string";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            string pattern = @"<h3.+?class=[""'].*?(w3-margin-top)";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(responseFromServer);
            if (matches.Count == 0)
            {
                Console.WriteLine("Site don't have tutorials");
            }
            else
            {
                Console.WriteLine(Url + " have " + matches.Count + " Tutorials");
            }
            //Console.WriteLine(responseFromServer);
        }

    }
}
