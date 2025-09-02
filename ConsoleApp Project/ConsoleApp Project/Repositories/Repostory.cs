using ConsoleApp_Project.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp_Project.Repositories
{
    internal class Repostory<T>
    {
        public void Serialize(string path, List<T> items)
        {
            string json = JsonConvert.SerializeObject(items);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(json);
            }
        }
        public List<T> Deserialize(string path )
        {
           List<T> items = null;
            string result = null;

            using (StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
            }

            if (string.IsNullOrEmpty(result))
            {
                items = new List<T>();

            }
            else
            {
                items = JsonConvert.DeserializeObject<List<T>>(result);
            }
            return items; 
        }
    }
}
