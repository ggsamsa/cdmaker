using System;
using System.Net;
using System.Xml.XPath;
using System.Globalization;

namespace BackofficeServerLib
{
    class GeoCoder
    {
        public GeoCoder()
        {
        }

        //Devolve a distancia percorrida de carro entre dois pares de coordenadas, em KM
        public double GetDrivingDistance(GeoPoint gp1, GeoPoint gp2)
        {
            double distance = 0;
            string url = "http://maps.googleapis.com/maps/api/directions/" + "xml?origin=" + gp1.getLatitude().ToString().Replace(",", ".") +
                "," + gp1.getLongitude().ToString().Replace(",", ".") + "&destination=" + gp2.getLatitude().ToString().Replace(",", ".") + ","
                + gp2.getLongitude().ToString().Replace(",", ".") + "&sensor=false";

            WebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = request.GetResponse();
                if (response != null)
                {
                    XPathDocument document = new XPathDocument(response.GetResponseStream());
                    XPathNavigator navigator = document.CreateNavigator();

                    // get response status
                    XPathNodeIterator statusIterator = navigator.Select("/DirectionsResponse/status");
                    while (statusIterator.MoveNext())
                    {
                        if (statusIterator.Current.Value != "OK")
                        {
                            return -1;
                        }
                    }

                    XPathNodeIterator resultIterator = navigator.Select("/DirectionsResponse/route/leg");
                    while (resultIterator.MoveNext())
                    {

                        XPathNodeIterator distancesIterator = resultIterator.Current.Select("distance/value");
                        while (distancesIterator.MoveNext())
                        {
                            distance = double.Parse(distancesIterator.Current.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return distance/1000;
        }

        /**
         * devolve a distancia percorrida de carro entre 3 pontos (Transportadora - Fabricante - Cliente - Transportadora)
         * para calcular a pegada de carbono
         * */

        public double GetDrivingDistance(GeoPoint gp1, GeoPoint gp2, GeoPoint gp3)
        {
            double d1 = GetDrivingDistance(gp1, gp2);
            double d2 = GetDrivingDistance(gp2, gp3);
            double d3 = GetDrivingDistance(gp3, gp1);

            return (d1 + d2 + d3);
        }

        public GeoPoint GetGeoPointFromAddress(string address)
        {
            GeoPoint myGp = new GeoPoint();

            string url = "http://maps.googleapis.com/maps/api/geocode/" + "xml?address=" + address + "&sensor=false";

            WebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = request.GetResponse();
                if (response != null)
                {
                    XPathDocument document = new XPathDocument(response.GetResponseStream());
                    XPathNavigator navigator = document.CreateNavigator();

                    // get response status
                    XPathNodeIterator statusIterator = navigator.Select("/GeocodeResponse/status");
                    while (statusIterator.MoveNext())
                    {
                        if (statusIterator.Current.Value != "OK")
                        {
                            Console.WriteLine("Error: response status = '" + statusIterator.Current.Value + "'");
                            return myGp;
                        }
                    }

                    // get results
                    XPathNodeIterator resultIterator = navigator.Select("/GeocodeResponse/result");
                    while (resultIterator.MoveNext())
                    {
                        Console.WriteLine("Result: ");

                        XPathNodeIterator formattedAddressIterator = resultIterator.Current.Select("formatted_address");
                        while (formattedAddressIterator.MoveNext())
                        {
                            Console.WriteLine(" formatted_address: " + formattedAddressIterator.Current.Value);
                        }

                        XPathNodeIterator geometryIterator = resultIterator.Current.Select("geometry");
                        while (geometryIterator.MoveNext())
                        {
                            Console.WriteLine(" geometry: ");

                            XPathNodeIterator locationIterator = geometryIterator.Current.Select("location");
                            while (locationIterator.MoveNext())
                            {
                                Console.WriteLine("     location: ");

                                XPathNodeIterator latIterator = locationIterator.Current.Select("lat");
                                while (latIterator.MoveNext())
                                {
                                    Console.WriteLine("         lat: " + latIterator.Current.Value);
                                    myGp.setLatitude(Double.Parse(latIterator.Current.Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));
                                }

                                XPathNodeIterator lngIterator = locationIterator.Current.Select("lng");
                                while (lngIterator.MoveNext())
                                {
                                    Console.WriteLine("         lng: " + lngIterator.Current.Value);
                                    myGp.setLongtitude(Double.Parse(lngIterator.Current.Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture));
                                }
                            }

                            XPathNodeIterator locationTypeIterator = geometryIterator.Current.Select("location_type");
                            while (locationTypeIterator.MoveNext())
                            {
                                Console.WriteLine("         location_type: " + locationTypeIterator.Current.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Clean up");
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }

            Console.WriteLine("Done.");

            return myGp;
        }
    }

    class GeoPoint
    {
        private double latitude;
        private double longitude;

        public GeoPoint()
        {
        }

        public GeoPoint(double mlat, double mlng)
        {
            latitude = mlat;
            longitude = mlng;
        }

        public double getLatitude()
        {
            return latitude;
        }

        public double getLongitude()
        {
            return longitude;
        }

        public void setLatitude(double lat)
        {
            latitude = lat;
        }

        public void setLongtitude(double lng)
        {
            longitude = lng;
        }
    }
}
