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
using Microsoft.Maui;

namespace Rural_Route.Data
{
    public class Database
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

        public void CreateProduct(Product product)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO um.product (product_name, product_price) VALUES (@product_name, @product_price) ", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("@product_name", product.Name));
                    command.Parameters.Add(new NpgsqlParameter("@product_price", product.Price));

                    command.ExecuteNonQuery();


                }
            }
        }

        public void CreateCustomer(Customer customer)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO um.customer (customer_name) VALUES (@customer_name) ", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("@customer_name", customer.Name));

                    command.ExecuteNonQuery();


                }
            }
        }

        public void CreateOrder(Order order, List<OrderProduct> orderproducts)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                long orderId;
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO um.Orders (customer_id, order_status, location, datetime) VALUES (@customer_id, @order_status, @location, @datetime); select currval('um.order_id_seq');", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("@customer_id", order.CustomerId));
                    command.Parameters.Add(new NpgsqlParameter("@order_status", order.OrderStatus));
                    command.Parameters.Add(new NpgsqlParameter("@location", order.Location));
                    command.Parameters.Add(new NpgsqlParameter("@datetime", order.DateTime));
                    orderId = (long)command.ExecuteScalar();
                }
                
                foreach (var product in orderproducts)
                {
                    product.OrderId = orderId;
                    using (var command = new NpgsqlCommand("INSERT INTO um.order_product (product_id, quantity, order_id) VALUES (@product_id, @quantity, @order_id) ", connection))
                    {
                        command.Parameters.Add(new NpgsqlParameter("@product_id", product.ProductId));
                        command.Parameters.Add(new NpgsqlParameter("@quantity", product.Quantity));
                        command.Parameters.Add(new NpgsqlParameter("@order_id", product.OrderId));
                        command.ExecuteNonQuery();
                    }
                }
                
            }
        }

        public List<Customer> SelectCustomerInfo()
        {
            var customerList = new List<Customer>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("Select * from  um.customer", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new Customer();
                            customer.Id = reader.GetInt32("customer_id");
                            customer.Name = reader.GetString("customer_name");
                            
                            customerList.Add(customer);
                        }
                    }

                }

            }
            return customerList;
        }

        public List<Product> SelectProductName()
        {
            var productList = new List<Product>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("Select * from  um.product", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product();
                            product.Id = reader.GetInt32("product_id");
                            product.Name = reader.GetString("product_name");

                            productList.Add(product);
                        }
                    }

                }

            }
            return productList;
        }



        public List<DriverOrderAndProduct> DisplayOrder()
        {
            var driverOrderAndProductList = new List<DriverOrderAndProduct>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var displayOrderName = $"""
                Select c.customer_id, c.customer_name, o.order_id, o.location, o.datetime
                from um.orders o
                join um.customer c on c.customer_id = o.customer_id 
                where o.order_status = 'Pending'
                """;

                connection.Open();
                using (var command = new NpgsqlCommand(displayOrderName, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var driverOrderAndProduct = new DriverOrderAndProduct();
                            driverOrderAndProduct.Order = new Order();
                            driverOrderAndProduct.Customer = new Customer();
                            driverOrderAndProduct.Products = new List<(string name, int quantity)>();
                            driverOrderAndProduct.Customer.Id = reader.GetInt32("customer_id");
                            driverOrderAndProduct.Customer.Name = reader.GetString("customer_name");
                            driverOrderAndProduct.Order.Id = reader.GetInt32("order_id");
                            driverOrderAndProduct.Order.DateTime = reader.GetDateTime("datetime");
                            driverOrderAndProduct.Order.Location = reader.GetString("location");

                            driverOrderAndProductList.Add(driverOrderAndProduct);
                        }
                    }
                }

                var orderIds = driverOrderAndProductList.Select(x => x.Order.Id).ToList();

                var productQuery = $"""
                select p.product_name, op.quantity, op.order_id
                from um.product p
                join um.order_product op on p.product_id = op.product_id 
                where op.order_id = Any(@orderIds);
                """;

                using (var command = new NpgsqlCommand(productQuery, connection))
                {

                    command.Parameters.Add(new NpgsqlParameter("@orderIds", orderIds));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            driverOrderAndProductList.First(x => x.Order.Id == reader.GetInt32("order_id"))
                                .Products.Add((reader.GetString("product_name"), reader.GetInt32("quantity")));


                            /*
                            var orderId = reader.GetInt32("order_id");

                           foreach (var driverOrderAndProduct in driverOrderAndProductList)
                            {
                                if(driverOrderAndProduct.Order.Id == orderId)
                                {
                                    driverOrderAndProduct.Products.Add((reader.GetString("name"), reader.GetInt32("quantity")));
                                }
                            }
                            */
                        }
                    }
                }
            }
            return driverOrderAndProductList;
        }

        public List<DriverOrderAndProduct> DisplayDriverOrder(int driver_id)
        {
            var driverOrderAndProductList = new List<DriverOrderAndProduct>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var displayOrderName = $"""
                Select c.customer_id, c.customer_name, o.order_id, o.location, o.datetime
                from um.orders o
                join um.customer c on c.customer_id = o.customer_id join um.user u on u.userid = o.driver_id 
                where o.order_status = 'Departed' and u.userid = {driver_id}
                """;

                connection.Open();
                using (var command = new NpgsqlCommand(displayOrderName, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var driverOrderAndProduct = new DriverOrderAndProduct();
                            driverOrderAndProduct.Order = new Order();
                            driverOrderAndProduct.Customer = new Customer();
                            driverOrderAndProduct.Products = new List<(string name, int quantity)>();
                            driverOrderAndProduct.Customer.Id = reader.GetInt32("customer_id");
                            driverOrderAndProduct.Customer.Name = reader.GetString("customer_name");
                            driverOrderAndProduct.Order.Id = reader.GetInt32("order_id");
                            driverOrderAndProduct.Order.DateTime = reader.GetDateTime("datetime");
                            driverOrderAndProduct.Order.Location = reader.GetString("location");

                            driverOrderAndProductList.Add(driverOrderAndProduct);
                        }
                    }
                }

                var orderIds = driverOrderAndProductList.Select(x => x.Order.Id).ToList();

                var productQuery = $"""
                select p.product_name, op.quantity, op.order_id
                from um.product p
                join um.order_product op on p.product_id = op.product_id 
                where op.order_id = Any(@orderIds);
                """;

                using (var command = new NpgsqlCommand(productQuery, connection))
                {

                    command.Parameters.Add(new NpgsqlParameter("@orderIds", orderIds));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            driverOrderAndProductList.First(x => x.Order.Id == reader.GetInt32("order_id"))
                                .Products.Add((reader.GetString("product_name"), reader.GetInt32("quantity")));


                            /*
                            var orderId = reader.GetInt32("order_id");

                           foreach (var driverOrderAndProduct in driverOrderAndProductList)
                            {
                                if(driverOrderAndProduct.Order.Id == orderId)
                                {
                                    driverOrderAndProduct.Products.Add((reader.GetString("name"), reader.GetInt32("quantity")));
                                }
                            }
                            */
                        }
                    }
                }
            }
            return driverOrderAndProductList;
        }

        public void UpdateDelivery(string order_status, int order_id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE um.orders SET order_status = '"+ order_status + "' where order_id = '" + order_id + "'", connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (NpgsqlException ex)
                    {
                        string nekej = ex.ToString();
                        throw;
                    }

                }
            }
        }

        public void UpdateDeliveryDriver(int driver_id, int order_id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE um.orders SET driver_id = '" + driver_id + "' where order_id = '" + order_id + "'", connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (NpgsqlException ex)
                    {
                        string nekej = ex.ToString();
                        throw;
                    }

                }
            }
        }
    }
}
