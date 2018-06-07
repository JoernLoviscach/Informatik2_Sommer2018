using System;
using System.Collections.Generic;
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

namespace SignaleAlsKlassenMitVererbung
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // nur als Beispiel
            Hund h = new Hund();
            h.Friss(new Nahrung());
            //Polymorphie
            Tier t = new Hund();
            t.Friss(new Nahrung()); // die Methode Friss von Hund wird aufgerufen.
        }

        private void buttonZeichne_Click(object sender, RoutedEventArgs e)
        {
            // upcast: immer sicher, Casting nicht nötig
            Signal s = new Sinusschwingung(13.0, 5.0);
            Signal t23 = new Rechteckschwingung(11.0, 5.0);
            Signal u = new Summe(s, 0.5, t23, 0.3);
            Signal v = new Summe(u, 0.5, t23, -0.1);
            //Signal w = new Ableitung(v);
            double x42 = s.SchätzeNennwert(13.0, 42.0, 0.0001);

            // downcast: nicht sicher, Casting nötig
            // Exception mit: Sinusschwingung z = (Sinusschwingung)t23;

            Zeichenfläche.Children.Clear();
            Polyline p = new Polyline();
            p.Stroke = Brushes.Blue;
            p.StrokeThickness = 2.0;
            Zeichenfläche.Children.Add(p);
            for (int i = 0; i < 1000; i++)
            {
                double t = 0.001 * i;
                double x = Zeichenfläche.Width * i / 1000;
                double y = Zeichenfläche.Height * (0.5 - 0.1 * v.GibWert(t));
                p.Points.Add(new Point(x, y));
            }
        }
    }

    //übliches banales Beispiel
    class Nahrung
    {

    }

    class Tier
    {
        public virtual void Friss(Nahrung n)
        {
            //...
        }
    }

    class Hund : Tier
    {
        public void HoleZeitung()
        {
            //...
        }

        // überschreiben
        public override void Friss(Nahrung n)
        {
            base.Friss(n);
            Schlafe();
        }

        public void Schlafe()
        {
            //...
        }
    }

    class Katze : Tier
    {

    }

    class Pudel : Hund
    {
        bool istFrisiert;
        public bool IstFrisiert { get { return istFrisiert; } }
    }

    class Signal
    {
        public virtual double GibWert(double t)
        {
            return 0.0;
        }

        public double SchätzeNennwert(double startZeit,
            double endZeit, double schrittweite)
        {
            double summe = 0.0;
            int anzahlSchritte = 0;
            // Problem: Rundungsfehler für t?
            // Lieber keine double-Zahl in Bedingung von for-Schleife
            for (double t = startZeit; t < endZeit; t += schrittweite)
            {
                double y = GibWert(t);
                summe += y * y;  // Oder: Math.Pow(y, 2); nebenbei: y^2 ist XOR
                anzahlSchritte++;
            }
            double mittelwert = summe / anzahlSchritte;
            return Math.Sqrt(mittelwert);
        }

        public override string ToString()
        {
            return "mein Signal";
        }
    }

    class Sinusschwingung : Signal
    {
        double frequenz;
        double amplitude;
        
        public Sinusschwingung(double frequenz, double amplitude)
        {
            this.frequenz = frequenz;
            this.amplitude = amplitude;
        }

        public override double GibWert(double t)
        {
            return amplitude * Math.Sin(2.0 * Math.PI * frequenz * t);
        }

        public override string ToString()
        {
            return "Sinusschwingung mit Frequenz " + frequenz + " und Amplitude " + amplitude;
        }
    }

    class Rechteckschwingung : Signal
    {
        double frequenz;
        double amplitude;

        public Rechteckschwingung(double frequenz, double amplitude)
        {
            this.frequenz = frequenz;
            this.amplitude = amplitude;
        }

        public override double GibWert(double t)
        {
            double perioden = frequenz * t;
            double gebrochenePerioden = perioden - Math.Floor(perioden);
            // Aber Vorsicht mit Aliasing!
            return amplitude * (gebrochenePerioden > 0.5 ? -1.0 : 1.0);
        }
    }

    class Summe : Signal
    {
        Signal s1;
        double faktor1;
        Signal s2;
        double faktor2;

        public Summe(Signal s1, double faktor1, Signal s2, double faktor2)
        {
            this.s1 = s1;
            this.faktor1 = faktor1;
            this.s2 = s2;
            this.faktor2 = faktor2;
        }

        public override double GibWert(double t)
        {
            return faktor1 * s1.GibWert(t) + faktor2 * s2.GibWert(t);
        }
    }

    class Ableitung : Signal
    {
        Signal s;

        public Ableitung(Signal s)
        {
            this.s = s;
        }

        public override double GibWert(double t)
        {
            const double deltaT = 1e-10;
            return (s.GibWert(t + deltaT) - s.GibWert(t - deltaT)) / (2.0 * deltaT);
        }
    }
}
