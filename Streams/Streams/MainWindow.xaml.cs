using Microsoft.Win32;
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

namespace Streams
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "mein Dateityp|*.bla|alle Typen|*.*";
            if (sfd.ShowDialog() == true)
            {
                Stream s = new FileStream(sfd.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(s);

                bw.Write(0.0);
                bw.Write("z");
                long hier = s.Position;
                bw.Write(42.3);
                bw.Write("abcäöü");
                bw.Write(12345678901234);
                s.Position = hier;
                bw.Write("qwertzuiopasdfghj");

                bw.Close();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "mein Dateityp|*.bla|alle Typen|*.*";
            if (ofd.ShowDialog() == true)
            {
                Stream s = new FileStream(ofd.FileName, FileMode.Open);
                BinaryReader br = new BinaryReader(s);

                double x = br.ReadDouble();
                string c = br.ReadString();

                br.Close();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "mein Dateityp|*.txt|alle Typen|*.*";
            if (sfd.ShowDialog() == true)
            {
                Stream s = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(s);

                sw.Write(0.0);
                sw.Write("z");
                sw.Write(42.3);
                sw.Write("abcäöü");
                sw.Write(12345678901234);

                sw.Close();
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "mein Dateityp|*.txt|alle Typen|*.*";
            if (ofd.ShowDialog() == true)
            {
                Stream s = new FileStream(ofd.FileName, FileMode.Open);
                StreamReader sr = new StreamReader(s);

                string st = sr.ReadToEnd();

                sr.Close();
            }
        }
    }
}
