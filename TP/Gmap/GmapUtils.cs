using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TP.ModelView;

namespace TP.Gmap
{
    public class GmapUtils
    {
        static string GeoCodeUrl = ConfigurationManager.AppSettings["GecodingUrl"];
        static string DirectionsCodeUrl = ConfigurationManager.AppSettings["DirectionUrl"];

        public static void GetGeoCode(string lookingForString, DownloadStringCompletedEventHandler callBack)
        {
            using (var client = new WebClient())
            {
                client.DownloadStringCompleted += callBack;
                client.DownloadStringAsync(new Uri(string.Format(GeoCodeUrl, lookingForString)));
            }
        }

        public static GoogleGeoCodeResponse GetGeoCode(string lookingForString)
        {

            var result = new System.Net.WebClient().DownloadString(string.Format(GeoCodeUrl, lookingForString));
            return JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(result);
        }

        public static void GetDriection(string oPostCode, DownloadStringCompletedEventHandler callBack)
        {
            using (var client = new WebClient())
            {
                client.DownloadStringCompleted += callBack;
                client.DownloadStringAsync(new Uri(string.Format(DirectionsCodeUrl, oPostCode)));
            }
        }

        public static GoogleDirectionsResponse GetDriection(string oPostCode)
        {

            var result = new System.Net.WebClient().DownloadString(string.Format(DirectionsCodeUrl, oPostCode));
            return JsonConvert.DeserializeObject<GoogleDirectionsResponse>(result);
        }
    }
}
