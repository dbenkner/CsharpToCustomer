using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersControllerProject
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal Sales { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return $"{Id,2:N0} | {Name,-20} | {City}, {State} | {Sales,12:C} | {Active}";
        }
    }

}
