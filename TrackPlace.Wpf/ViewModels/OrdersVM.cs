using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TrackPlace.Models;
using TrackPlace.Repository;
using TrackPlace.WPF.Commands;
using TrackPlace.WPF.Views;

namespace TrackPlace.WPF.ViewModels
{
    public class OrdersVM : BaseVM
    {
        public static TruckInOrder CurrentOrder { get; set; }       
        public ICommand ChangeOrderCommand { get; private set; }
        private ObservableCollection<TruckInOrder> _orders;
        private TruckInOrderRepository _truckInOrderRepository;
        public ObservableCollection<TruckInOrder> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                NotifyPropertyChanged("Orders");
            }
        }

        /// <summary>
        /// Constructer where all Properties are inistalized.
        /// </summary>
        public OrdersVM()
        {            
            ChangeOrderCommand = new RelayCommand(ExecuteViewOrder);
            _truckInOrderRepository = new TruckInOrderRepository(new TrackPlaceDbContext());
            Load();
        }
        /// <summary>
        /// Load all orders to ObservableColection
        /// </summary>
        private void Load()
        {
            _orders = new ObservableCollection<TruckInOrder>(
                _truckInOrderRepository.GetAllTruckInOrders(LoginVM.User.UserAccontId));
        }

        #region [Change Order]
        /// <summary>
        /// Command for opeining order invoice
        /// </summary>
        /// <param name="obj"> takes in view object</param>
        public void ExecuteViewOrder(Object obj)
        {
            Recepie_window rec = new Recepie_window();
            rec.Show();
        }
        #endregion
    }
}