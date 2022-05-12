sing System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RefactoringGuru.DesignPatterns.Memento.Conceptual
{
    // Creatorul conține o stare importantă care poate în cele din urmă
    // Schimbare. De asemenea, declară o metodă de salvare a stării în interiorul instantaneului și
    // metodă de restabilire a stării din ea.
    class Originator
    {
        // Pentru comoditate, starea creatorului este stocată într-o singură variabilă.
        private string _state;

        public Originator(string state)
        {
            this._state = state;
            Console.WriteLine("Originator: My initial state is: " + state);
        }

        // Logica de afaceri a Creatorului poate afecta starea sa internă.
        // Prin urmare, clientul trebuie să facă o copie de rezervă a stării cu
        // folosind metoda de salvare înainte de a rula metodele logicii de afaceri.
        public void DoSomething()
        {
            Console.WriteLine("Originator: I'm doing something important.");
            this._state = this.GenerateRandomString(30);
            Console.WriteLine($"Originator: and my state has changed to: {_state}");
        }

        private string GenerateRandomString(int length = 10)
        {
            string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;

            while (length > 0)
            {
                result += allowedSymbols[new Random().Next(0, allowedSymbols.Length)];

                Thread.Sleep(12);

                length--;
            }

            return result;
        }

        // Stochează starea curentă în interiorul instantaneului.
        public IMemento Save()
        {
            return new ConcreteMemento(this._state);
        }

        // Restabiliți starea Creator din obiectul instantaneu.
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            this._state = memento.GetState();
            Console.Write($"Originator: My state has changed to: {_state}");
        }
    }

    // Interfața Snapshot oferă o modalitate de a prelua metadatele instantanee, cum ar fi
    // ca data de creare sau titlu. Cu toate acestea, nu dezvăluie statul
    // Creator.
    public interface IMemento
    {
        string GetName();

        string GetState();

        DateTime GetDate();
    }

    // Un anumit instantaneu conține infrastructura pentru stocarea stării
    // Creator.
    class ConcreteMemento : IMemento
    {
        private string _state;

        private DateTime _date;

        public ConcreteMemento(string state)
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        // Creatorul folosește această metodă la restaurarea acesteia
        // condiție.
        public string GetState()
        {
            return this._state;
        }

        // Alte metode sunt folosite de Guardian pentru a afișa metadate.
        public string GetName()
        {
            return $"{this._date} / ({this._state.Substring(0, 9)})...";
        }

        public DateTime GetDate()
        {
            return this._date;
        }
    }

    // Guardian este independent de clasa Concrete Snapshot. Astfel el nu
    // are acces la starea creatorului stocată în interiorul instantaneului. El
    // funcționează cu toate instantaneele prin interfața de bază Snapshot.
    class Caretaker
    {
        private List<IMemento> _mementos = new List<IMemento>();

        private Originator _originator = null;

        public Caretaker(Originator originator)
        {
            this._originator = originator;
        }

        public void Backup()
        {
            Console.WriteLine("\nCaretaker: Saving Originator's state...");
            this._mementos.Add(this._originator.Save());
        }

        public void Undo()
        {
            if (this._mementos.Count == 0)
            {
                return;
            }

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);

            Console.WriteLine("Caretaker: Restoring state to: " + memento.GetName());

            try
            {
                this._originator.Restore(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine("Caretaker: Here's the list of mementos:");

            foreach (var memento in this._mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Cod client.
            Originator originator = new Originator("Super-duper-super-puper-super.");
            Caretaker caretaker = new Caretaker(originator);

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            Console.WriteLine();
            caretaker.ShowHistory();

            Console.WriteLine("\nClient: Now, let's rollback!\n");
            caretaker.Undo();

            Console.WriteLine("\n\nClient: Once more!\n");
            caretaker.Undo();

            Console.WriteLine();
        }
    }
}