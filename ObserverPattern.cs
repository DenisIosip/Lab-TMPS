using System;
using System.Collections.Generic;
using System.Threading;

namespace RefactoringGuru.DesignPatterns.Observer.Conceptual
{
    public interface IObserver
    {
        // Obține actualizarea de la editor
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        // Atașează observatorul la editor.
        void Attach(IObserver observer);

        //Desprinde observatorul de editor.
        void Detach(IObserver observer);

        // Notifică toți observatorii despre eveniment.
        void Notify();
    }

    // Editorul deține o stare importantă și anunță observatorii despre
    // modificările sale.
    public class Subject : ISubject
    {
        // Pentru comoditate, această variabilă stochează starea editorului,
        // necesare tuturor abonaților.
        public int State { get; set; } = -0;

        // Lista abonaților. În viața reală, lista de abonați poate
        // stocat într-o formă mai detaliată (clasificată după tipul de eveniment și
        // etc.)
        private List<IObserver> _observers = new List<IObserver>();

        // Metode de gestionare a abonamentului.
        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        // Declanșează o actualizare în fiecare abonat.
        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        // De obicei, logica abonamentului este doar o parte din ceea ce face editorul.
        // Editorii conțin adesea o logică de afaceri importantă care
        // declanșează metoda de notificare ori de câte ori ar trebui să se întâmple ceva
        // important (sau după).
        public void SomeBusinessLogic()
        {
            Console.WriteLine("\nSubject: I'm doing something important.");
            this.State = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.WriteLine("Subject: My state has just changed to: " + this.State);
            this.Notify();
        }
    }

    //Observatorii specifici răspund la actualizările lansate de către editor
    // de care sunt atașate.
    class ConcreteObserverA : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State < 3)
            {
                Console.WriteLine("ConcreteObserverA: Reacted to the event.");
            }
        }
    }

    class ConcreteObserverB : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
            {
                Console.WriteLine("ConcreteObserverB: Reacted to the event.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Cod client.
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();
        }
    }
}