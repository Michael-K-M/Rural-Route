using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rural_Route.Data
{
    public class DriverOrderAndProduct
    {
        public Order Order { get; set; }
        public Customer Customer { get; set; }
        public List<(string name, int quantity)> Products { get; set; }

        public User Driver { get; set; }

        public override string ToString()
        {
            return Customer.Name;
        }
    }
}
