using System;

namespace RefactoringGuru.DesignPatterns.Singleton.Conceptual.NonThreadSafe
{
    // Clasa Singleton oferă o metodă `GetInstance` care se comportă ca
    // constructor alternativ și permite clienților să obțină același lucru
    // o instanță a clasei la fiecare apel.

    // Singleton-ul ar trebui să fie întotdeauna o clasă „sigilată” pentru a preveni clasa
    // moștenire prin clase externe și, de asemenea, prin clase imbricate.
    public sealed class Singleton
    {
        // Constructorul Singleton ar trebui să fie întotdeauna ascuns pentru a preveni
        // creează un obiect prin operatorul nou.
        private Singleton() { }

        // Obiectul singleton este stocat într-un câmp de clasă static. Exista
        // mai multe moduri de a inițializa acest câmp și toate au diferite
        // avantaje și dezavantaje. În acest exemplu, vom lua în considerare cel mai simplu dintre
        // le, al căror dezavantaj este incapacitatea completă de a corect
        // lucrează într-un mediu cu mai multe fire.
        private static Singleton _instance;

        // Aceasta este o metodă statică care controlează accesul la instanța singleton.
        // La prima rulare, instanțiază un singleton și îl plasează
        // câmp static. La rulările ulterioare, se întoarce la client
        // obiect stocat într-un câmp static.
        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        // În sfârșit, orice singleton ar trebui să conțină o logică de afaceri,
        // care poate fi executat pe instanța sa.
        public void someBusinessLogic()
        {
            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Cod client.
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

            if (s1 == s2)
            {
                Console.WriteLine("Singleton works, both variables contain the same instance.");
            }
            else
            {
                Console.WriteLine("Singleton failed, variables contain different instances.");
            }
        }
    }
}
