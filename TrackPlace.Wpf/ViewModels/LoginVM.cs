using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Navigation;
using TrackPlace.Models;
using TrackPlace.Repository;
using TrackPlace.WPF.Commands;
using TrackPlace.WPF.Helper;
using TrackPlace.WPF.ViewModels.Interface;
using TrackPlace.WPF.Views;


namespace TrackPlace.WPF.ViewModels
{
    public class LoginVM : BaseVM
    {
        private string _errorMSg;
        private string _passwordVM;
        private UserAccontRepository _userAccontRepository;
        private UserAccont _user { get; set; }
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; set; }
        public static UserAccont User { get; set; }
        public string PasswordInVM
        {
            get { return _passwordVM; }
            set
            {
                _passwordVM = value;
                NotifyPropertyChanged("PasswordInVM");
            }
        }
        public string Errormsg
        {
            get { return _errorMSg; }
            set
            {
                _errorMSg = value;
                NotifyPropertyChanged("Errormsg");
            }
        }

        public UserAccont UserAccont => _user;

        public LoginVM()
        {
            _userAccontRepository = new UserAccontRepository(new TrackPlaceDbContext());
            _user = new UserAccont()
            {
                Password = new Password(),
                Person = new Person()
            };
            RegisterCommand = new RelayCommand(ExecuteRegister);
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        #region Login logic
        /// <summary>
        /// https://code.msdn.microsoft.com/windowsdesktop/Get-Password-from-df012a86. Login logic
        /// </summary>
        /// <param name="parameter"> takes in view object</param>
        /// <returns> Returns if user has validated or not(true or flase)</returns>
        public void IsValid(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                PasswordInVM = SecureStringConvertor.ConvertToUnsecureString(secureString);
            }
            int userId = _userAccontRepository.Login(PasswordInVM, _user.Person.EMailAddress);
            if ( userId != 0)
            {
                User = _userAccontRepository.Find(userId); 
                ApplicationController.nav(new ApplicationWindow());
            }
            else
            {
                Errormsg = "Antud parool või kasutajanimi on vale";
            }
        }
        #endregion

        #region LoginCommand
      
        /// <summary>
        /// Execute login button
        /// </summary>
        /// <param name="o"> take sin view object</param>
        private void ExecuteLogin(object o)
        {
            IsValid(o);
        }

        #endregion

        #region [Regster Command]
        private void ExecuteRegister(object o)
        {
            Registration reg = new Registration();
            reg.Show();
        }
        #endregion
    }
}