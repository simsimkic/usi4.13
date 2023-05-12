using HealthCare.Context;
using System;
using System.ComponentModel;
using System.Windows;

namespace HealthCare.View.ManagerView
{
    public partial class ManagerMenu : Window
    {
        private MainWindow _loginWindow;
        private Hospital _hospital;

        public ManagerMenu(MainWindow loginWindow, Hospital hospital)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
            _hospital = hospital;
        }

        private void Button_Equipment(object sender, EventArgs e)
        {
            new InventoryListingView(this, _hospital).Show();
        }
        private void Button_Ordering(object sender, RoutedEventArgs e)
        {
            new EquipmentOrderView(this, _hospital).Show();
        }

        private void Button_Rearranging(object sender, RoutedEventArgs e)
        {
            new EquipmentRearrangingView(this, _hospital).Show();
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            Close();
            _loginWindow.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _loginWindow.Show();
        }
    }
}
