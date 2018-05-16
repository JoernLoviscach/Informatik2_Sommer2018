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
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < gebäude.Length; i++)
            {
                gebäude[i].Zeichne(Zeichenfläche);
            }
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

        public Gebäude(Polygon umriss, string id, ushort bauart, string baur_text, string signa_text)
        {
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
            return 42.0; // TO DO
        }
    }
}
