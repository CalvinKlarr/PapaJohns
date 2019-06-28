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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : MetroWindow
    {
        List<Mesa> mesas;
        public Window2(Canvas design, List<Mesa> dict)
        {
            designSpace = design;
            mesas = dict;
            InitializeComponent();

           
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadButton_2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
