﻿using FlightSimulator.ViewModels.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

// the view code of the the manul xaml.
namespace FlightSimulator.Views {
    public partial class Manual : UserControl {
        public Manual() {
            InitializeComponent();
            DataContext = new ManualViewModel();
        }
    }
}
