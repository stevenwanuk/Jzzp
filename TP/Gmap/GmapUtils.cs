using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TP.ModelView;
using TP.AppStatic;

namespace TP.Gmap
{
    public class GmapUtils
    {
        public static  void GetAddress(string lokkingForString, DownloadStringCompletedEventHandler callBack)
        {

            using (var client = new WebClient())
            {
                client.DownloadStringCompleted += callBack;
                client.DownloadStringAsync(new Uri(string.Format(TPConfig.GetAddressUrl, lokkingForString)));
            }
        }

        public static void GetGeoCode(string lookingForString, DownloadStringCompletedEventHandler callBack)
        {
            using (var client = new WebClient())
            {
                client.DownloadStringCompleted += callBack;
                client.DownloadStringAsync(new Uri(string.Format(TPConfig.GecodingUrl, lookingForString)));
            }
        }

        public static GoogleGeoCodeResponse GetGeoCode(string lookingForString)
        {

            var result = new System.Net.WebClient().DownloadString(string.Format(TPConfig.GecodingUrl, lookingForString));
            return JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(result);
        }

        public static void GetDriection(string oPostCode, DownloadStringCompletedEventHandler callBack)
        {
            using (var client = new WebClient())
            {
                client.DownloadStringCompleted += callBack;
                client.DownloadStringAsync(new Uri(string.Format(TPConfig.DirectionUrl, oPostCode)));
            }
        }

        public static GoogleDirectionsResponse GetDriection(string oPostCode)
        {

            var result = new System.Net.WebClient().DownloadString(string.Format(TPConfig.DirectionUrl, oPostCode));
            return JsonConvert.DeserializeObject<GoogleDirectionsResponse>(result);
        }
    }
}
