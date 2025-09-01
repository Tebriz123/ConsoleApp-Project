using ConsoleApp_Project.Models;
using ConsoleApp_Project.Repositories;
using ConsoleApp_Project.Utilits.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            string gmail;
            do
            {
                Console.WriteLine("Gmail daxil ein:");
                 gmail = Console.ReadLine();
                if (!gmail.Contains("@"))
                {
                    Console.WriteLine("Gmail duzgun daxil edilmeyib!");
                }
            }while (!gmail.Contains("@"));
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
                Console.WriteLine("Id daxil edin:");
                string id = Console.ReadLine().Trim();
                if (!Guid.TryParse(id, out Guid productId))
                {
                    Console.WriteLine("Id formati duzgun deyil");
                    continue;
                }

                Product product = products.Find(p => p.Id.ToString() == id);
                if (product == null)
                {
                    Console.WriteLine($"Bu {id} - li mehsul tapilmadi");
                    continue;
                }
                int stock;
                while (true)
                {
                    Console.WriteLine("Nece eded almaq isteyirsiz?");

                    if (int.TryParse(Console.ReadLine(), out stock) && stock > 0) break;
                    Console.WriteLine("stock duzgun daxil edilmeyib");
                }
                if (stock > product.Stock)
                {
                    Console.WriteLine($"Kifayet qeder mehsul yoxdur. Mehsul sayi: {product.Stock} qeder mehsul var!");
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
                Console.WriteLine("Davam etmek isteyirsizmi?");
                string answer = Console.ReadLine().ToLower();
                if (answer != "y" && answer != "yes") break;

            }
            if (items.Count == 0)
            {
                Console.WriteLine("Sifaris bosdur ");
                return;
            }
            Order order = new Order(items, gmail);
            List<Order> orders = OrderRepostory.Deserialize(_path);
            orders.Add(order);
            ProductRepostory.Serialize(_path, products);
            OrderRepostory.Serialize(_path, orders);
            Console.WriteLine("sifaris yaradildi.");


        }


        public void ShawAllOrders()
        {
            List<Order> orders = OrderRepostory.Deserialize(_path);
            orders.ForEach(o => o.PrintInfo());
            
        }

        public void ChangeOrderStatus()
        {
            Console.WriteLine("Id daxil edin:");
            string id = Console.ReadLine();
            List<Order> orders = OrderRepostory.Deserialize(_path);
            Order order = orders.Find(o => o.Id.ToString() == id);
            if(order == null)
            {
                Console.WriteLine("Id duzgun deyil!");
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
                Console.WriteLine("order status deyisdi");
            }
            else
            {
                Console.WriteLine("Bele bir status yoxdur");
            }
            

        }


    }
}
