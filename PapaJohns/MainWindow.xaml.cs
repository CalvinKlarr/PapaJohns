using MahApps.Metro.Controls;
using Microsoft.Win32;
using PapaJohnsCODE;
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
using System.Xml.Serialization;

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
             List<Mesa> mesas;

        //OPEN CANVAS

        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
          //  dlg.FileName = "CANVAS";
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string filename = dlg.FileName;
                Canvas canvus = Window1.DeSerializeXAML(filename) as Canvas;

                // Add all child elements (lines, rectangles etc) to canvas
                while (canvus.Children.Count > 0)
                {
                    UIElement obj = canvus.Children[0]; // Get next child
                    canvus.Children.Remove(obj); // Have to disconnect it from result before we can add it
                    canvas.Children.Add(obj); // Add to canvas
                }
            }

            //OPEN RELACIONES

            XmlSerializer serializer = new XmlSerializer(typeof(List<Mesa>));

            OpenFileDialog ofd = new OpenFileDialog();
           // ofd.FileName = "RELACIONES";
            ofd.DefaultExt = ".xaml"; // Default file extension
            ofd.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            ofd.ShowDialog();

            using (Stream reader = new FileStream(ofd.FileName, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                mesas = (List<Mesa>)serializer.Deserialize(reader);
            }


            Window2 window2 = new Window2(canvas, mesas);
            window2.Show();

        }

    }
}
