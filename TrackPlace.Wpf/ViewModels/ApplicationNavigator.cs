using System;
using System.Windows.Input;
using TrackPlace.WPF.Commands;
using TrackPlace.WPF.Helper;
using TrackPlace.WPF.Views;

namespace TrackPlace.WPF.ViewModels
{
    /// <summary>
    /// Class for navigation usercontrols inside Main Application window
    /// </summary>
    public class ApplicationNavigator : BaseVM
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICommand RegTruckCommand { get; private set; }
        public ICommand RegOrderCommand { get; private set; }
        public ICommand OrdersCommand { get; private set; }
        public ICommand LogOutCommand { get; private set; }
        private object _selectedViewModel;

        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                NotifyPropertyChanged("SelectedViewModel");
            }
        }

        public ApplicationNavigator()
        {
            SelectedViewModel = new OrdersVM();
            RegTruckCommand = new RelayCommand(ExecuteRegTruck);
            OrdersCommand = new RelayCommand(ExecuteOrders);
            RegOrderCommand = new RelayCommand(ExecuteRegOrder);
            LogOutCommand = new RelayCommand(ExecuteLogOut);
            Firstname = LoginVM.User.Person.FirstName;
            Lastname = LoginVM.User.Person.LastName;
        }

        /// <summary>
        /// Go to Addorder.xaml
        /// </summary>
        /// <param name="o"> takes in viwe object</param>
        private void ExecuteRegOrder(Object o)
        {
            SelectedViewModel = new AddOrderVM();
        }

        /// <summary>
        /// Go to  AddTruck.xaml
        /// </summary>
        /// <param name="o"> takes in view object</param>
        private void ExecuteRegTruck(Object o)
        {
            SelectedViewModel = new AddTrucksVM();
        }

        /// <summary>
        /// Displays all orders.
        /// </summary>
        /// <param name="o">
        /// takes in view object
        /// </param>
        private void ExecuteOrders(Object o)
        {
            SelectedViewModel = new OrdersVM();
        }

        /// <summary>
        /// Go to Login.xaml
        /// </summary>
        /// <param name="o"> Takes in in view object</param>
        private void ExecuteLogOut(Object o)
        {
            ApplicationController.nav(new Login());
        }
    }
}