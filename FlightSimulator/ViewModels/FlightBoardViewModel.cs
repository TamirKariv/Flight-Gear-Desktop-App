using FlightSimulator.Models;
using FlightSimulator.Views.Windows;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    //ViewModel of the FlightBoard
    public class FlightBoardViewModel : BaseNotify
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private FlightBoardModel flightBoardModel = new FlightBoardModel(new Server());
        private Settings settings = new Settings();

        //update Lon and Lat values from the model
        public FlightBoardViewModel()
        {
            flightBoardModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("Lon"))
                {
                    Lon = flightBoardModel.Lon;
                }
                if (e.PropertyName.Equals("Lat"))
                {
                    Lat = flightBoardModel.Lat;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
                }
            };
        }

        //open the settings on click
        void ClickSettings()
        {
            if (settings == null || !settings.IsLoaded)
            {
                settings = new Settings();
                settings.Show();
            }
        }

        //settings command
        private ICommand commandSettings; 
        public ICommand CommandSettings
        {

            get
            {
                if (commandSettings == null)
                {
                    CommandHandler ch = new CommandHandler(() => { ClickSettings();});
                    commandSettings = ch;
                    return commandSettings;

                }
                else
                {
                    return commandSettings;
                }
            }
        }
  

        //initlize the connection when clicking the connect buttom
        void IntilizeConnection()
        {
            Thread connectionThread = new Thread(() => Client.Instance.ConnectToServer(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort));
            connectionThread.Start();
            flightBoardModel.GetServerData(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);


        }

        //connection command
        private ICommand connection; 
        public ICommand Connection
        {
            get
            {
                if (connection == null)
                {
                    CommandHandler ch = new CommandHandler(() => { IntilizeConnection(); });
                    connection = ch;
                    return connection;

                }
                else
                {
                    return connection;
                }
            }
        }
     

    }
}
