namespace DependencyInjectionApp;

public class Hammer // dependency
{
    public void Use()
    {
        Console.WriteLine("Hammering Nails!");
    }
}

public class Saw
{
    public void use()
    {
        Console.WriteLine("Sawing Wood!")
    }
}

// The hammer and the saw are dependencies of the builder

public class Builder // builder has dependencies
{
    // define what de builder needs:
    private Hammer _hammer;
    private Saw _saw;

    // actually bringing what de builder needs:
    public Builder()
    {
        // Builder is responsible for bringing the hammer: this is the dependency
        _hammer = new Hammer();
        _saw = new Saw();
    }

    public void BuildHouse()
    {
        _hammer.Use();
        _saw.Use();
        Console.WriteLine("House is build")
    }
}

// Dependecy injection: in stead of the builder going out to buy the hammer and saw himself
// it is brought to him. So the builder can focus on building, without worrying about how to get the tools.

// so WITH constructur dependency injection it will look like this:

public class BuilderDI_C
{
    private Hammer _hammer;
    private Saw _saw;

    // Constructur Dependency Injection (DI)
    // In words: the builder only shows up to work, but it does not bring its own tools: hammer and saw
    public Builder(Hammer hammer, Saw saw) // so provide the builder with the tools here
    {
        _hammer = hammer;
        _saw = saw;
    }
    
    public void BuildHouse()
    {
        _hammer.Use();
        _saw.Use();
        Console.WriteLine("House is built")
    }
    
    internal class Program
        static void Main(string[] args)
        {
            Hammer hammer = new Hammer();
            Saw saw = new Saw();
            Builder builder = new Builder(hammer, saw);

            builder.BuildHouse();

            Console.ReadLine();
        }
}

// WITH setter dependency injection it will look like this:

public class BuilderDI_S
{
    private Hammer Hammer { get; set; };
    private Saw Saw { get; set; };

    // Setter Dependency Injection (DI)
    // In words: the builder only shows up to work, but it does not bring its own tools: hammer and saw
    
    public void BuildHouse()
    {
        Hammer.Use();
        Saw.Use();
        Console.WriteLine("House is built")
    }
    
    internal class Program
        static void Main(string[] args)
        {
            Hammer hammer = new Hammer(); // Create dependencies outsite
            Saw saw = new Saw(); // Create dependencies outsite
            Builder builder = new Builder();
            builder.Hammer = hammer; // Inject dependencies via Setters
            builder.Saw = saw; // Inject dependencies via Setters

            builder.BuildHouse();

            Console.ReadLine();
        }
}

// WITH interface dependen injection it will look like this:
// providing dependencies through interface that class implements.

namespace DependencyInjectionApp_I
{
    // interface defines methods to set dependencies
    public interface IToolUser
    {
        void SetHammer(Hammer hammer);
        void setSaw(Saw saw);
    }
    
    public class Hammer
    {
        public void Use()
        {
            Console.WriteLine("Hammering Nails!");
        }
    }

    public class Saw
    {
        public void use()
        {
            Console.WriteLine("Sawing Wood!")
        }
    }
    
    public void SetHammer(Hammer hammer)
    {
        _hammer = hammer
    }

    public void SetSaw(Saw saw)
    {
        _saw = saw;
    }
    
    public class Builder: IToolUser
    {
        private Hammer _hammer;
        private Saw _saw;

        public void BuildHouse()
        {
            _hammer.Use();
            _saw.Use();
            Console.WriteLine("House is build")
        }
    }
    
    internal class Program
        static void Main(string[] args)
        {
            Hammer hammer = new Hammer(); // Create dependencies outsite
            Saw saw = new Saw(); // Create dependencies outsite
            Builder builder = new Builder();
            builder.SetHammer(hammer);
            builder.SetSaw(saw);
            
            builder.BuildHouse();

            Console.ReadLine();
        }
}