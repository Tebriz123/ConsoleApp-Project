using ConsoleApp_Project.Models;
using ConsoleApp_Project.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp_Project
{
    internal class ManagementApplication
    {
        public ProductService ProductService { get; set; }
        public ManagementApplication()
        {
            ProductService = new ProductService();
        }
        public void Run()
        {
            int num = 0;
            string str = null;
            bool result = false;

            while (!(num == 0 && result))
            {
                Console.WriteLine("1.Create Product\n2.Delete Product\n3.Get Product By Id\n4.Show All Product\n5.Refill Product\n6.Order Product\n7.Shaw All Orders\n8.Change Order Status\n\n0.Exit");
                str = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(str, out num);
                switch (num)
                {
                    case 1:
                        ProductService.CreateProduct();
                        break;
                    case 2:
                        ProductService.DeleteProduct();
                        break;
                    case 3:
                        ProductService.GetProductById();
                        break;
                    case 4:
                        ProductService.ShowAllProduct();
                        
                        break;
                    case 5:
                        ProductService.RefillProduct();
                        break;
                    case 6:
                        ProductService.OrderProduct();
                        break;
                    case 7:
                        Console.WriteLine("ORDERS");
                        break;
                    case 8:
                        Console.WriteLine("ORDERS STATUS");
                        break;

                    case 0:
                        if (result)
                        {
                            Console.WriteLine("Program finished");
                        }
                        else
                        {
                            Console.WriteLine("Wrong input");
                        }   
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;





                }
            }


        }
    }
}


