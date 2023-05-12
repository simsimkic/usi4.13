using HealthCare.Context;
using HealthCare.ViewModel.ManagerViewModel;
using System;
using System.Windows;

namespace HealthCare.View.ManagerView
{
    public partial class InventoryListingView : Window
    {
        private Window _parent;
        private InventoryListingViewModel _model;

        public InventoryListingView(Window parent, Hospital hospital)
        {
            InitializeComponent();
            _parent = parent;
            _parent.IsEnabled = false;

            _model = new InventoryListingViewModel(hospital);
            DataContext = _model;
        }

        private void Button_Reset(object sender, RoutedEventArgs e)
        {
            _model.LoadAll();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _parent.IsEnabled = true;
            Close();
        }
    }
}
