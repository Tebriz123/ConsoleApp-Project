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

            bool isDublicate = products.Any(p => p.Name == name);
            if (isDublicate)
            { 
                Console.Clear();
                Console.WriteLine("Hemin adda product var...");
                return;
            }

            Console.Write("Please enter price: ");
            decimal price;
            string answer = Console.ReadLine();
            Console.Clear();
            decimal.TryParse(answer, out price);
            if (price <= 0)
            {
                Console.WriteLine("dugun qiymet daxil edin");
                return;
            }
            Console.Write("Please enter stock: ");
            int stock;
            string ans = Console.ReadLine();
            Console.Clear();
            int.TryParse(ans, out stock);
            if (stock < 0)
            {
                Console.WriteLine("Stock menfi ola bilmez");
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

            List<Product> products = ProductRepostory.Deserialize(_path);


            Product delId = products.Find(p => p.Id.ToString() == id);
            if (delId == null)
            {
                Console.WriteLine("Id duzgun deyil!");
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
            List<Product> products = ProductRepostory.Deserialize(_path);
            Product delId = products.Find(p => p.Id.ToString() == id);
            if (delId == null)
            {
                Console.WriteLine("Id duzgun deyil!");
                return;
            }
            delId.IsDeleted = isDelete;
            ProductRepostory.Serialize(_path, products);

        }




        public void GetProductById()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Id daxil edin:");
            string id = Console.ReadLine();
            List<Product> products = ProductRepostory.Deserialize(_path);
            Product product = products.Find(p => p.Id.ToString() == id);
            if (product == null)
            {
                Console.WriteLine($"Bu Id - li mehsul tapilmadi");
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
                if (!p.IsDeleted) Console.WriteLine($"Id: {p.Id} Name: {p.Name} Price: {p.Price} Stock: {p.Stock}");
            }
        }

        public void RefillProduct()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Id daxil edin:");
            string id = Console.ReadLine();
            List<Product> products = ProductRepostory.Deserialize(_path);
            Product product = products.Find(p => p.Id.ToString() == id);
            Console.Clear();
            if (product == null)
            {
                Console.WriteLine($"Bu Id - li mehsul tapilmadi");
                return;
            }
            else
            {
                Console.WriteLine($"Id: {product.Id} Name: {product.Name} Price: {product.Price} Stock: {product.Stock}");
            }
            Console.WriteLine("Stock daxil edin:");
            int stock;
            string ans = Console.ReadLine();
            Console.Clear();
            int.TryParse(ans, out stock);

            if (stock < 0)
            {
                Console.WriteLine("Stock duzgun daxil edilmib.");
                return;
            }
            else
            {
                product.Stock += stock;
            }
            ProductRepostory.Serialize(_path, products);
            Console.WriteLine($"{product.Stock} ");

        }



        public void ShowDeleteProduct()
        {
            List<Product> products = ProductRepostory.Deserialize( _path);
           
        }

    }
}
