using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_data_trasnformation
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (StreamReader r = new StreamReader("./cars.json"))
                {
                    var jsonString = r.ReadToEnd();
                    var carsList = JsonSerializer.Deserialize<List<Car>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    // Create first class Product as a new model
                    // Then transform carsList data into the new model with Select
                    var map = carsList.Select(i => new Product
                    {
                        ProductId = i.Id,
                        ProductName = $"{i.Make} {i.Model}",
                        Category = i.Type
                    });
                    var serialize = JsonSerializer.Serialize(map.ToArray(), new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(serialize);
                    // FileStream is a class used for reading and writing files
                    using (FileStream f = new FileStream("./products.json", FileMode.Create, FileAccess.Write, FileShare.None))
                    {// write serialize into f file 
                        using (StreamWriter w = new StreamWriter(f))
                        {
                            w.WriteLine(serialize);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
