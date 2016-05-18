using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TP.Common;

namespace TP.AppStatic
{
    public static class TPConfig
    {

        public static int TerminalId { get; set; }
        public static string GecodingUrl { get; set; }
        public static string DirectionUrl { get; set; }
        public static string StaticMapUrl { get; set; }
        public static string GMap { get; set; }
        public static string PrintFile {get;set;}
        public static string PrintItemSampleFile { get; set; }
        public static string PrintResImagePath { get; set; }
        public static int QRHeight { get; set; }
        public static int QRWidth { get; set; }
        public static bool EnaEnabledGMap { get; set; }

        public static int PrintPageWidth { get; set; }


        public static bool Load()
        {

            var result = false;
            try
            {

                TerminalId = Convert.ToInt32(ConfigurationManager.AppSettings["TerminalId"]);
                GecodingUrl = ConfigurationManager.AppSettings["GecodingUrl"];
                DirectionUrl = ConfigurationManager.AppSettings["DirectionUrl"];
                StaticMapUrl = ConfigurationManager.AppSettings["StaticMapUrl"];
                GMap = ConfigurationManager.AppSettings["GMap"];
                PrintFile = ConfigurationManager.AppSettings["PrintFile"];
                PrintItemSampleFile = ConfigurationManager.AppSettings["PrintItemSampleFile"];
                PrintResImagePath = ConfigurationManager.AppSettings["PrintResImagePath"];
                QRHeight = Convert.ToInt32(ConfigurationManager.AppSettings["QRHeight"]);
                QRWidth = Convert.ToInt32(ConfigurationManager.AppSettings["QRWidth"]);
                EnaEnabledGMap = Convert.ToBoolean(ConfigurationManager.AppSettings["EnaEnabledGMap"]);

                //Print
                PrintPageWidth = Convert.ToInt32(ConfigurationManager.AppSettings["PrintPageWidth"]);


                result = true;
            }catch(Exception e)
            {
                Log4netUtil.For(typeof(TPConfig)).Error(e);
            }
            return result;
        }
    }
}
