using System;

namespace RefactoringGuru.DesignPatterns.Adapter.Conceptual
{
    // Clasa țintă declară o interfață cu care poate lucra clientul
    // Codul.
    public interface ITarget
    {
        string GetRequest();
    }

    // Clasa care este adaptată conține un comportament util, dar acesta
    // interfața este incompatibilă cu codul client existent. adaptabil
    // clasa are nevoie de ceva lucru înainte ca codul clientului să poată face acest lucru
    // foloseste-l.
    class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Specific request.";
        }
    }

    // Adaptorul face interfața clasei Adapted compatibilă cu ținta
    // interfață.
    class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            this._adaptee = adaptee;
        }

        public string GetRequest()
        {
            return $"This is '{this._adaptee.GetSpecificRequest()}'";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Adaptee adaptee = new Adaptee();
            ITarget target = new Adapter(adaptee);

            Console.WriteLine("Adaptee interface is incompatible with the client.");
            Console.WriteLine("But with adapter client can call it's method.");

            Console.WriteLine(target.GetRequest());
        }
    }
}