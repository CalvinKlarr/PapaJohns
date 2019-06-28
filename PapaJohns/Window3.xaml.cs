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
using System.Windows.Shapes;

namespace PapaJohns
{
    /// <summary>
    /// Interaction logic for selectMozoWindow.xaml
    /// </summary>
    public partial class selectMozoWindow : MetroWindow
    {
        private Mozo moz;
        public Mozo Moz { get { return moz; } set { moz = value; } }
        public selectMozoWindow(List<Mozo> mozos)
        {
            InitializeComponent();
            mozoBox.ItemsSource = mozos;
        }

        private void MozoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mozo mozo = mozoBox.SelectedItem as Mozo;
            moz = mozo;
            Close();
        }
    }
}
