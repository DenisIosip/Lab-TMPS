namespace RefactoringGuru.DesignPatterns.FactoryMethod.Conceptual
{
    // Clasa Creator declară o metodă din fabrică care trebuie să revină
    // obiect de clasă Product. Subclasele de creatori oferă de obicei
    // implementarea acestei metode.
    abstract class Creator
    {
        // Rețineți că Creatorul poate oferi și o implementare
        // metoda implicită din fabrică.
        public abstract IProduct FactoryMethod();

        // De asemenea, rețineți că, în ciuda numelui, responsabilitatea principală
        // Creator nu este despre crearea de produse. De obicei conține
        // o logică de bază de afaceri care se bazează pe obiecte
        // Produse returnate prin metoda fabricii. Subclasele pot indirect
        // modifică această logică de afaceri, suprascriind metoda din fabrică și revenind
        // alt tip de produs din acesta.
        public string SomeOperation()
        {
            // Apelați metoda din fabrică pentru a obține obiectul produs.
            var product = FactoryMethod();
            // Apoi, lucrați cu acest produs.
            var result = "Creator: The same creator's code has just worked with "
                + product.Operation();

            return result;
        }
    }

    // Creatorii  înlocuiesc metoda din fabrică pentru a
    // schimba tipul produsului rezultat.
    class ConcreteCreator1 : Creator
    {
        // Rețineți că semnătura metodei folosește în continuare tipul
        // produs abstract, deși returnat de fapt din metodă
        // produs specific. Deci Creatorul poate rămâne
        // independent de clasele specifice de produse.
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct1();
        }
    }

    class ConcreteCreator2 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct2();
        }
    }

    // Interfața de produs declară operațiunile pe care toate
    // produse specifice.
    public interface IProduct
    {
        string Operation();
    }

    // Produse specifice oferă diferite implementări de interfață
    // Produs.
    class ConcreteProduct1 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct1}";
        }
    }

    class ConcreteProduct2 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct2}";
        }
    }

    class Client
    {
        public void Main()
        {
            Console.WriteLine("App: Launched with the ConcreteCreator1.");
            ClientCode(new ConcreteCreator1());

            Console.WriteLine("");

            Console.WriteLine("App: Launched with the ConcreteCreator2.");
            ClientCode(new ConcreteCreator2());
        }

        // Codul client funcționează cu o instanță a unui anumit creator, deși
        // prin interfața de bază. În timp ce clientul continuă să lucreze cu
        // de către creator prin interfața de bază, puteți trece oricare
        // subclasa creator.
        public void ClientCode(Creator creator)
        {
            // ...
            Console.WriteLine("Client: I'm not aware of the creator's class," +
                "but it still works.\n" + creator.SomeOperation());
            // ...
        }
    }

    class FactoryMethod
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }
}
