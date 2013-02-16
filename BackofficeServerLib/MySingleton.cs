using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackofficeServerLib
{
    public class MySingleton
    {
        private static MySingleton instance;
        private List<Order> orders = new List<Order>();
        private int counter;

        private MySingleton()
        {
            init();
        }

        public static MySingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MySingleton();
                }
                return instance;
            }
        }

        public void init()
        {
            counter = 0;
        }

        public List<string> getOrdersStatus(string username)
        {
            List<string> rOrders = new List<string>();
            foreach (Order o in orders)
            {
                if (o.clientUsername == username)
                {
                    string tmp = "Order ID: " + o.id + "| Address: " + o.getAddress() + "| Status: " + o.status;
                    rOrders.Add(tmp);
                }
            }
            return rOrders;
        }

        public void setOrderStatus(int idInFab, string status)
        {
            foreach (Order o in orders)
            {
                if (o.idInFab == idInFab)
                {
                    o.status = status;
                }
            }
        }

        public void addOrder(Order order)
        {
            orders.Add(order);
        }

        public void setIdInFabricante(int pId, int pIdInFab)
        {
            foreach (Order o in orders)
            {
                if (o.id == pId)
                {
                    o.idInFab = pIdInFab;
                }
            }
        }

        public int getNextOrderId()
        {
            counter += 1;
            return counter;
        }
    }
}
