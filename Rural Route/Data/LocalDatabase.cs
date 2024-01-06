using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rural_Route.Data
{
    internal class LocalDatabase
    {
        private readonly SQLiteConnection _con;

        public LocalDatabase()
        {
            _con = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, "ruralRoute.db"));
            _con.CreateTable<User>();
        }

        public User GetLoggedInUser()
        {
            var userList = _con.Table<User>();
            return userList.FirstOrDefault();
        }

        public void SaveUser(User user)
        {    
            _con.Insert(user);    
        }

        public void DeleteUser()
        {
            _con.DeleteAll<User>();           
        }
    }
}
