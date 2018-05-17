using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KlassenFürKataster
{
    // Daten: https://open-data.bielefeld.de/dataset/alkis-geb%C3%A4ude-bauteile
    // Der Einfachheit halber in dieser Datei "),(" durch "," ersetzt.

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            A a1 = new A(13); // class, also Referenztyp
            A a2 = a1;
            a1.X = 42;
            int x2 = a2.X; // 42

            B b1 = new B(13); // struct, also Werttyp
            B b2 = b1;
            b1.Y = 42;
            int y2 = b2.Y; // 13

            InitializeComponent();

            // nur zur Erinnerung:
            int    x = 42;
            Person a = new Person("Egon", "Müller", 42);
            // nicht möglich: a.vorname = "Egon";
            string n = a.Vorname;
            int u = a.BerechneDoppeltesAlter();
            Person b = new Person("Berta", "Schmitz", 75);
            u = b.BerechneDoppeltesAlter();
        }

        Gebäude[] gebäude;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string[] zeilen = System.IO.File.ReadAllLines(@"C:\Users\jl\Desktop\result.csv");
            int anzahlGebäude = zeilen.Length - 1;
            gebäude = new Gebäude[anzahlGebäude];
            for (int i = 0; i < anzahlGebäude; i++)
            {
                // Professionell macht man dies mit einem "regulären Ausdruck".
                string[] teile = zeilen[i + 1].Split(';');
                string u = teile[0].Substring(11, teile[0].Length - 14);
                string[] punkte = u.Split(',');
                Polygon polygon = new Polygon();
                polygon.ToolTip = i.ToString();
                polygon.Fill = Brushes.Blue;
                polygon.FillRule = FillRule.Nonzero;
                polygon.Stroke = Brushes.Red;
                polygon.StrokeThickness = 1.0;
                for (int k = 0; k < punkte.Length - 1; k++)
                {
                    string[] koordinaten = punkte[k].Split(' ');
                    polygon.Points.Add(
                        new Point(
                            0.2 * (-464000 + double.Parse(koordinaten[0], CultureInfo.InvariantCulture)),
                            Zeichenfläche.Height - 0.2 *(-5765300 + double.Parse(koordinaten[1], CultureInfo.InvariantCulture))
                            ));
                }
                gebäude[i] = new Gebäude(polygon, teile[1], ushort.Parse(teile[2]), teile[3], teile[4]);
            }

            int zahl = Gebäude.GesamtanzahlGebäude;

            double f = gebäude[18792].BerechneFläche();

            int minPunkte = int.MaxValue;
            int maxPunkte = 0;
            long summePunktzahl = 0;
            for (int i = 0; i < anzahlGebäude; i++)
            {
                int z = gebäude[i].ZahlDerPunkte;
                if(z < minPunkte)
                {
                    minPunkte = z;
                }
                if (z > maxPunkte)
                {
                    maxPunkte = z;
                }
                summePunktzahl += z;
            }
            double durchschnittlichePunktzahl
                = summePunktzahl / (double)anzahlGebäude;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < gebäude.Length; i++)
            {
                gebäude[i].Zeichne(Zeichenfläche);
            }
        }
    }

    // class: Referenztyp
    // Man bekommt eine Referenz (vgl. Pointer).
    class A 
    {
        int x;
        public int X { get { return x; } set { x = value; } }
        public A(int x)
        {
            this.x = x;
        }
    }

    // struct: Werttyp
    // Man bekommt wirkliche Bytes.
    struct B
    {
        int y;
        public int Y { get { return y; } set { y = value; } }
        public B(int y)
        {
            this.y = y;
        }
    }

    class Person // als dummes Beispiel
    {
        string nachname; // Feld* = Attribut = member variable
                         // *Es ist kein Array gemeint!
        string vorname;
        public string Vorname { get { return vorname; } } // Eigenschaft = property
        ushort alter;

        // kein Rückgabetyp und heißt wie die Klasse
        public Person(string vorname, string nachname, ushort alter) // Konstruktor = constructor = ctor
        {
            this.nachname = nachname;
            this.vorname = vorname;
            this.alter = alter;
        }

        public ushort BerechneDoppeltesAlter() // Methode = member function
        {
            return (ushort)(2 * alter);
        }
    }

    class Gebäude
    {
        Polygon umriss;
        string id;
        ushort bauart;
        string baur_text;
        string signa_text;

        static int gesamtanzahlGebäude; // "static": bezieht sich auf Klasse statt Instanz
        static public int GesamtanzahlGebäude { get { return gesamtanzahlGebäude; } }

        public int ZahlDerPunkte { get { return umriss.Points.Count; } }

        public Gebäude(Polygon umriss, string id, ushort bauart, string baur_text, string signa_text)
        {
            gesamtanzahlGebäude++;
            this.umriss = umriss;
            this.id = id;
            this.bauart = bauart;
            this.baur_text = baur_text;
            this.signa_text = signa_text;
        }

        public void Zeichne(Canvas zeichenfläche)
        {
            zeichenfläche.Children.Add(umriss);
        }

        public double BerechneFläche()
        {
            double summe = 0.0;
            Point p0 = umriss.Points[0];
            for (int i = 1; i < ZahlDerPunkte - 1; i++)
            {
                Point pi = umriss.Points[i];
                Point piPlus1 = umriss.Points[i + 1];
                summe += 0.5 * ( ( pi.X - p0.X ) * (piPlus1.Y - p0.Y) - (pi.Y - p0.Y) * (piPlus1.X - p0.X));
            }
            return summe / (0.2 * 0.2); // Korrektur für Zoom
        }
    }
}
