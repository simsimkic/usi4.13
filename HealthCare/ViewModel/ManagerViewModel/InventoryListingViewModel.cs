using HealthCare.Context;
using HealthCare.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class InventoryListingViewModel : ViewModelBase
    {
        private readonly Inventory _inventory;
        private readonly EquipmentService _equipmentService;
        private readonly RoomService _roomService;
        public ObservableCollection<InventoryItemViewModel> Items { get; }
        private List<InventoryItemViewModel> _models;

        public InventoryListingViewModel(Hospital hospital)
        {
            _inventory = hospital.Inventory;
            _equipmentService = hospital.EquipmentService;
            _roomService = hospital.RoomService;
            Items = new ObservableCollection<InventoryItemViewModel>();

            _models = GetModels();
            LoadAll();
        }

        public void Filter()
        {
            InventoryFilter filter = new InventoryFilter(_models);
            filter.FilterQuantity(TgNone, TgLittle, TgLot);
            filter.FilterEquipmentType(TgExaminationalEq, TgOperationalEq, TgFurnitureEq, TgHallwayEq);
            filter.FilterRoomType(TgExaminationalRm, TgOperationalRm, TgPatientCareRm, TgReceptionRm, TgWarehouseRm);
            filter.FilterAnyProperty(TbQuery);
            LoadModels(filter.GetFiltered());
        }

        public void LoadAll()
        {
            InitializeButtons();
            LoadModels(_models);
        }

        private void LoadModels(List<InventoryItemViewModel> items)
        {
            Items.Clear();
            items.ForEach(item => Items.Add(item));
        }

        private List<InventoryItemViewModel> GetModels()
        {
            var models = new List<InventoryItemViewModel>();
            _inventory.GetAll().ForEach(x => {
                var equipment = _equipmentService.Get(x.EquipmentId);
                var room = _roomService.Get(x.RoomId);
                models.Add(new InventoryItemViewModel(x, equipment, room));
            });
            return models;
        }

        private void InitializeButtons()
        {
            TgExaminationalRm = false;
            TgOperationalRm = false;
            TgPatientCareRm = false;
            TgReceptionRm = false;
            TgWarehouseRm = false;
            
            TgExaminationalEq = false;
            TgOperationalEq = false;
            TgFurnitureEq = false;
            TgHallwayEq = false;
            
            TgNone = false;
            TgLittle = false;
            TgLot = false;
            
            TbQuery = "";
        }

        private bool _tgNone, _tgLittle, _tgLot;
        private bool _tgExaminationalEq, _tgOperationalEq, _tgFurnitureEq, _tgHallwayEq;
        private bool _tgExaminationalRm, _tgOperationalRm, _tgPatientCareRm, _tgReceptionRm, _tgWarehouseRm;
        private string _tbQuery = "";

        public bool TgNone
        {
            get => _tgNone;
            set { _tgNone = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgLittle
        {
            get => _tgLittle;
            set { _tgLittle = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgLot
        {
            get => _tgLot;
            set { _tgLot = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgExaminationalEq
        {
            get => _tgExaminationalEq;
            set { _tgExaminationalEq = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgOperationalEq
        {
            get => _tgOperationalEq;
            set { _tgOperationalEq = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgFurnitureEq
        {
            get => _tgFurnitureEq;
            set { _tgFurnitureEq = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgHallwayEq
        {
            get => _tgHallwayEq;
            set { _tgHallwayEq = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgExaminationalRm
        {
            get => _tgExaminationalRm;
            set { _tgExaminationalRm = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgOperationalRm
        {
            get => _tgOperationalRm;
            set { _tgOperationalRm = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgPatientCareRm
        {
            get => _tgPatientCareRm;
            set { _tgPatientCareRm = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgReceptionRm
        {
            get => _tgReceptionRm;
            set { _tgReceptionRm = value; OnPropertyChanged(); Filter(); }
        }
        public bool TgWarehouseRm
        {
            get => _tgWarehouseRm;
            set { _tgWarehouseRm = value; OnPropertyChanged(); Filter(); }
        }
        public string TbQuery
        {
            get => _tbQuery;
            set { _tbQuery = value; OnPropertyChanged(); Filter(); }
        }
    }
}
