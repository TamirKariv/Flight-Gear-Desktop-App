using FlightSimulator.Models;
using System;

namespace FlightSimulator.ViewModels.Windows {

    //ViewModel of ManualConrols
    class ManualViewModel {
        ControlsModel manuelModel; 

        public ManualViewModel()
        {
            manuelModel = new ControlsModel();
        }

        //set the values of the elevator,rudder,aileron and the throttle

        public double Elevator
        {
            set
            {
                string setElevator = "set /controls/flight/elevator ";
                manuelModel.ControlsCommandSend(setElevator + Convert.ToString(value)); ;
            }
        }


        public double Rudder
        {
            set
            {
                string setRudder = "set /controls/flight/rudder ";
                manuelModel.ControlsCommandSend(setRudder + Convert.ToString(value));
            }
        }


        public double Aileron
        {
            set
            {
                string setAielron = "set /controls/flight/aileron ";
                manuelModel.ControlsCommandSend(setAielron + Convert.ToString(value));
            }
        }
       
        public double Throttle
        {
            set
            {
                string setThrottle = "set /controls/engines/current-engine/throttle ";
                manuelModel.ControlsCommandSend(setThrottle + Convert.ToString(value));
            }
        }
    }
}
