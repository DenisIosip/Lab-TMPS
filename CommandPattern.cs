using System;

namespace RefactoringGuru.DesignPatterns.Command.Conceptual
{
    // Interfața Command declară o metodă de executare a comenzilor.
    public interface ICommand
    {
        void Execute();
    }

    // Unele comenzi sunt capabile să efectueze operațiuni simple pe cont propriu.
    class SimpleCommand : ICommand
    {
        private string _payload = string.Empty;

        public SimpleCommand(string payload)
        {
            this._payload = payload;
        }

        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: See, I can do simple things like printing ({this._payload})");
        }
    }

    // Dar există și comenzi care deleg operațiuni mai complexe altora
    // obiecte numite „destinatari”.
    class ComplexCommand : ICommand
    {
        private Receiver _receiver;

        // Datele de context necesare pentru a rula metodele receptorului.
        private string _a;

        private string _b;

        // Comenzile complexe pot lua unul sau mai multe obiecte-
        // destinatari, împreună cu orice date de context prin intermediul constructorului.
        public ComplexCommand(Receiver receiver, string a, string b)
        {
            this._receiver = receiver;
            this._a = a;
            this._b = b;
        }

        // Comenzile pot delega execuția oricărei metode de receptor.
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");
            this._receiver.DoSomething(this._a);
            this._receiver.DoSomethingElse(this._b);
        }
    }

    // Clasele Receiver conțin o logică comercială importantă. Ei sunt capabili
    // efectuează tot felul de operațiuni legate de executarea cererii. De fapt,
    // orice clasă poate fi un Receptor.
    class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: Working on ({a}.)");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: Also working on ({b}.)");
        }
    }

    // Expeditorul este asociat cu una sau mai multe comenzi. El trimite
    // cerere de comandă.
    class Invoker
    {
        private ICommand _onStart;

        private ICommand _onFinish;

        // Inițializarea comenzii
        public void SetOnStart(ICommand command)
        {
            this._onStart = command;
        }

        public void SetOnFinish(ICommand command)
        {
            this._onFinish = command;
        }

        // Expeditorul este independent de clasele specifice de comandă și receptor.
        // Expeditorul transmite indirect cererea către destinatar prin executarea comenzii.
        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Does anybody want something done before I begin?");
            if (this._onStart is ICommand)
            {
                this._onStart.Execute();
            }

            Console.WriteLine("Invoker: ...doing something really important...");

            Console.WriteLine("Invoker: Does anybody want something done after I finish?");
            if (this._onFinish is ICommand)
            {
                this._onFinish.Execute();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Codul clientului poate parametriza expeditorul cu oricare
            // comenzi.
            Invoker invoker = new Invoker();
            invoker.SetOnStart(new SimpleCommand("Say Hi!"));
            Receiver receiver = new Receiver();
            invoker.SetOnFinish(new ComplexCommand(receiver, "Send email", "Save report"));

            invoker.DoSomethingImportant();
        }
    }
}