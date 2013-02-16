using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Site.ServiceReference1;
using System.Threading;

namespace Site
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginResponseStructure lrs = new LoginResponseStructure();
            string username = "";
            while (!lrs.status)
            {
                Console.WriteLine("Enter Username:");
                username = Console.ReadLine();

                Console.WriteLine("Enter Password:");
                string password = Console.ReadLine();

                SessionManager sm = new SessionManager();
                lrs = sm.login(username, password, "");

                if (!lrs.status)
                {
                    Console.WriteLine("Wrong username/password");
                }
            }

            while (true)
            {
                Console.WriteLine("What do you wish to do?");
                Console.WriteLine("(C)heck an order status / (O)rder a cd / (E)xit");

                string option = Console.ReadLine().ToUpper();

                switch (option)
                {
                    case "O":
                        orderCd(username);
                        break;
                    case "C":
                        checkOrders(username);
                        break;
                    case "E":
                        return;
                    default:
                        Console.WriteLine("Wrong option");
                        break;
                }
            }
        }

        static void checkOrders(string username)
        {
            BackofficeServiceClient client = new BackofficeServiceClient();
            string[] statuses = client.getOrdersStatus(username);

            foreach (string s in statuses)
            {
                Console.WriteLine(s);
            }
        }

        static void orderCd(string username)
        {
            BackofficeServiceClient client = new BackofficeServiceClient();

            Console.WriteLine("Start by searching for an artist name: ");
            string fArtist = Console.ReadLine();
            Console.WriteLine();
            
            client.setClientUsername(username);
            
            foreach (string a in client.getSimilarNamed(fArtist))
            {
                Console.WriteLine(a);
            }

            Console.WriteLine();
            Console.WriteLine("Insert an artist from above: ");
            string artist = Console.ReadLine();
            Console.WriteLine();

            client.setArtistToOrder(artist);
            string[] tracks = client.getTracksFromLastfm(artist);

            foreach (string t in tracks)
            {
                Console.WriteLine(t);
            }

            Console.WriteLine();
            Console.WriteLine("Enter the track you want to buy. Enter only \";\" to finish.");
            string track = "";
            float totalPrice = 0;
            while (track != ";")
            {
                track = Console.ReadLine();

                if (track != ";")
                {
                    client.addTrackToOrder(track);
                    totalPrice = client.getTotalPrice();
                    Console.WriteLine(" Total cost so far: {0}", totalPrice);
                }
            }

            if (totalPrice > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Do you want to buy the following tracks for {0}€ ?", totalPrice);
                foreach (string t in client.getTracksFromOrder())
                {
                    Console.WriteLine("-> " + t);
                }
                Console.WriteLine();
                Console.WriteLine("(y/n)");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("Enter your address:");
                    string address = Console.ReadLine();
                    client.setAddress(address);
                    Thread t = new Thread(client.commitOrder);
                    //client.commitOrder();
                    t.Start();
                    Console.WriteLine("Thank you");
                    Console.WriteLine();
                }
            }
        }
    }
}
