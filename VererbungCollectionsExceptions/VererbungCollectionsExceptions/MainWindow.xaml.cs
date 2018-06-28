using System;
using System.Collections.Generic;
using System.IO;
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

namespace VererbungCollectionsExceptions
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int[,,] abc = new int[,,] { { { 1 } } };

            int k = 0;
            try
            {
                File.Delete(@"C:\Users\jl\Desktop\ordner 13\test 43.txt");
                string[] s = File.ReadAllLines(@"C:\Users\jl\Desktop\ordner 13\test 42.txt");
                for (; k < s.Length; k++)
                {
                    string[] teile = s[k].Split('\t');
                    string zeichenkette = teile[0];
                    int zahl1 = int.Parse(teile[1]);
                    int zahl2 = int.Parse(teile[2]);
                    File.AppendAllText(@"C:\Users\jl\Desktop\ordner 13\test 43.txt", zeichenkette + "\t" + (zahl1 + zahl2) + Environment.NewLine);
                }
            }
            catch(IndexOutOfRangeException)
            {
                MessageBox.Show("Die Zeile " + (k+1) + " war zu kurz.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Tier t = new Katze(42);
            t.Schlafe();
            t.Friss(new Nahrung());

            Tier.Klasse kl = Tier.Klasse.Amphibien;
            switch (kl)
            {
                case Tier.Klasse.Amphibien:
                    break;
                case Tier.Klasse.Reptilien:
                    break;
                case Tier.Klasse.Säugetiere:
                    break;
                case Tier.Klasse.Vögel:
                    break;
                default:
                    break;
            }

            //Array
            Tier[] a = new Tier[10];
            a[2] = t;
            a[3] = t;
            a[9] = new Katze(13);
            a[9].Schlafe();
            // a[8] = new Tier(4); Fehler, weil Tier abstract ist.
            int d = a.Length;

            //Collections
            List<Tier> b = new List<Tier>();
            b.Add(t);
            b.Add(t);
            b.Add(new Katze(7));
            int c = b.Count;
            b.RemoveAt(1); // Die späteren rutschen nach!
            b.Insert(0, new Katze(3));
            b[1].Schlafe();
            b[2] = new Katze(9);
            Queue<Tier> f = new Queue<Tier>();
            f.Enqueue(t);
            f.Enqueue(t);
            Tier g = f.Dequeue();
            Stack<Tier> h = new Stack<Tier>();
            h.Push(t);
            h.Push(t);
            Tier i = h.Pop();
            //in der Klammer ein Lambda-Ausdruck
            int j = b.Sum(x => 42 * x.Alter);
        }
    }

    class Nahrung
    {
    }

    abstract class Tier
    {
        int alter;
        public int Alter { get { return alter; } }

        public enum Klasse { Amphibien, Reptilien, Säugetiere, Vögel };

        public Tier(int alter)
        {
            this.alter = alter;
        }

        public virtual void Friss(Nahrung n)
        {
            // ...
        }

        public abstract void Schlafe();
    }

    class Katze : Tier
    {
        public Katze(int xyz)
            : base(3 * xyz)
        {

        }

        public override void Friss(Nahrung n)
        {
            // ...
            base.Friss(n); // könnte man weglassen
            // ...
        }

        public override void Schlafe()
        {
            // ...
        }
    }
}
