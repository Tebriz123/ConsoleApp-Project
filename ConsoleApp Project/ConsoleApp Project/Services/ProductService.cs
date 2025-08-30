using ConsoleApp_Project.Models;
using ConsoleApp_Project.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ConsoleApp_Project.Services
{
    internal class ProductService
    {
        private readonly string _path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../../../Data/Products.json"));

        public Repostory<Product> ProductRepostory { get; set; }= new Repostory<Product>();
        public void CreateProduct()
        {

            Console.WriteLine("Please enter name: ");
            string name = Console.ReadLine().Trim();
            Console.Clear();
            if (name.Length < 1)
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
            Console.Write("Enter product Id: ");
            string id = Console.ReadLine();

            List<Product> products = ProductRepostory.Deserialize(_path);
            
            
            Product delId = products.Find(p => p.Id.ToString() == id);
            products.Remove(delId);
            ProductRepostory.Serialize(_path, products);
            Console.WriteLine($"{id} pr");
        }


        public void GetProductById()
        {
            Console.WriteLine("Id daxil edin:");
            string id = Console.ReadLine();
            List<Product> products = ProductRepostory.Deserialize(_path);
            Product product = products.Find(p => p.Id.ToString() == id);
            if (product == null)
            {
                Console.WriteLine($"Bu {id}li mehsul tapilmadi");
                return;
            }
            else
            {
                Console.WriteLine($"Id: {product.Id} Name: {product.Name} Price: {product.Price} Stock: {product.Stock}");
            }
        }



        public void ShowAllProduct()
        {
            List<Product> products = ProductRepostory.Deserialize(_path);

            products.ForEach(p => Console.WriteLine($"Id: {p.Id} Name: {p.Name} Price: {p.Price} Stock: {p.Stock}"));
            
        }
    }
}
