using System.Net.NetworkInformation;
using Newtonsoft.Json;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<string> Tags { get; set; }
}

public class Program
{
    public static void Main()
    {
        // string json = "{\"Name\": \"Laptop\", \"Price\": 999.99, \"Tags\":[\"Electronics\", \"Computers\"]}";
        // Product product = JsonConvert.DeserializeObject<Product>(json);
        // Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Tags: {string.Join(", ", product.Tags)}");

        Product newProduct = new Product
        {
            Name = "Smartphone",
            Price = 699.99m,
            Tags = new List<string> {"Electronics", "Mobile"},
        };
        string newJson = JsonConvert.SerializeObject(newProduct, Formatting.Indented);
        Console.WriteLine($"Serialized JSON: \n{newJson}");
    }
}