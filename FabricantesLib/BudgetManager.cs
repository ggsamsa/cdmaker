using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FabricantesLib
{
 
    /**
     * é utilizado o design pattern singleton para garantir que a mesma queue de orçamentos e' partilhada pelos 3 fabricantes
     * assim e' simulada uma mudanca regular de custos de fabrico
     * */
    
    public class BudgetManager
    {
        private Queue<double> queue = new Queue<double>();

        private static BudgetManager instance;

        private BudgetManager()
        {
            init();
        }
        
        public static BudgetManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new BudgetManager();
                }
                return instance;
            }
        }

        public void init()
        {
            queue.Enqueue(1.8);
            queue.Enqueue(2);
            queue.Enqueue(2.1);
        }
        
        public double getBudget()
        {
            double current = queue.Dequeue();
            queue.Enqueue(current);
            return current;
        }
    }
}
