using System.Windows;

namespace HealthCare.Command
{
    public class CancelCommand : CommandBase
    {
        private Window _window;
        public CancelCommand(Window window)
        {
            _window = window;
        }

        public override void Execute(object parameter)
        {
            _window.Close();
        }
    }
}
