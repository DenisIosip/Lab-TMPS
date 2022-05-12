using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Builder.Conceptual
{
    // Interfața Builder declară metode de creare pentru diferite părți
    // Obiecte produs.
    public interface IBuilder
    {
        void BuildPartA();

        void BuildPartB();

        void BuildPartC();
    }

    // Clasele Concrete Builder urmează interfața Builder și oferă
    // implementări specifice ale etapelor de construcție. Programul dvs. poate avea
    // mai multe variante de Builders, implementate diferit.
    public class ConcreteBuilder : IBuilder
    {
        private Product _product = new Product();

        // Noua instanță de generator trebuie să conțină un obiect produs gol,
        // care este folosit în asamblarea ulterioară.
        public ConcreteBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._product = new Product();
        }

        // Toți pașii de producție funcționează pe aceeași instanță
        // produs.
        public void BuildPartA()
        {
            this._product.Add("PartA1");
        }

        public void BuildPartB()
        {
            this._product.Add("PartB1");
        }

        public void BuildPartC()
        {
            this._product.Add("PartC1");
        }

        // Constructorii conreti  trebuie să furnizeze propriile metode
        // obține rezultate. Acest lucru se datorează faptului că diferite tipuri
        // constructorii pot crea produse complet diferite cu diferite
        // interfețe. Prin urmare, astfel de metode nu pot fi declarate în bază
        // Interfață Builder (cel puțin într-un tip static
        // limbaj de programare).
        //
        // De obicei, după returnarea rezultatului final către client,
        // Instanța de constructor trebuie să fie pregătită pentru a începe producția
        // următorul produs. Prin urmare, este o practică obișnuită să numiți metoda
        // resetați la sfârșitul corpului metodei GetProduct. Cu toate acestea, acest comportament nu este
        // este necesar, vă puteți face constructorii să aștepte
        // cerere explicită de resetare din codul clientului înainte de a scăpa de
        // rezultatul anterior.
        public Product GetProduct()
        {
            Product result = this._product;

            this.Reset();

            return result;
        }
    }

    // Are sens să utilizați modelul Builder doar atunci când dvs
    // produsele sunt destul de complexe și necesită o configurație extinsă.
    //
    // Spre deosebire de alte modele de generație, diverși constructori
    // poate produce produse care nu au legătură. Cu alte cuvinte, rezultatele
    // diferiți constructori pot să nu urmeze întotdeauna același lucru
    // interfață.
    public class Product
    {
        private List<object> _parts = new List<object>();

        public void Add(string part)
        {
            this._parts.Add(part);
        }

        public string ListParts()
        {
            string str = string.Empty;

            for (int i = 0; i < this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }

            str = str.Remove(str.Length - 2); // removing last ",c"

            return "Product parts: " + str + "\n";
        }
    }

    // Directorul este responsabil doar pentru executarea pașilor de construire într-un anumit
    // secvențe. Acest lucru este util atunci când se produc produse într-un anumit
    // comandă sau configurație specială. Strict vorbind, director de clasă
    // opțional deoarece clientul poate controla direct constructorii.
    public class Director
    {
        private IBuilder _builder;

        public IBuilder Builder
        {
            set { _builder = value; }
        }

        // Director poate construi mai multe variante de produs folosind
        // pași de construcție identici.
        public void BuildMinimalViableProduct()
        {
            this._builder.BuildPartA();
        }

        public void BuildFullFeaturedProduct()
        {
            this._builder.BuildPartA();
            this._builder.BuildPartB();
            this._builder.BuildPartC();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Codul client creează un obiect builder, îl transmite directorului,
            // și apoi inițiază procesul de construire. Rezultat final
            // preluat din obiectul constructor.
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Builder = builder;

            Console.WriteLine("Standard basic product:");
            director.BuildMinimalViableProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Standard full featured product:");
            director.BuildFullFeaturedProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            // Amintiți-vă că modelul Builder poate fi folosit fără o clasă
            // Director.
            Console.WriteLine("Custom product:");
            builder.BuildPartA();
            builder.BuildPartC();
            Console.Write(builder.GetProduct().ListParts());
        }
    }
}