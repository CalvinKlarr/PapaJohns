using MahApps.Metro.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PapaJohns
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : MetroWindow
    {
        private Point mouseClick;
        private Image draggedImage;
        private double rotation = 0;
        private List<Mesa> mesas;
        private int cont;
        private Mesa mesa;
        
        public Window1()
        {
            InitializeComponent();
            toolBox.MouseDoubleClick += ToolBox_MouseDoubleClick;
            mesas = new List<Mesa>();
            backgroundChoice.SelectedItem = pisoDos;
            sizeChoice.SelectedItem = sizeOne;
            cont = 0;


        }

        private void ToolBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (toolBox.SelectedItem != null)
            {
                ListBoxItem lbItem = (ListBoxItem)toolBox.SelectedItem;
                

                StackPanel stack = (StackPanel)lbItem.Content;

                TextBlock text;
                string selected = "";
                foreach (var child in stack.Children)
                {
                    if(child.GetType().ToString() == "System.Windows.Controls.TextBlock")
                    {
                        text = (TextBlock) child;
                        selected = text.Text;
                    }
                }
                
                if(selected == " Mesa")
                {
                    Image table = new Image();
                    table.Source = new BitmapImage(new Uri("table.png", UriKind.Relative));
                    table.Height = 50;
                    table.Width = 50;
                    table.Name = "mesa" + cont;
                    cont++;
                    Canvas.SetLeft(table, 0);
                    Canvas.SetTop(table, 0);
                    
                    mesa = new Mesa();
                    mesa.ImageName = table.Name;
                    
                    mesas.Add(mesa);
                    designSpace.Children.Add(table);
                    
                }
                if(selected == " Silla")
                {
                    Image chair = new Image();
                    chair.Source = new BitmapImage(new Uri("chair.png", UriKind.Relative));
                    chair.Height = 30;
                    chair.Width = 30;
                    Canvas.SetLeft(chair, 0);
                    Canvas.SetTop(chair, 0);
                    designSpace.Children.Add(chair);
                }
                if (selected == " Mesa redonda")
                {
                    Image rtable = new Image();
                    rtable.Source = new BitmapImage(new Uri("roundtable.png", UriKind.Relative));
                    rtable.Height = 50;
                    rtable.Width = 50;
                    rtable.Name = "mesa" + cont;
                    cont++;
                    Canvas.SetLeft(rtable, 0);
                    Canvas.SetTop(rtable, 0);
                    
                    mesa = new Mesa();
                    mesa.ImageName = rtable.Name;
                    mesas.Add(mesa);
                    designSpace.Children.Add(rtable);
                    //DICK

                }
                if (selected == " Taburete")
                {
                    Image stool = new Image();
                    stool.Source = new BitmapImage(new Uri("stool.png", UriKind.Relative));
                    stool.Height = 30;
                    stool.Width = 30;
                    Canvas.SetLeft(stool, 0);
                    Canvas.SetTop(stool, 0);
                    designSpace.Children.Add(stool);
                }
                if(selected == " Pared")
                {
                    Image wall = new Image();
                    wall.Source = new BitmapImage(new Uri("wall.png", UriKind.Relative));
                    wall.Height = 50;
                    wall.Width = 50;
                    Canvas.SetLeft(wall, 0);
                    Canvas.SetTop(wall, 0);
                    designSpace.Children.Add(wall);
                }
            }
        }

        private void DesignSpace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = e.Source as Image;

            if(image != null && designSpace.CaptureMouse())
            {
                mouseClick = e.GetPosition(designSpace);
                draggedImage = image;
                Panel.SetZIndex(draggedImage, 1);
            }

        }

        private void DesignSpace_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(draggedImage != null)
            {
                designSpace.ReleaseMouseCapture();
                Panel.SetZIndex(draggedImage, 0);
                draggedImage = null;
            }

        }

        private void DesignSpace_MouseMove(object sender, MouseEventArgs e)
        {
            if(draggedImage != null)
            {
                var position = e.GetPosition(designSpace);
                var offset = position - mouseClick;
                mouseClick = position;
                Canvas.SetLeft(draggedImage, Canvas.GetLeft(draggedImage) + offset.X);
                Canvas.SetTop(draggedImage, Canvas.GetTop(draggedImage) + offset.Y);
            }

        }

        private void SizeChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sizeChoice.SelectedItem.Equals(sizeOne))
            {
                designSpace.Height = 600;
                designSpace.Width = 800;

            }

            if (sizeChoice.SelectedItem.Equals(sizeTwo))
            {
                designSpace.Height = 400;
                designSpace.Width = 600;
            }

            if (sizeChoice.SelectedItem.Equals(sizeThree))
            {
                designSpace.Height = 200;
                designSpace.Width = 400;
            }

        }

        private void BackgroundChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageBrush ib = new ImageBrush();

            if (backgroundChoice.SelectedItem.Equals(pisoUno))
            {
                ib.ImageSource = new BitmapImage(new Uri("../../board.png", UriKind.Relative));
                ib.Stretch = Stretch.Fill;
                designSpace.Background = ib;
                
            }
            if (backgroundChoice.SelectedItem.Equals(pisoDos))
            {
                ib.ImageSource = new BitmapImage(new Uri("../../floorboard.png", UriKind.RelativeOrAbsolute));
                ib.Stretch = Stretch.Fill;
                designSpace.Background = ib;
            }
            
        }

        private void DesignSpace_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = e.Source as Image;
            if(image != null)
            {
                if(rotation >= 360) {
                    rotation = 0;

                }
                rotation = rotation + 90;

                RotateTransform rotate = new RotateTransform(rotation);
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                image.RenderTransform = rotate;
            }
        }

        private void DesignSpace_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var image = e.Source as Image;
            if (image != null)
            {
                designSpace.Children.Remove(image);
                var mesa = mesas.Find(x => x.ImageName == image.Name);
                mesas.Remove(mesa);
            }
        }

        //This no anda jeje
        private void ClearCanvas()
        {
            // Remove existing segments.
            for (int i = designSpace.Children.Count - 1; i >= 0; i--)
            {
                if (designSpace.Children[i] is Line)
                    
                    designSpace.Children.RemoveAt(i);
            }
        }




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


        //=================================================================================
        //SAVE AND LOAD DE CANVAS
        //=================================================================================

        //En este evento se realiza el guardado, las cosas a guardar son el Canvas, nombre designSpace y el diccionario mesas
        //idealmente hay que guardarlos en un mismo archivo.
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
                SerializeToXAML(designSpace, filename);
            }
        }

        //Aca hace una carga, tenes que cargar el objeto designSpace, osea sacarlo del archivo y asignarlo al que ya esta
        //y lo mismo con el diccionario
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Remove existing segments.
            ClearCanvas();

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
                    designSpace.Children.Add(obj); // Add to canvas
                }
            }

        }




        //=================================================================================
        //SAVE AND LOAD DE MESAS
        //=================================================================================




    /*    public static void SerializeToXML(MainWindow window, Mesa mesitas, int dpi, string filename)
        {
            string mystrXAML = XamlWriter.Save(mesitas);
            FileStream filestream = File.Create(filename);
            StreamWriter streamwriter = new StreamWriter(filestream);
            streamwriter.Write(mystrXAML);
            streamwriter.Close();
            filestream.Close();
        }*/

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



        private void SaveButton_2_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "UIElement File"; // Default file name
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            dlg.ShowDialog();

            SerializeObject(dlg.FileName);


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



