using ConsoleApp_Project.Models;
using System;
using System.IO;
using System.Linq.Expressions;

namespace ConsoleApp_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ManagementApplication menu = new ManagementApplication();
            menu.Run();

        }
    }
}
