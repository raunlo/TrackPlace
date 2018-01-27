using System;
using System.Windows;
using System.Windows.Input;
using TrackPlace.Models;
using TrackPlace.Repository;
using TrackPlace.WPF.Commands;

namespace TrackPlace.WPF.ViewModels
{
    /// <summary>
    /// ViewModel for adding trucks
    /// </summary>
    public class AddTrucksVM : BaseVM
    {      
        private string _errorMsg;

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
                NotifyPropertyChanged("ErrorMsg");
            }
        }

        private TruckRepository _truckRepository;

        /// <summary>
        /// Saves the Truck information
        /// </summary>
        public ICommand SaveCommand { get; set; }

        private Truck _truck;
        public Truck Truck => _truck;

        public AddTrucksVM()
        {
            _truckRepository = new TruckRepository(new TrackPlaceDbContext());
            SaveCommand = new RelayCommand(ExecuteMethod);
            _truck = new Truck();
        }

        /// <summary>
        /// Execute method for adding truck
        /// </summary>
        /// <param name="obj">
        /// Takes in view object
        /// </param>
        private void ExecuteMethod(Object obj)
        {
            ErrorMsg = " ";
            if (!_truckRepository.IfExsists(LoginVM.User.UserAccontId))
            {    
                //Adding detail to object and then adding it to database
                _truck.TrailerCubage = _truck.TrailerHeight * _truck.TrailerLength * _truck.TrailerWidth;
                _truck.UserAccontId = LoginVM.User.UserAccontId;
                _truckRepository.Add(_truck);
                _truckRepository.SaveChanges();
                MessageBox.Show("Teie veok lisati andmebaasi");
                //logger
                Logger.Logger.log($"{DateTime.Now} lisati {Truck.Make}, {Truck.Model}, {Truck.BodyType}, {Truck.TypeOfTrucks} anmebaasi. Veoki parameetrid on: {Truck.TrailerHeight} X {Truck.TrailerLength} X {Truck.TrailerWidth}", $"{LoginVM.User.Person.FirstName}_{LoginVM.User.Person.LastName}_log_file");
            }
            else
            {
                ErrorMsg = "Antud kasutajal on juba veok registeeritud";
            }
        }
    }
}