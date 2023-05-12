using HealthCare.Command;
using HealthCare.Context;
using System.Collections.Generic;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands
{
    public class EndEquipmentQuantityEditingCommand : CommandBase
    {
        private readonly UsedDynamicEquipmentViewModel _viewModel;
        private readonly Window _window;
        private readonly Hospital _hospital;

        public EndEquipmentQuantityEditingCommand(Hospital hospital, Window window, UsedDynamicEquipmentViewModel viewModel) 
        {
            _viewModel = viewModel;
            _hospital = hospital;
            _window = window;
        }

        public override void Execute(object parameter)
        {
            _window.Close();
            Dictionary<int, int> newQuantities = new Dictionary<int, int>();
            foreach(EquipmentViewModel equipment in _viewModel.UsedDynamicEquipment)
            {
                newQuantities.Add(equipment.InventoryId, equipment.CurrentQuantity);
            }
            _hospital.Inventory.ChangeDynamicEquipmentQuantity(newQuantities);
        }
    }
}
