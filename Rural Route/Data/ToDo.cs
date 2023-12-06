using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rural_Route.Data
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
