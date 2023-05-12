using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.ViewModel.ManagerViewModel;
using System;
using System.Windows;

namespace HealthCare.View.ManagerView
{
    public partial class EquipmentRearrangingView : Window
    {
        private Window _parent;
        private Hospital _hospital;
        private RearrangingViewModel _model;
        private Equipment _selected;

        public EquipmentRearrangingView(Window parent, Hospital hospital)
        {
            InitializeComponent();
            _hospital = hospital;
            _parent = parent;
            _parent.IsEnabled = false;
            _selected = new Equipment();

            _model = new RearrangingViewModel(_hospital);
            DataContext = _model;
        }

        private void cb_SelectionChanged(object sender, EventArgs e)
        {
            var selected = cbEquipment.SelectedItem as Equipment;
            if (selected is null) return;

            _selected = selected;
            if (_selected.IsDynamic)
                datePicker.IsEnabled = false;
            else datePicker.IsEnabled = true;
            _model.Load(_selected);
        }

        private void Button_Transfer(object sender, RoutedEventArgs e) {
            try {
                Validate();
            } catch (ValidationException ve) {
                Utility.ShowWarning(ve.Message);
                return;
            }

            CreateTransfer();
            Utility.ShowInformation("Uspešna operacija.");
            tbQuantity.Text = "";
            datePicker.SelectedDate = null;
            _model.Load(_selected);
        }

        private void CreateTransfer()
        {
            var equipment = ((Equipment)cbEquipment.SelectedItem).Id;
            var quantity = int.Parse(tbQuantity.Text.Trim());
            var date = datePicker.SelectedDate;
            var from = ((InventoryItemViewModel)lvFromRoom.SelectedItem).Room.Id;
            var to = ((InventoryItemViewModel)lvToRoom.SelectedItem).Room.Id;

            var transfer = new TransferItem(equipment, quantity, DateTime.Now, false, from, to);
            if (date is null) {
                _hospital.TransferService.Execute(transfer);
                return;
            }

            transfer.Scheduled = (DateTime)date;
            _hospital.TransferService.Add(transfer);
        }

        private void Validate()
        {
            int quantity;
            var selected = cbEquipment.SelectedItem as Equipment;
            if (selected is null)
                throw new ValidationException("Izaberite opremu za prenos.");
            else if (!(int.TryParse(tbQuantity.Text.Trim(), out quantity) && quantity > 0))
                throw new ValidationException("Količina opreme za prenos mora da bude pozitivan broj.");

            var from = (InventoryItemViewModel)lvFromRoom.SelectedItem;
            var to = (InventoryItemViewModel)lvToRoom.SelectedItem;
            if (from is null || to is null)
                throw new ValidationException("Izaberite sobe za prenos iz obe tabele.");
            else if (from.Room.Id == to.Room.Id)
                throw new ValidationException("Prenos opreme iz sobe u nju samu nije moguć.");
            else if (from.Quantity < quantity)
                throw new ValidationException("Nema dovoljno opreme da bi se izvršio prenos.");

            var date = datePicker.SelectedDate;
            if (!selected.IsDynamic && date is null)
                throw new ValidationException("Pošto oprema nije dinamička obavezno je izabrati datum prenosa.");
            else if (!selected.IsDynamic && date <= DateTime.Now)
                throw new ValidationException("Datum prenosa ne sme da bude u prošlosti.");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _parent.IsEnabled = true;
            Close();
        }
    }
}
