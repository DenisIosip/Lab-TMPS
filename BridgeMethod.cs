using System;

namespace RefactoringGuru.DesignPatterns.Bridge.Conceptual
{
    // Abstracția stabilește o interfață pentru partea „control” a celor două ierarhii
    // clase. Conține o referință la un obiect din ierarhia de implementare și
    // îi delegă toată munca reală.
    class Abstraction
    {
        protected IImplementation _implementation;

        public Abstraction(IImplementation implementation)
        {
            this._implementation = implementation;
        }

        public virtual string Operation()
        {
            return "Abstract: Base operation with:\n" +
                _implementation.OperationImplementation();
        }
    }

    // Puteți extinde abstracția fără a schimba clasele implementării.
    class ExtendedAbstraction : Abstraction
    {
        public ExtendedAbstraction(IImplementation implementation) : base(implementation)
        {
        }

        public override string Operation()
        {
            return "ExtendedAbstraction: Extended operation with:\n" +
                base._implementation.OperationImplementation();
        }
    }

    // Implementarea setează interfața pentru toate clasele de implementare. El nu este
    // trebuie să se potrivească cu interfața Abstraction. În practică, ambele interfețe
    // poate fi complet diferit. De obicei, interfața de implementare
    // furnizează numai operații primitive, în timp ce Abstraction
    // definește operațiuni de nivel superior pe baza acestor primitive.
    public interface IImplementation
    {
        string OperationImplementation();
    }

    // Fiecare implementare concretă corespunde unei platforme specifice și
    // implementează interfața de implementare folosind API-ul acestei platforme.
    class ConcreteImplementationA : IImplementation
    {
        public string OperationImplementation()
        {
            return "ConcreteImplementationA: The result in platform A.\n";
        }
    }

    class ConcreteImplementationB : IImplementation
    {
        public string OperationImplementation()
        {
            return "ConcreteImplementationA: The result in platform B.\n";
        }
    }

    class Client
    {
        // Cu excepția fazei de inițializare, când obiectul Abstraction
        // se leagă de un anumit obiect de implementare, codul clientului trebuie
        // depind doar de clasa Abstraction. Deci codul clientului
        // poate suporta orice combinație de abstractizare și implementare.
        public void ClientCode(Abstraction abstraction)
        {
            Console.Write(abstraction.Operation());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Abstraction abstraction;
            // Codul client ar trebui să funcționeze cu orice pre-configurat
            // o combinație configurată de abstractizare și implementeaza.
            abstraction = new Abstraction(new ConcreteImplementationA());
            client.ClientCode(abstraction);

            Console.WriteLine();

            abstraction = new ExtendedAbstraction(new ConcreteImplementationB());
            client.ClientCode(abstraction);
        }
    }
}