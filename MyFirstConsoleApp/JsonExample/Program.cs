using Newtonsoft.Json;

public class Person
{
    public string Name { get; set; }
}

public class Program
{
    public static void Main()
    {
        // string json = "{\"Name\": \"Naomi\"}";
        // Person person = JsonConvert.DeserializeObject<Person>(json);
        // Console.WriteLine($"Name: {person.Name}");

        Person newPerson = new Person
        {
            Name = "Naomi"
        };
        string newJson = JsonConvert.SerializeObject(newPerson, Formatting.Indented);
        Console.WriteLine(newJson);
    }
}