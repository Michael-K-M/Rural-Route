using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rural_Route.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }
        public Position Pos { get; set; }

    }

    public enum Position
    {
        [PgName("Admin")]
        Admin = 0,
        [PgName("SalesRep")]
        SalesRep = 1,
        [PgName("Driver")]
        Driver = 2
    }
}
