using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using FlightSimulator.Models;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    
    public partial class FlightBoard : UserControl
    {
        ObservableDataSource<Point> planeLocations = null;
        FlightBoardViewModel flightboardVM;

        public FlightBoard()
        {
          InitializeComponent();
          flightboardVM = new FlightBoardViewModel();
          DataContext = flightboardVM;
          flightboardVM.PropertyChanged += Vm_PropertyChanged;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           planeLocations = new ObservableDataSource<Point>();
           planeLocations.SetXYMapping(p => p);
           plotter.AddLineGraph(planeLocations, 2, "Route");

        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon") ) {
                planeLocations.AppendAsync(Dispatcher, new Point(flightboardVM.Lat, flightboardVM.Lon));
            }
        }
    }

}

