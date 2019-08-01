using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using PapaJohnsCODE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;

namespace PapaJohns
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : MetroWindow
    {
        private List<Mesa> mesas;
        private Image rightClicked;
        private List<Mozo> mozos;


        public Window2()
        {
            InitializeComponent();
            mozos = new List<Mozo>();


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
                    var obj = canvus.Children[0] as Image; // Get next child
                    
                    if (obj.Name.Contains("mesa"))
                    {
                        obj.MouseRightButtonDown += Obj_MouseRightButtonDown;
                        obj.MouseLeftButtonDown += Obj_MouseLeftButtonDown;
                        

                    }
                    canvus.Children.Remove(obj); // Have to disconnect it from result before we can add it

                    designCanvas.Children.Add(obj); // Add to canvas

                }
                designCanvas.Height = canvus.Height;
                designCanvas.Width = canvus.Width;
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
                if (mesas != null)
                {

                }
            }

        }

        private void Obj_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var im = sender as Image;
            if(im != null) {
                Mesa mes = mesas.Find(x => x.ImageName == im.Name);
                ToolTip tp = new ToolTip();
                tp.Content = mes.ToString();
                im.ToolTip = tp;
            }
            

            
        }

        private void Obj_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu cm = this.FindResource("mContext") as ContextMenu;
            cm.PlacementTarget = sender as Image;
            rightClicked = sender as Image;
            cm.IsOpen = true;
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


            string filenamerelations = dlg.FileName + "_Relations.xaml";

            SerializeObject(filenamerelations);





        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            //CARGAR LAS RELACIONES
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "RELACIONES";
            ofd.DefaultExt = ".xaml"; // Default file extension
            ofd.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            ofd.ShowDialog();

            DeserializeObject(ofd.FileName);

            //CARGAR EL CANVAS

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "CANVAS";
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            designCanvas.Children.Clear();


            // Process open file dialog box results
            if (result == true)
            {
                string filename = dlg.FileName;
                Canvas canvas = DeSerializeXAML(filename) as Canvas;

                // Add all child elements (lines, rectangles etc) to canvas
                while (canvas.Children.Count > 0)
                {
                    var obj = canvas.Children[0] as Image; // Get next child
                    if (obj.Name.Contains("mesa"))
                    {
                        obj.MouseRightButtonDown += Obj_MouseRightButtonDown;
                        obj.MouseLeftButtonDown += Obj_MouseLeftButtonDown;

                    }
                    canvas.Children.Remove(obj); // Have to disconnect it from result before we can add it
                    designCanvas.Children.Add(obj); // Add to canvas
                }
                designCanvas.Background = canvas.Background;
                designCanvas.Height = canvas.Height;
                designCanvas.Width = canvas.Width;
            }

        }


        //=========================
        //       RELATIONS
        //=========================
        private void SerializeObject(string filename)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(List<Mesa>));
            List<Mesa> i = mesas;

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
            /*
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "UIElement File"; // Default file name
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            dlg.ShowDialog();

            SerializeObject(dlg.FileName);
            */

        }


        private void LoadButton_2_Click(object sender, RoutedEventArgs e)
        {/*
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.DefaultExt = ".xaml"; // Default file extension
            ofd.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension

            ofd.ShowDialog();

            DeserializeObject(ofd.FileName);
            */
        }

        private void clienteItem_Click(object sender, RoutedEventArgs e)
        {
            Mesa mesa = mesas.Find(x => x.ImageName == rightClicked.Name);
            ClienteWindow clienteWindow = new ClienteWindow();

            clienteWindow.ShowDialog();
            string text = null;
            text = clienteWindow.clBox;
            if (text != null)
            {
                mesa.Cliente = text;
                this.ShowMessageAsync("Cliente agregado con exito!", "");
            }
        }



        private void MozoItem_Click(object sender, RoutedEventArgs e)
        {
            if (mozos.Any())
            {
                Mesa mesa = mesas.Find(x => x.ImageName == rightClicked.Name);
                Mozo moz = null;
                selectMozoWindow window3 = new selectMozoWindow(mozos);
                window3.ShowDialog();
                moz = window3.Moz;
                if (moz != null)
                {
                    mesa.mozito = moz;

                }
                this.ShowMessageAsync("Mozo asignado con exito!", "");
            }
            else
            {
                this.ShowMessageAsync("Agregue un mozo primero!", "");
            }



        }

        private void ReservacionItem_Click(object sender, RoutedEventArgs e)
        {
            Mesa mesa = mesas.Find(x => x.ImageName == rightClicked.Name);
            reservacionWindow reservacion = new reservacionWindow();
            reservacion.ShowDialog();
            DateTime dt = reservacion.picker;
            mesa.Reservacion = dt;
            this.ShowMessageAsync("Reservacion asignada con exito!", "");


        }

        private void MontoItem_Click(object sender, RoutedEventArgs e)
        {
            Mesa mesa = mesas.Find(x => x.ImageName == rightClicked.Name);
            int result = -1;
            montoWindow monto = new montoWindow();
            monto.ShowDialog();
            result = monto.mBox;
            if (result != -1)
            {
                mesa.Consumo = result;
                this.ShowMessageAsync("Monto asignado con exito!", "");
            }


        }

        private void DetailsItem_Click(object sender, RoutedEventArgs e)
        {
            Mesa mesa = mesas.Find(x => x.ImageName == rightClicked.Name);

            this.ShowMessageAsync("Detalles de la mesa: ", mesa.ToString());


        }

        private void AgregarMozo_Click(object sender, RoutedEventArgs e)
        {

            Mozo mozo = new Mozo();
            Random rm = new Random();
            string[] nombres = { "George", "Richard", "Robert", "Bob", "Pancracio", "Rigoberto" };
            mozo.nombre = nombres[rm.Next(0, 5)];
            mozos.Add(mozo);
            this.ShowMessageAsync("Mozo agregado con exito!", "Asignelo con el click derecho sobre una mesa.");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage table = new BitmapImage(new Uri("table.png", UriKind.Relative));
            BitmapImage rtable = new BitmapImage(new Uri("roundtable.png", UriKind.Relative));
            BitmapImage otable = new BitmapImage(new Uri("occupiedTable.png", UriKind.Relative));
            BitmapImage ortable = new BitmapImage(new Uri("occupiedRoundTable.png", UriKind.Relative));


            if (datePicker.SelectedDate != null)
            {
                foreach (var m in mesas)
                {

                    m.update((DateTime)datePicker.SelectedDate);
                }

                foreach (var c in designCanvas.Children)
                {

                    var image = c as Image;
                    if (image.Name.Contains("mesa"))
                    {
                        Mesa mesa = mesas.Find(x => x.ImageName == image.Name);
                        if (image != null)
                        {
                            if (mesa.Estado == "Reservada")
                            {

                                if (mesa.TipoDeMesa == "Cuadrada")
                                {
                                    image.Source = otable;


                                }
                                if (mesa.TipoDeMesa == "Redonda")
                                {
                                    image.Source = ortable;

                                }
                                designCanvas.UpdateLayout();
                            }
                            if (mesa.Estado == "Libre")
                            {

                                if (mesa.TipoDeMesa == "Cuadrada")
                                {

                                    image.Source = table;

                                }
                                if (mesa.TipoDeMesa == "Redonda")
                                {
                                    image.Source = rtable;

                                }

                            }
                        }
                    }
                }
            }
            else
            {
                this.ShowMessageAsync("Seleccione una fecha primero!", "");
            }


        }
    }
}