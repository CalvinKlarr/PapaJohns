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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Point mouseClick;
        private Image draggedImage;
        public Window1()
        {
            InitializeComponent();
            toolBox.MouseDoubleClick += ToolBox_MouseDoubleClick;


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
                    designSpace.Children.Add(table);
                    
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
    }
}
