using System;

namespace RefactoringGuru.DesignPatterns.Composite.Conceptual
{
    // Interfața de bază a componentei definește comportamentul care este schimbat
    // decoratori.
    public abstract class Component
    {
        public abstract string Operation();
    }

    // Componentele concrete oferă implementări implicite de comportament.
    // Pot exista mai multe variante ale acestor clase.
    class ConcreteComponent : Component
    {
        public override string Operation()
        {
            return "ConcreteComponent";
        }
    }
    // Clasa de bază Decorator urmează aceeași interfață ca și celelalte
    // Componente. Scopul principal al acestei clase este de a defini o interfață wrapper pentru
    // toți decoratorii de beton. Implementarea implicită a codului wrapper poate
    // include un câmp pentru a stoca componenta înfășurată și mijloacele acesteia
    // inițializare.
    abstract class Decorator : Component
    {
        protected Component _component;

        public Decorator(Component component)
        {
            this._component = component;
        }

        public void SetComponent(Component component)
        {
            this._component = component;
        }

        // Decoratorul deleagă toată munca componentei înfășurate.
        public override string Operation()
        {
            if (this._component != null)
            {
                return this._component.Operation();
            }
            else
            {
                return string.Empty;
            }
        }
    }
    // Decoratorii de beton apelează obiectul înfășurat și îi schimbă rezultatul
    // într-un fel.
    class ConcreteDecoratorA : Decorator
    {
        public ConcreteDecoratorA(Component comp) : base(comp)
        {
        }

        // Decoratorii pot apela implementarea părinte a operației, în loc de
        // pentru a apela direct obiectul înfășurat. Această abordare simplifică
        // extinderea orelor de decorator.
        public override string Operation()
        {
            return $"ConcreteDecoratorA({base.Operation()})";
        }
    }

    // Decoratorii își pot face comportamentul înainte sau după apelarea înfășurării
    // obiect.
    class ConcreteDecoratorB : Decorator
    {
        public ConcreteDecoratorB(Component comp) : base(comp)
        {
        }

        public override string Operation()
        {
            return $"ConcreteDecoratorB({base.Operation()})";
        }
    }

    public class Client
    {
        // Codul client funcționează cu toate obiectele care utilizează interfața
        // Componentă. Astfel, rămâne independent de specific
        // clase de componente cu care să lucrați.
        public void ClientCode(Component component)
        {
            Console.WriteLine("RESULT: " + component.Operation());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            var simple = new ConcreteComponent();
            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(simple);
            Console.WriteLine();

            // ...si decorat.
            //
            // decoratorii pot împacheta nu numai
            // componente simple, dar și alți decoratori.
            ConcreteDecoratorA decorator1 = new ConcreteDecoratorA(simple);
            ConcreteDecoratorB decorator2 = new ConcreteDecoratorB(decorator1);
            Console.WriteLine("Client: Now I've got a decorated component:");
            client.ClientCode(decorator2);
        }
    }
}
