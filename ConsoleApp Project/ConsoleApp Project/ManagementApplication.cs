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
        public OrderService OrderService { get; set; }
        public ManagementApplication()
        {
            ProductService = new ProductService();
            OrderService = new OrderService();
        }
        public void Run()
        {
            int num = 0;
            string str = null;
            bool result = false;


            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();


            while (!(num == 0 && result))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.BackgroundColor = ConsoleColor.White;

                Console.WriteLine("1.Create Product\n2.Delete Product\n3.Get Product By Id\n4.Show All Product\n5.Refill Product\n6.Order Product\n7.Shaw All Orders\n8.Change Order Status\n9.Archive\n\n0.Exit");
                str = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(str, out num);
                switch (num)
                {
                    case 1:
                        ProductService.CreateProduct();
                        break;
                    case 2:
                        Console.WriteLine("1.Delete Product\n2.Soft Delete");
                        str = Console.ReadLine();
                        Console.Clear();
                        result = int.TryParse(str, out num);
                        switch (num)
                        {
                            case 1:
                                ProductService.DeleteProduct();
                                break;
                            case 2:
                                ProductService.SoftDelete();
                                break;
                        }
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
                        OrderService.OrderProduct();
                        break;
                    case 7:
                        OrderService.ShawAllOrders();
                        break;
                    case 8:
                        OrderService.ChangeOrderStatus();
                        break;
                    case 9:
                        Console.WriteLine("1.Show delete product\n2.Show Soft Delete Product");
                        str = Console.ReadLine();
                        Console.Clear();
                        result = int.TryParse(str, out num);
                        switch (num)
                        {
                            case 1:
                                Console.WriteLine("Show delete product");
                                    break;
                            case 2:
                                Console.WriteLine("Show Soft Delete");
                                break;
                          
                        }
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


