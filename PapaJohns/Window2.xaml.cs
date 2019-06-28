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
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace PapaJohns
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : MetroWindow
    {
        List<Mesa> mesas;
        public Window2()
        {
            InitializeComponent();
            //OPEN CANVAS


            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "CANVAS";
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
                    designCanvas.Children.Add(obj); // Add to canvas
                }
                designCanvas.Background = canvus.Background;
            }

            //OPEN RELACIONES

            XmlSerializer serializer = new XmlSerializer(typeof(List<Mesa>));

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "RELACIONES";
            ofd.DefaultExt = ".xaml"; // Default file extension
            ofd.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            ofd.ShowDialog();

            using (Stream reader = new FileStream(ofd.FileName, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                mesas = (List<Mesa>)serializer.Deserialize(reader);
            }

        }


        //============================
        //          CANVAS
        //============================

        // De-Serialize XML to UIElement using a given filename.
        public static UIElement DeSerializeXAML(string filename)
        {
            // Load XAML from file. Use 'using' so objects are disposed of properly.
            using (System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                return System.Windows.Markup.XamlReader.Load(fs) as UIElement;
            }
        }

        // Serializes any UIElement object to XAML using a given filename.
        public static void SerializeToXAML(UIElement element, string filename)
        {
            // Use XamlWriter object to serialize element
            string strXAML = System.Windows.Markup.XamlWriter.Save(element);

            // Write XAML to file. Use 'using' so objects are disposed of properly.
            using (System.IO.FileStream fs = System.IO.File.Create(filename))
            {
                using (System.IO.StreamWriter streamwriter = new System.IO.StreamWriter(fs))
                {
                    streamwriter.Write(strXAML);
                }
            }
        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "UIElement File"; // Default file name
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                SerializeToXAML(designCanvas, filename);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();


            // Process open file dialog box results
            if (result == true)
            {
                string filename = dlg.FileName;
                Canvas canvas = DeSerializeXAML(filename) as Canvas;

                // Add all child elements (lines, rectangles etc) to canvas
                while (canvas.Children.Count > 0)
                {
                    UIElement obj = canvas.Children[0]; // Get next child
                    canvas.Children.Remove(obj); // Have to disconnect it from result before we can add it
                    designCanvas.Children.Add(obj); // Add to canvas
                }
                designCanvas.Background = canvas.Background;
            }

        }


        //=========================
        //       RELATIONS
        //=========================
        private void SerializeObject(string filename)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(List<Mesa>));
            List<Mesa> i = new List<Mesa>();

            // Create an XmlTextWriter using a FileStream.
            Stream fs = new FileStream(filename, FileMode.Create);
            XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
            // Serialize using the XmlTextWriter.
            serializer.Serialize(writer, i);
            writer.Close();
        }

        private void DeserializeObject(string filename)
        {
            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(typeof(List<Mesa>));


            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                mesas = (List<Mesa>)serializer.Deserialize(reader);
            }
        }


        private void SaveButton_2_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "UIElement File"; // Default file name
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            dlg.ShowDialog();

            SerializeObject(dlg.FileName);

        }


        private void LoadButton_2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.DefaultExt = ".xaml"; // Default file extension
            ofd.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            ofd.ShowDialog();

            DeserializeObject(ofd.FileName);
        }
    }
}