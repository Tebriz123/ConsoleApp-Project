using ConsoleApp_Project.Models;
using ConsoleApp_Project.Repositories;
using ConsoleApp_Project.Utilits.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ConsoleApp_Project.Services
{
    internal class ProductService
    {
        private readonly string _path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../../../Data/Products.json"));
        public Repostory<Product> ProductRepostory { get; set; } = new Repostory<Product>();
        public void CreateProduct()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Please enter name: ");
            string name = Console.ReadLine().Trim();
            Console.Clear();
            if (name.Length <= 1)
            {
                Console.WriteLine("please enter right name:");
                return;
            }
            List<Product> products = ProductRepostory.Deserialize(_path);

            bool isDublicate = products.Any(p => p.Name == name.Trim());
            if (isDublicate)
            { 
                Console.Clear();
                Console.WriteLine("There is a product with the same name....");
                return;
            }

            Console.Write("Please enter price: ");
            decimal price;
            string answer = Console.ReadLine();
            Console.Clear();
            decimal.TryParse(answer, out price);
            if (price <= 0)
            {
                Console.WriteLine("Price cannot be negative.");
                return;
            }
            Console.Write("Please enter stock: ");
            int stock;
            string ans = Console.ReadLine();
            Console.Clear();
            int.TryParse(ans, out stock);
            if (stock < 0)
            {
                Console.WriteLine("Stock can not be negative");
                return;

            }
            Product product = new Product(name, price, stock);
            Console.WriteLine($"Id: {product.Id} Name: {product.Name} Price: {product.Price} Stock: {product.Stock}");

            products.Add(product);
            ProductRepostory.Serialize(_path, products);
        }

        public void DeleteProduct()
        {
            ShowAllProduct();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("Enter product Id: ");
            string id = Console.ReadLine();
            Console.Clear();
            List<Product> products = ProductRepostory.Deserialize(_path);


            Product delId = products.Find(p => p.Id.ToString() == id.Trim());
            if (delId == null)
            {
                Console.WriteLine("The ID is wrong");
                return;
            }
            products.Remove(delId);
            ProductRepostory.Serialize(_path, products);
            Console.WriteLine("Wrong Id!");
        }


        public void SoftDelete()
        {
            IsDeleteTrigger(true);
           
        }

        public void RetrieveProduct()
        {
            IsDeleteTrigger(false);

        }

        public void IsDeleteTrigger(bool isDelete)
        {
            ShowAllProduct();
            Console.Write("Enter product Id: ");
            string id = Console.ReadLine();
            Console.Clear();
            List<Product> products = ProductRepostory.Deserialize(_path);
            Product delId = products.Find(p => p.Id.ToString() == id.Trim());
            if (delId == null)
            {
                Console.WriteLine("The ID is wrong");
                return;
            }
            delId.IsDeleted = isDelete;
            ProductRepostory.Serialize(_path, products);
            
        }




        public void GetProductById()
        {
            ShowAllProduct();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Please enter Id:");
            string id = Console.ReadLine();
            Console.Clear();
            List<Product> products = ProductRepostory.Deserialize(_path);
            Product product = products.Find(p => p.Id.ToString() == id.Trim());
            if (product == null)
            {
                Console.WriteLine($"This product ID was not found.");
                return;
            }
            else
            {
                Console.WriteLine($"Id: {product.Id} Name: {product.Name} Price: {product.Price} Stock: {product.Stock}");
            }
            
        }



        public void ShowAllProduct()
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            List<Product> products = ProductRepostory.Deserialize(_path);
            foreach (Product p in products)
            {
                if (!p.IsDeleted)
                {
                    string stockStatus = p.Stock == 0 ? "Out of Stock" : $"Stock: {p.Stock}";
                    Console.WriteLine($"Id: {p.Id} Name: {p.Name} Price: {p.Price} {stockStatus}");
                }
                
            }
        }

        public void RefillProduct()
        {
            ShowAllProduct();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Please enter Id:");
            string id = Console.ReadLine();
            List<Product> products = ProductRepostory.Deserialize(_path);
            Product product = products.Find(p => p.Id.ToString() == id.Trim());
            Console.Clear();
            if (product == null)
            {
                Console.WriteLine($"This product ID was not found.");
                return;
            }
            else
            {
                Console.WriteLine($"Id: {product.Id} Name: {product.Name} Price: {product.Price} Stock: {product.Stock}");
            }
            Console.WriteLine("Enter Stock:");
            int stock;
            bool result = false;
            string ans = Console.ReadLine();
            Console.Clear();
            int.TryParse(ans, out stock);

            if (stock > 0 )
            {
                product.Stock += stock;
                
            }
            else
            {
                Console.WriteLine("The stock was not entered correctly.");
                return;
            }
            ProductRepostory.Serialize(_path, products);
            Console.WriteLine($"{product.Stock} ");

        }

        public void ShowEveryProduct()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;

            List<Product> products = ProductRepostory.Deserialize(_path);

            if (!products.Any())
            {
                Console.WriteLine("There are no any products!");
                Console.ResetColor();
                return;
            }

            var active = products.Where(p => !p.IsDeleted);
            var deActive = products.Where(p => p.IsDeleted);

            Console.WriteLine("Active");
            foreach (Product p in active)
            {
                Console.WriteLine($" Name: {p.Name} Price: {p.Price} Stock: {p.Stock} Id: {p.Id} IsDelete: {p.IsDeleted}");
            }

            Console.WriteLine("\nDeActivE:");
            foreach (Product p in deActive)
            {
                Console.WriteLine($" Name: {p.Name} Price: {p.Price} Stock: {p.Stock} Id: {p.Id} IsDelete: {p.IsDeleted}");
            }
            Console.WriteLine("\n");

        }
        public void ShowSoftDelete()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;

            List<Product> products = ProductRepostory.Deserialize(_path);

            bool anyDeleted = false;

            foreach (Product p in products)
            {
                if (p.IsDeleted) 
                {
                    Console.WriteLine($" Id: {p.Id} Name: {p.Name} Price: {p.Price} Stock: {p.Stock}");
                    anyDeleted = true;
                }
            }

            if (!anyDeleted)
            {
                Console.WriteLine("There are no soft deleted products!");
            }

            Console.ResetColor();
        }

        public void ToggleSoftProduct()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;
            ShowSoftDelete();
            Console.WriteLine("Please enter Id");
            string id = Console.ReadLine();
            Console.Clear();
            List<Product> products = ProductRepostory.Deserialize( _path);
            Product product = products.Find(p => p.Id.ToString() ==id.Trim());
            if(product == null)
            {
                Console.WriteLine("The ID is wrong");
                return;
            }
            if (product.IsDeleted)
            {
                product.IsDeleted = false;
            }
            else
            {
                Console.WriteLine("Enter the correct ID.");
            }
            ProductRepostory.Serialize(_path, products);
        }

    }
}
