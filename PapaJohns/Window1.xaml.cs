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
        private Dictionary<Image, Mesa> mesas;
        private Mesa mesa;
        
        public Window1()
        {
            InitializeComponent();
            toolBox.MouseDoubleClick += ToolBox_MouseDoubleClick;
            mesas = new Dictionary<Image, Mesa>();
            backgroundChoice.SelectedItem = pisoDos;
            sizeChoice.SelectedItem = sizeOne;


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
                    Canvas.SetLeft(table, 0);
                    Canvas.SetTop(table, 0);
                    mesa = new Mesa();
                    mesas.Add(table, mesa);
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
                    Canvas.SetLeft(rtable, 0);
                    Canvas.SetTop(rtable, 0);
                    mesa = new Mesa();
                    mesas.Add(rtable, mesa);
                    designSpace.Children.Add(rtable);

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
        //En este evento se realiza el guardado, las cosas a guardar son el Canvas, nombre designSpace y el diccionario mesas
        //idealmente hay que guardarlos en un mismo archivo.
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            string json = JsonConvert.SerializeObject(mesas);
            //string json2 = JsonConvert.SerializeObject(designSpace);

            string[] savefile = new string[] { json };

            SaveFileDialog sfd = new SaveFileDialog();


            sfd.FileName = "untitled";
            sfd.Filter = "Json Files(*.json) | *.json | Text Files(*.txt) | *.txt | All Files(*.*) | *.*  ";
            //   sfd.DefaultExt = "json";

            sfd.ShowDialog();


            File.WriteAllLines(sfd.FileName, savefile);

            //   Nullable<bool> result = sfd.ShowDialog();

            MainWindow mainWindow = new MainWindow();

            //if (result == true)
            //{
            SerializeToXML(mainWindow, designSpace, 96, sfd.FileName);
            //}





        }

        public static void SerializeToXML(MainWindow window, Canvas canvas, int dpi, string filename)
        {
            string mystrXAML = XamlWriter.Save(canvas);
            FileStream filestream = File.Create(filename);
            StreamWriter streamwriter = new StreamWriter(filestream);
            streamwriter.Write(mystrXAML);
            streamwriter.Close();
            filestream.Close();
        }

        private void DesignSpace_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var image = e.Source as Image;

            designSpace.Children.Remove(image);

        }
    }



}

