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
            Signal s = new Sinusschwingung(13.0, 5.0);
            Zeichenfläche.Children.Clear();
            Polyline p = new Polyline();
            p.Stroke = Brushes.Blue;
            p.StrokeThickness = 2.0;
            Zeichenfläche.Children.Add(p);
            for (int i = 0; i < 1000; i++)
            {
                double t = 0.001 * i;
                double x = Zeichenfläche.Width * i / 1000;
                double y = Zeichenfläche.Height * (0.5 - 0.1 * s.GibWert(t));
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
    }
}
