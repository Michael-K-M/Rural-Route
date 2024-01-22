using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rural_Route.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string OrderStatus { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


    }

    
}
