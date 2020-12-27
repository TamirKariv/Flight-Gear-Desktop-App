using FlightSimulator.ViewModels;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models {
    //Model of FlightBoard
    class FlightBoardModel : BaseNotify {
        public event PropertyChangedEventHandler PropertyChanged;
        private Server dataReciver;
        public FlightBoardModel(Server dataReciver) {
            this.dataReciver = dataReciver;
        }
        private double lon;
        public double Lon {
            get
            {
                return lon;
            }
            set
            {
                lon = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Lon"));

                }
            }
        }
        private double lat;
        public double Lat {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Lat"));

                }
            }
        }

        //get the Lon and Lat values from the server via the reciver
        public void GetServerData(string ip, int port) {
            dataReciver.OpenServer(ip, port);
            Thread serverRead= new Thread(() =>
            {
            while (!dataReciver.Terminate)
            {

                    string[] values = dataReciver.ReciveData();
                    Lon = Convert.ToDouble(values[0]);
                    Lat = Convert.ToDouble(values[1]);
            }
            });
            serverRead.Start();
        }
        //check if already connected
        public bool AlreadyConnected() {
            return dataReciver.IsConnected; }
        //terminate the reciver
        public void TerminateReciver()
        {
            this.dataReciver.Terminate = true;
        }
    }
}
