﻿using MahApps.Metro.Controls;
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
    /// Interaction logic for reservacionWindow.xaml
    /// </summary>
    public partial class reservacionWindow : MetroWindow
    {
        public DateTime picker;
        public reservacionWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            picker = (DateTime)reservacionPicker.SelectedDate;
            Close();
        }
    }
}