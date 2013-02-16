using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportadorasLib
{
    public class CostManager
    {
        private Queue<double> queue = new Queue<double>();

        private static CostManager instance;

        private CostManager()
        {
            init();
        }

        public static CostManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CostManager();
                }
                return instance;
            }
        }

        public void init()
        {
            queue.Enqueue(0.4);
            queue.Enqueue(0.2);
            queue.Enqueue(0.6);
        }

        public double getCostPerKm()
        {
            double current = queue.Dequeue();
            queue.Enqueue(current);
            return current;
        }
    }
}
