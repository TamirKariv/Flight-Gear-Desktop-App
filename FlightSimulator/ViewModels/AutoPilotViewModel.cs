using FlightSimulator.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace FlightSimulator.ViewModels
{
    //ViewModel for the AutoPilot
    class AutoPilotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ControlsModel autoPilotModel;


        public AutoPilotViewModel()
        {
            autoPilotModel = new ControlsModel();
        }

        //the color of the autopilot's textbox
        private Brush color;
        public Brush Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Color"));
                }
            }

        }
        //the command given to the autopilot
        private string autoPilotCommand;
        public string AutoPilotCommand
        {

            set
            {
                autoPilotCommand = value;

                //if no command is given the color is set to white, otherwise red.
                if (string.IsNullOrEmpty(AutoPilotCommand))
                {
                    Color = Brushes.White;
                }

                else if (!(string.IsNullOrEmpty(AutoPilotCommand) && Color != Brushes.White))
                {
                    Color = Brushes.LightPink;
                }
            }
            get
            {
                return autoPilotCommand;
            }
        }


        //clear command 
        private ICommand commandClear;
        public ICommand CommandClear
        {
            get
            {
                //create a new command and set color to white
                if (commandClear == null)
                {
                    CommandHandler ch = new CommandHandler(() =>
                    {
                        Color = Brushes.White;
                        AutoPilotCommand = "";
                        if (PropertyChanged != null)
                        {
                            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(AutoPilotCommand));
                        }
                    });
                    commandClear = ch;
                    return commandClear;
                }
                else
                {
                    return commandClear;
                }

            }
        }
        //ok command
        private ICommand commandOK;

        public ICommand CommandOK
        {
            get
            {
                //create a new command and send the given command
                if (commandOK == null)
                {

                    CommandHandler ch = new CommandHandler(() =>
                    {
                        string command = AutoPilotCommand;
                        AutoPilotCommand = "";
                        if (PropertyChanged != null)
                        {
                            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(AutoPilotCommand));
                        }
                        Color = Brushes.White;
                        autoPilotModel.ControlsCommandSend(command);
                    });
                    commandOK = ch;
                    return commandOK;
                }
                else
                {
                    return commandOK;
                }

            }
        }









    }
}
