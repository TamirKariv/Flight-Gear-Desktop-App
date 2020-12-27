using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace FlightSimulator.Models
{
    //Model for the Controls - AutoPilot and Manual
    class ControlsModel
    {
        //send the controls command to the server
        public void ControlsCommandSend(string command)
        {
            if (Client.Instance.IsRuninning)
            {
                Thread commandSender = new Thread(() => Client.Instance.SendData(command));
                commandSender.Start();
            }
        }

    }
}
