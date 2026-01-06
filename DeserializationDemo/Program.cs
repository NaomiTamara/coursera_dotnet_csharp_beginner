using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Text.Json;

public class Program
{
    public class Person
    {
        public string UserName { get; set; }
        public int UserAge { get; set; }
    }
    
    static void Main()
    {
        // Binary deserialization
        Person deserializedPerson1;
        using (var fs = new FileStream("person.dat", FileMode.Open))
        using (var reader = new BinaryReader(fs))
        {
            deserializedPerson1 = new Person
            {
                UserName = reader.ReadString(),
                UserAge = reader.ReadInt32(),
            };
        }

        Console.WriteLine("Binary Deserialization");
        Console.WriteLine(deserializedPerson1.UserName);
        Console.WriteLine(deserializedPerson1.UserAge);
        
        // XML deserialization
        var xmlData = "<Person><UserName>Alice</UserName><UserAge>30</UserAge></Person>";
        var serializer = new XmlSerializer(typeof(Person));
        using (var reader = new StringReader(xmlData))
        {
            var deserializedPerson2 = (Person)serializer.Deserialize(reader);
            Console.WriteLine("XML Deserialization");
            Console.WriteLine(deserializedPerson2.UserName);
            Console.WriteLine(deserializedPerson2.UserAge);
        }
        
        // JSON deserialization
        var jsonData = "{\"UserName\":\"Charlie\", \"UserAge\": 25}";
        var deserializedPerson3 = JsonSerializer.Deserialize<Person>(jsonData);
        Console.WriteLine("JSON Deserialization");
        Console.WriteLine(deserializedPerson3.UserName);
        Console.WriteLine(deserializedPerson3.UserAge);
    }
}