using ConsoleApp_Project.Models;
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
            List<Product> products = null;
            string result = null;

            using (StreamReader sr = new StreamReader(_path))
            {
                result = sr.ReadToEnd();
            }

            if (string.IsNullOrEmpty(result))
            {
                products = new List<Product>();

            }
            else
            {
                products = JsonConvert.DeserializeObject<List<Product>>(result);
            }
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
            string json = JsonConvert.SerializeObject(products);

            using (StreamWriter sw = new StreamWriter(_path))
            {
                sw.Write(json);
            }
        }

        public void DeleteProduct()
        {
            Console.Write("Enter product Id: ");
            string id = Console.ReadLine();

            List<Product> products = null;
            string result = null;

            using (StreamReader sr = new StreamReader(_path))
            {
                result = sr.ReadToEnd();
            }

            if (string.IsNullOrEmpty(result))
            {
                products = new List<Product>();

            }
            else
            {
                products = JsonConvert.DeserializeObject<List<Product>>(result);
            }
            
            Product delId = products.Find(p => p.Id.ToString() == id);
            products.Remove(delId);
            string json = JsonConvert.SerializeObject(products);
            using (StreamWriter sw = new StreamWriter(_path))
            {
                sw.Write(json);
            }
            Console.WriteLine($"{id} pr");
        }


        public void GetProductById()
        {

        }



        public void ShowAllProduct()
        {

        }
    }
}
