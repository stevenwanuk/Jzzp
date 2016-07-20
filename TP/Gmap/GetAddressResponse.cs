using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP.Gmap
{
    public class GetAddressResponse
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public String[] Addresses { get; set; }
    }
}
