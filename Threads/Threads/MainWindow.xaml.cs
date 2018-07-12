using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Threads
{
    public partial class MainWindow : Window
    {
        Thread t2;
        bool t2SollAbbrechen;
        object lockFürt2SollAbbrechen = new object();

        DispatcherTimer timer = new DispatcherTimer();
        double ergebnis;
        object lockFürErgebnis = new object();

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1.0);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lock(lockFürErgebnis)
            {
                textBox.Text = ergebnis.ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Thread t1 = new Thread(arbeiten1);
            t1.IsBackground = true;
            t1.Name = "mein erster";
            t1.Start();

            t2 = new Thread(arbeiten2);
            t2.IsBackground = true;
            t2.Name = "mein zweiter";
            t2.Start();

            Thread t3 = new Thread(arbeiten3);
            t3.IsBackground = true;
            t3.Name = "mein dritter";
            t3.Start();
        }

        void arbeiten1()
        {
            Random zufall = new Random();
            ulong versuche = 0;
            ulong treffer = 0;
            while (true)
            {
                versuche++;
                double x = zufall.NextDouble();
                double y = zufall.NextDouble();
                if (x * x + y * y < 1.0)
                {
                    treffer++;
                }
                if(versuche % 1000000 == 0)
                {
                    System.Diagnostics.Debug.WriteLine("1 " + 4.0 * treffer / versuche);
                }
            }
        }

        void arbeiten2()
        {
            Random zufall = new Random();
            ulong versuche = 0;
            ulong treffer = 0;
            bool abbrechen = false;
            while (!abbrechen)
            {
                versuche++;
                double x = zufall.NextDouble();
                double y = 2.0 * zufall.NextDouble();
                if (x * x + y * y < 1.0)
                {
                    treffer++;
                }
                if (versuche % 1000000 == 0)
                {
                    lock(lockFürErgebnis)
                    {
                        ergebnis = 4.0 * treffer / versuche;
                    }
                    System.Diagnostics.Debug.WriteLine("2 " + 4.0 * treffer / versuche);
                    //verboten: textBox1.Text = (4.0 * treffer / versuche).ToString();
                    Dispatcher.BeginInvoke(new Action(() => textBox1.Text = (4.0 * treffer / versuche).ToString() ));
                }

                lock(lockFürt2SollAbbrechen)
                {
                    abbrechen = t2SollAbbrechen;
                }
            }
        }

        void arbeiten3()
        {
            Random zufall = new Random();
            ulong versuche = 0;
            ulong treffer = 0;
            while (true)
            {
                versuche++;
                double x = 2.0 * zufall.NextDouble();
                double y = 2.0 * zufall.NextDouble();
                if (x * x + y * y < 1.0)
                {
                    treffer++;
                }
                if (versuche % 1000000 == 0)
                {
                    System.Diagnostics.Debug.WriteLine("3 " + 4.0 * treffer / versuche);
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            lock (lockFürt2SollAbbrechen)
            {
                t2SollAbbrechen = true;
            }
        }
    }
}
