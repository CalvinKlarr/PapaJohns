﻿using MahApps.Metro.Controls;
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
        Dictionary<Image, Mesa> mesas;
        public Window2(Canvas design, Dictionary<Image,Mesa> dict)
        {
            
            InitializeComponent();
            this.designSpace = design;
            mesas = dict;
        }
    }
}