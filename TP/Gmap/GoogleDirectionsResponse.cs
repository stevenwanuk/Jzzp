using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP.Gmap
{
    public class GoogleDirectionsResponse
    {
        public string status { get; set; }
        public route[] routes { get; set; }
        public geocoded_waypoint[] geocoded_waypoints { get; set; }
    }

    public class route
    {
        public bound bounds { get; set; }
        public string summary { get; set; }
        public string copyrights { get; set; }
        public leg[] legs { get; set; }


    }

    public class bound
    {
        public location northeast { get; set; }
        public location southwest { get; set; }
    }

    public class leg
    {
        public string travel_mode { get; set; }
        public distance distance { get; set; }
        public duration duration { get; set; }
        public string end_address { get; set; }
        public location end_location { get; set; }
        public string start_address { get; set; }
        public location start_location { get; set; }
        public step[] steps { get; set; }
        public polyline polyline { get; set; }
    }

    public class step
    {
        public string travel_mode { get; set; }
        public distance distance { get; set; }
        public duration duration { get; set; }
        public string end_address { get; set; }
        public location end_location { get; set; }
        public string start_address { get; set; }
        public location start_location { get; set; }
        public polyline polyline { get; set; }
    }

    public class distance
    {
        public decimal value { get; set; }
        public string text { get; set; }
    }

    public class duration
    {
        public decimal value { get; set; }
        public string text { get; set; }
    }

    public class polyline
    {
        public string points { get; set; }
    }



    public class geocoded_waypoint
    {
        public string geocoder_status { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }
}
