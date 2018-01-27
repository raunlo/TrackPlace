using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TrackPlace.Models;
using TrackPlace.Repository;
using TrackPlace.WPF.Commands;
using TrackPlace.WPF.Helper;

namespace TrackPlace.WPF.ViewModels
{
    /// <summary>
    /// ViewModel class for login usercontrol
    /// </summary>
    public class AddOrderVM : BaseVM
    {
        //Command for saving order
        public ICommand SaveOrderCommand { get; set; }

        #region Fields

        private string _errormsg;
        private TruckInOrderRepository _truckInOrderRepository;
        private TruckInOrder _truckInOrder;
        private TruckRepository _truck;
        private Truck truckDriver { get; set; }
        private Order _order;

        #endregion

        #region [properties]

        public string Errormsg
        {
            get => _errormsg;
            set
            {
                _errormsg = value;
                NotifyPropertyChanged("Errormsg");
            }
        }

        public TruckInOrder TruckInOrder => _truckInOrder;

        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                NotifyPropertyChanged("Order");
            }
        }

        #endregion

        //Constructor
        public AddOrderVM()
        {
            _truck = new TruckRepository(new TrackPlaceDbContext());
            _truckInOrderRepository = new TruckInOrderRepository(new TrackPlaceDbContext());
            _truckInOrder = new TruckInOrder();
            _order = new Order();
            SaveOrderCommand = new RelayCommand(ExecuteSave);
        }

        #region [Save Order]

        /// <summary>
        /// This is logic all about saving order. Gets on object from view with command parameter
        /// </summary>
        /// <param name="o"> takes view object in</param>
        private void ExecuteSave(object o)
        {
            Errormsg = "";
            DateTime StartDate = (DateTime) Order.LoadingDateTime;
            if (StartDate.Hour > 17 && StartDate.Hour < 9)
            {
                Errormsg = "Peale võtmis ekellaeg peab olema väikse. Kellaeg peab olema 9st 5ni";
                return;
            }

            if (StartDate < DateTime.Now)
            {
                Errormsg = "Kuupäev ja aeg ei saa olla minevikus!";
                return;
            }

            //Calculates Product cubbage
            if (_order.ProductCubage == 0)
            {
                _order.ProductCubage = _order.ProductHeight * _order.ProductLength * _order.ProductWidth;
            }

            _order.ProductCubage = _order.ProductHeight * _order.ProductLength * _order.ProductWidth;
            // Calculates time to get from one place to another
            int time = Geocoder.calcTime(
                _order.LoadingCounty + _order.LoadingCity + _order.LoadingAddress + _order.LoadingHouseNumber,
                _order.UnloadingCounty + _order.UnloadingCity + _order.UnloadingAddress + _order.UnloadingHouseNumber);
            if (time == 0)
            {
                Errormsg = "Antud sihtekohta või lähtekohta ei leitud";
                return;
            }

            //Adding references
            _truckInOrder.Order = _order;
            _truckInOrder.TruckId = _truckInOrderRepository.IsFree(_truckInOrder);
            if (!_truckInOrderRepository.AreDatesCorrect(_truckInOrder))
            {
                Errormsg = "Need ajad ei sobi!";
                return;
            }

            //finds driver
            _truckInOrder.Order.UserAccontId = LoginVM.User.UserAccontId;
            truckDriver = _truck.Find(_truckInOrder.TruckId);

            if (_truckInOrder.TruckId == 0)
            {
                Errormsg = "Ei leia teie soovile sõidukit";
                return;
            }

            if (!validation())
            {
                Errormsg = "Kõik väljad peavad olema täidetud";
                return;
            }

            DateTime result = (DateTime) _order.LoadingDateTime;
            result = result.AddHours(time / 3600 + 1);
            _order.UnloadingDateTime = result;
            //Adding to database
            _truckInOrder.OrderId = _order.OrderId;
            _truckInOrder.Order = _order;
            _truckInOrder.TruckId = _truckInOrderRepository.IsFree(_truckInOrder);
            truckDriver = _truck.Find(_truckInOrder.TruckId);
            _truckInOrderRepository.Add(_truckInOrder);
            _truckInOrderRepository.SaveChanges();
            //Logging
            Logger.Logger.log(
                $"Tellimus esitati {DateTime.Now}. Autojuhiks on {truckDriver.UserAccont.Person.FirstName} {truckDriver.UserAccont.Person.LastName}. Veoki numbrimärk on: {truckDriver.RegistrationNumber}," +
                $"kauba nimetus on {Order.ProductName}, peale võtmise aeg on {Order.LoadingDateTime} ja asukohaks on: {Order.LoadingCounty}, {Order.LoadingCity}, {Order.LoadingAddress}, {Order.LoadingHouseNumber} ja" +
                $"kauba maha laadimsie aeg on {Order.UnloadingDateTime} ja asukohaks on: {Order.UnloadingCounty}, {Order.UnloadingCity}, {Order.UnloadingAddress}, {Order.UnloadingHouseNumber}"
                ,
                $"{LoginVM.User.Person.FirstName}_{LoginVM.User.Person.LastName}_log_file");
            MessageBox.Show("Teie Tellimus on lisatud!");
        }

        /// <summary>
        /// Validates if all needed data in present
        /// </summary>
        /// <returns></returns>
        public bool validation()
        {
            if (_truckInOrder.Order.LoadingAddress != null && _truckInOrder.Order.LoadingAddress != null &&
                _truckInOrder.Order.UnloadingAddress != null
                && _truckInOrder.Order.LoadingCounty != null && _truckInOrder.Order.LoadingDateTime != null &&
                _truckInOrder.Order.ProductName != null)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}