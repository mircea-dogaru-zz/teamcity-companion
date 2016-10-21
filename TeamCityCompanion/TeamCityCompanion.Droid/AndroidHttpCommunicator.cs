using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using Android.Widget;
using TeamCityCompanion.Droid;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(AndroidHttpCommunicator))]

namespace TeamCityCompanion.Droid
{
    public class AndroidHttpCommunicator : ICommunicator
    {
        public async Task<XmlReader> Get(string endpoint, string query)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(new Uri(endpoint + query));
                request.ContentType = "application/xml";
                request.Method = "GET";

                var response = await request.GetResponseAsync();
                var stream = response.GetResponseStream();
                    
                var xmlReader = XmlReader.Create(stream);
                return xmlReader;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
                return null;
            }
        }
    }
}