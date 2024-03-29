﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for montoWindow.xaml
    /// </summary>
    public partial class montoWindow : MetroWindow
    {
        public int mBox;
        public montoWindow()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            int res = -1;
            if (int.TryParse(montoBox.Text,out res))
            {
                if(res != -1)
                {
                    mBox = res;
                    Close();
                }
            }
            else
            {
                this.ShowMessageAsync("Ingrese un valor correcto","");
            }

        }
    }
}
