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

namespace Datentypen
{
//    C: char, short, int, long; jeweils auch mit unsigned

//    C#: sbyte, short, int, long
//    byte, ushort, uint, ulong

//    byte: 0...255

//    255 =  11111111_2
//    256 = 100000000_2

//    sbyte = -128...0...127

//    127 = 01111111_2
//   -127 = 10000001_2
//    ----------------
//      0 = 00000000_2

//   -128 = 10000000_2  

    public partial class MainWindow : Window
    {
        enum Status { Betriebsbereit, Defekt, InWartung, Fehlt }

        Status meinZustand = Status.Betriebsbereit;

        public MainWindow()
        {
            InitializeComponent();

            sbyte a = 123;
            a += 20; // Überraschung!

            long b = long.MaxValue;

            double c = 1.14;
            float d = 2.72f;
            double doubleMax = double.MaxValue;
            float floatMax = float.MaxValue;

            double x = Math.Sqrt(-13.0);
            double y = Math.Sin(x);
            double z = Math.Acos(7.0);

            double u = 1.0 / 0.0;
            double v = 0.0 * u;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x = 0;
            for (double i = 0; i < 10000000; i += 0.1)
            {
                x++;
            } // Überraschung!

            string s = textBox.Text;
            long z = long.Parse(s);
            label.Content = 2 * z;
            long g = 1 / z; // Null eingeben: Teilen durch null, Fehler!

            switch (meinZustand)
            {
                case Status.Betriebsbereit:
                    meinZustand = Status.Defekt;
                    break;
                case Status.Defekt:
                    break;
                case Status.InWartung:
                    break;
                case Status.Fehlt:
                    break;
                default:
                    break;
            }
        }

        private void buttonZähle_Click(object sender, RoutedEventArgs e)
        {
            // C-Array mit 1, 2, 3:
            // int s[] = {1, 2, 3};

            string[] wörter = textBox1.Text.Split(new string[] { " ", "\r\n" },
                                            StringSplitOptions.RemoveEmptyEntries);
            label.Content = wörter.Length;
            Array.Sort(wörter);

            string aktuellesWort = "";
            int anzahl = 0;
            string bisherHäufigstesWort = "";
            int anzahlBisherAmHäufigsten = 0;
            for (int i = 0; i < wörter.Length; i++)
            {
                if(wörter[i] == aktuellesWort)
                {
                    anzahl++;
                }
                else
                {
                    aktuellesWort = wörter[i];
                    anzahl = 1;
                }

                if (anzahl > anzahlBisherAmHäufigsten)
                {
                    anzahlBisherAmHäufigsten = anzahl;
                    bisherHäufigstesWort = aktuellesWort;
                }
            }

            MessageBox.Show("Das häufigste Wort ist \"" + bisherHäufigstesWort + "\".");
        }
    }
}
