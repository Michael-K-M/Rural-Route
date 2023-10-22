using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Npgsql;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Rural_Route.Data
{
    class Database
    {
        string connectionString = "Server=102.33.120.123;Port=5432;Database=rural_route_db;User Id=pi;Password=M1chaelmohr;";

        public User SignInUser(string username, string password)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT userid, firstname, lastname, username, Possition FROM um.user where username = @username and password = @password ", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("@username", username));
                    command.Parameters.Add(new NpgsqlParameter("@password", password));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User();
                            user.Id = reader.GetInt32("userid");
                            user.Username = reader.GetString("username");
                            user.Name = reader.GetString("firstname");
                            user.LastName = reader.GetString("lastname");
                            user.Pos = (Position)Enum.Parse(typeof(Position), reader.GetString("Possition"));
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public void CreateAccount(User user, string password)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO um.User (FirstName, LastName, username, password, possition) VALUES (@FirstName, @LastName, @username, @password, @possition) ", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("@FirstName", user.Name));
                    command.Parameters.Add(new NpgsqlParameter("@LastName", user.LastName));
                    command.Parameters.Add(new NpgsqlParameter("@username", user.Username));
                    command.Parameters.Add(new NpgsqlParameter("@password", password));
                    command.Parameters.Add(new NpgsqlParameter("@possition", user.Pos));
                    command.ExecuteNonQuery();

                   
                }
            }
        }

       
    }
}
