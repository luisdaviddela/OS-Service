using System;
using System.IO;
using System.Net;
using System.Text;
using static OS_Service.OneSignal;
namespace OS_Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";


            request.Headers.Add("authorization", String.Format("Basic {0}", OneSignal.RestApiKey));


            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                        + "\"app_id\": \""+AppId+"\","
                                        + "\"headings\": {\"en\": \"SMPS-Adinco S.A. DE C.V.\"}," // TITULO
                                        + "\"subtitle\": {\"en\": \"Josue Gonzalez\"},"           // SUBTITULOs
                                        + "\"contents\": {\"en\": \"Nueva solicitud de pedido\"},"// MENSAJE
                                        + "\"included_segments\": [\"All\"]}");

            //----------------------------------
            //var obj = new
            //{
            //    app_id = "1651",
            //    contents = new { en = "Hola" },
            //    //data = new { image = "http://dsadasdasd.png" },
            //    data = new { image = "https://cdn-images-1.medium.com/max/800/1*7xHdCFeYfD8zrIivMiQcCQ.png" },
            //    included_segments = new string[] { "All" }
            //};
            //----------------------------------
            string responseContent = null;
            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}
