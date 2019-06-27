using MahApps.Metro.Controls;
using PapaJohnsCODE;
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

namespace PapaJohns
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }
        //Aca tenes que hacer la carga a los dos objetos nuevos esos, y mandarselos por parametro a window2 
        private void ViewTile_Click(object sender, RoutedEventArgs e)
        {
            Canvas canvas = new Canvas();
            Dictionary<Image,Mesa> mesas = new Dictionary<Image, Mesa>();
            Window2 window2 = new Window2(canvas, mesas);
            window2.Show();

        }
    }
}
