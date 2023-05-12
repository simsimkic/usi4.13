using HealthCare.Model;
using System;

namespace HealthCare.Service
{
    public class TransferService : NumericService<TransferItem>
    {
        private readonly Inventory _inventory;

        public TransferService(string filepath, Inventory inventory) : base(filepath)
        {
            _inventory = inventory;
        }

        public void Execute(TransferItem transfer) {
            var reduceItem = new InventoryItem(
                transfer.EquipmentId, transfer.FromRoom, transfer.Quantity);
            var restockItem = new InventoryItem(
                transfer.EquipmentId, transfer.ToRoom, transfer.Quantity);

            if (!_inventory.TryReduceInventoryItem(reduceItem))
                return;

            _inventory.RestockInventoryItem(restockItem);
            transfer.Executed = true;

            if (transfer.Id == 0) 
                Add(transfer);
            else 
                Update(transfer);
        }

        public void ExecuteAll()
        {
            GetAll().ForEach(x => {
                if (!x.Executed && x.Scheduled <= DateTime.Now)
                    Execute(x);
            });
        }
    }
}
