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

namespace BasteleienMitZufall
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int a = 42;
        Random rand = new Random();

        private void buttonMeiner_Click(object sender, RoutedEventArgs e)
        {
            // der Mittelwert von 100 Zufallszahlen 
            long summe = 0;
            for (int i = 0; i < 100; i++)
            {
                summe += rand.Next(1, 7);
            }
            labelAusgabeMittelwert.Content = (summe/100.0).ToString();

            // Vorsicht mit dem (fehlenden) Semikolon!
            // for(;;) { }
            // while() { }
            // if() { } else { }
            // do { } while();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // 100x Würfeln -> Histogramm
            // in C: int anzahl[6] = {0};
            int[] anzahl = new int[6]; // auf 0 initialisiert
            for (int i = 0; i < 100; i++)
            {
                int z = rand.Next(1, 7);
                // Wenn z == 1 ist, erhöhe anzahl[0].
                // Wenn z == 2 ist, erhöhe anzahl[1].
                // ...
                // Wenn z == 6 ist, erhöhe anzahl[5].
                anzahl[z - 1]++;
            }

            zeichenfläche.Children.Clear();
            double breite = zeichenfläche.ActualWidth / 6.0;
            for (int j = 0; j < 6; j++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = breite;
                rect.Height = anzahl[j] / 100.0 * zeichenfläche.ActualHeight;
                rect.Fill = Brushes.Red;
                Canvas.SetLeft(rect, j * breite);
                Canvas.SetBottom(rect, 0.0);
                zeichenfläche.Children.Add(rect);
            }
        }
    }
}
