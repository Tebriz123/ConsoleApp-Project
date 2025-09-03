using ConsoleApp_Project.Models;
using ConsoleApp_Project.Repositories;
using ConsoleApp_Project.Utilits.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ConsoleApp_Project.Services
{

    internal class OrderService
    {
        private readonly string _productPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../../../Data/Products.json"));
        private readonly string _path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../../../Data/Orders.json"));
        public Repostory<Product> ProductRepostory { get; set; } = new Repostory<Product>();
        public Repostory<Order> OrderRepostory { get; set; } = new Repostory<Order>();
        public void OrderProduct()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            string email;
            

            do
            {
                Console.WriteLine("Enter the email:");
                email = Console.ReadLine();
                Console.Clear();
                if (!email.Contains("@"))
                {
                    Console.WriteLine("Email is wrong!");
                }
            } while (!email.Contains("@"));
            List<Product> products = ProductRepostory.Deserialize(_productPath);
            foreach (var item in products)
            {
                item.PrintInfo();
            }
            if (products.Count == 0)
            {
                Console.WriteLine("No product found");
            }
            List<OrderItem> items = new List<OrderItem>();
            while (true)
            {
                Console.WriteLine("Enter product Id or type 'cancel' to go back:");
                string id = Console.ReadLine().Trim();
                Console.Clear();
                if (id.ToLower().Trim() == "cancel")
                {
                    Console.WriteLine("Operation cancelled, returning to menu...");
                    return;
                }
              
                if (!Guid.TryParse(id, out Guid productId))
                {
                    Console.WriteLine("The ID format is incorrect.");
                    continue;
                }

                Product product = products.Find(p => p.Id.ToString() == id.Trim());
                if (product == null)
                {
                    Console.WriteLine($"This product ID was not found.");
                    continue;
                }
                int stock;
                while (true)
                {
                    Console.WriteLine("Enter stock (or type 'cancel' to go back):");
                    string input = Console.ReadLine().Trim();
                    Console.Clear();
                    if (input.ToLower() == "cancel")
                    {
                        Console.WriteLine("Operation cancelled, returning to menu...");
                        return; 
                    }
                    if (int.TryParse(input, out stock))
                    {
                        if (stock > 0) 
                        {
                            break; 
                        }
                        else
                        {
                            Console.WriteLine("Stock cannot be negative. Try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number, please try again.");
                    }
                }
                if (stock > product.Stock)
                {
                    Console.WriteLine($"There is not enough product. Product quantity: {product.Stock}");
                    continue;
                }
                product.Stock -= stock;
                OrderItem orderItem = new OrderItem
                {
                    Product = product,
                    Count = stock,
                    Price = product.Price,
                    SubTotal = product.Price * stock
                };
                items.Add(orderItem);
                Console.WriteLine("Do you want to continue?(yes/no)");
                string answer = Console.ReadLine().ToLower();
                Console.Clear();
                if (answer != "y" && answer != "yes") break;
               
            }
            if (items.Count == 0)
            {
                Console.WriteLine("The order is empty.");
                return;
            }
            Order order = new Order(items, email);
            List<Order> orders = OrderRepostory.Deserialize(_path);
            orders.Add(order);
            ProductRepostory.Serialize(_productPath, products);
            OrderRepostory.Serialize(_path, orders);
            Console.WriteLine("The order created");


        }

      
        public void ShawAllOrders()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            List<Order> orders = OrderRepostory.Deserialize(_path);
            orders.ForEach(o => o.PrintInfo());
        }

        public void ChangeOrderStatus()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            ShawAllOrders();
            Console.WriteLine("Please enter  ID:");
            string id = Console.ReadLine();
            List<Order> orders = OrderRepostory.Deserialize(_path);
            Order order = orders.Find(o => o.Id.ToString() == id.Trim());
            if (order == null)
            {
                Console.WriteLine("Id is wrong!");
                return;
            }
            Console.WriteLine("Yeni statusu elave edin");
            foreach (var item in Enum.GetValues(typeof(OrderStatus)))
            {
                Console.WriteLine($"{(int)item}  -  {item}");
            }
            if (int.TryParse(Console.ReadLine(), out int newStatus) && Enum.IsDefined(typeof(OrderStatus), newStatus))
            {
                order.Status = (OrderStatus)newStatus;
                OrderRepostory.Serialize(_path, orders);
                Console.WriteLine("order status changed");
            }
            else
            {
                Console.WriteLine("There is no such status");
            }


        }


    }
}
