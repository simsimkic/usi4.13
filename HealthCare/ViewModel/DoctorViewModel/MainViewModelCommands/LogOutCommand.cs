using HealthCare.Command;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    public class LogOutCommand : CommandBase
    {
        private readonly Window _window;
        public LogOutCommand(Window window)
        {
            _window = window;
        }

        public override void Execute(object parameter)
        {
            _window.Close();
        }
    }
}
