using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrackPlace.WPF.Helper
{
    /// <summary>
    /// Calculates distance and time from one point to another point
    /// </summary>
    class Geocoder
    {       
    /// <summary>
    /// Calucaltes distance between 2 points
    /// </summary>
    /// <param name="origin"> starting point</param>
    /// <param name="destination"> Destinaton point</param>
    /// <returns></returns>
        public static int calcDis(string origin, string destination)
        {
            //Get info from google.maps webapi
            int distance = 0;
            string url = @"https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin +
                         "&destinations=" + destination + "&mode=driving&language=et&key=AIzaSyBodCPaqDh7LBPuoOx0iZFIHIY1NRhgtNk&sensor=false";

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            // read XML Code
            DataSet ds = new DataSet();
            ds.ReadXml(new XmlTextReader(new StringReader(responsereader)));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables["element"].Rows[0]["status"].ToString() == "OK")
                {
                    distance = Int32.Parse(ds.Tables["distance"].Rows[0]["value"].ToString());
                }
            }
            return distance;
        }

        /// <summary>
        /// Calculates time to get from 1 to the other place
        /// </summary>
        /// <param name="origin">Starting point</param>
        /// <param name="destination"> Destnation point</param>
        /// <returns></returns>
        public static int calcTime(string origin, string destination)
        {
            //Get info from google.maps web api
            int Time = 0;
            string url = @"https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin +
                         "&destinations=" + destination +
                         "&mode=driving&language=et&key=AIzaSyBodCPaqDh7LBPuoOx0iZFIHIY1NRhgtNk&sensor=false";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            // read XML Code
            DataSet ds = new DataSet();
            ds.ReadXml(new XmlTextReader(new StringReader(responsereader)));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables["element"].Rows[0]["status"].ToString() == "OK")
                {
                    Time = Int32.Parse(ds.Tables["duration"].Rows[0]["value"].ToString());
                }
            }
            return Time;
        }
    }
}
