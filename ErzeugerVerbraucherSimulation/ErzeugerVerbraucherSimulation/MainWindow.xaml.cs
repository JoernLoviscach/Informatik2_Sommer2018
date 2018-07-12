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

namespace ErzeugerVerbraucherSimulation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // 1000 Waschmaschinen; über Tag plotten!
            List<Element> elemente = new List<Element>();
            for (int i = 0; i < 1000; i++)
            {
                elemente.Add(new Waschmaschine());
            }

            zeichenfläche.Children.Clear();
            Polyline poly = new Polyline();
            poly.Stroke = Brushes.Blue;
            poly.StrokeThickness = 2.0;
            zeichenfläche.Children.Add(poly);

            for (int viertelstunde = 0; viertelstunde < 24 * 4; viertelstunde++)
            {
                double uhrzeit = 0.25 * viertelstunde;

                // ok, aber langatmig:
                //double leistung = 0.0;
                //for (int i = 0; i < elemente.Count; i++)
                //{
                //    leistung += elemente[i].GibLeistungInWattZumZeitpunkt(uhrzeit);
                //}

                //besser, aber noch nicht perfekt:
                //double leistung = 0.0;
                //foreach (Element item in elemente)
                //{
                //    leistung += item.GibLeistungInWattZumZeitpunkt(uhrzeit);
                //}

                //heute so:
                double leistung = elemente.Sum(x => x.GibLeistungInWattZumZeitpunkt(uhrzeit));
                // Der Lambda-Ausdruck "x => ..." darin bedeutet:
                // double anonymeFunktion(Element x)
                // { return x.GibLeistungInWattZumZeitpunkt(uhrzeit)); }

                poly.Points.Add(new Point(uhrzeit * zeichenfläche.ActualWidth / 24.0, (1.0 - (leistung + 2e6) / 4e6 ) * zeichenfläche.ActualHeight ));
            }
        }
    }

    abstract class Element
    {
        public abstract double GibLeistungInWattZumZeitpunkt(double UhrzeitInStunden);
    }

    class Solaranlage : Element
    {
        public override double GibLeistungInWattZumZeitpunkt(double UhrzeitInStunden)
        {
            throw new NotImplementedException();
        }
    }

    class Waschmaschine : Element
    {
        static Random zufall = new Random();
        double start = 8.0 + 8.0 * zufall.NextDouble();

        public override double GibLeistungInWattZumZeitpunkt(double UhrzeitInStunden)
        {
            if(UhrzeitInStunden >= start && UhrzeitInStunden < start + 2.0)
            {
                if(UhrzeitInStunden > start + 0.333)
                {
                    return -200.0;
                }
                else
                {
                    return -2000.0;
                }
            }
            return 0.0;
        }
    }
}
