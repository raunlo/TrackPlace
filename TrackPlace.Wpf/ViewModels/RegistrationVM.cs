using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TrackPlace.Models;
using TrackPlace.Repository;
using TrackPlace.WPF.Commands;

namespace TrackPlace.WPF.ViewModels
{   
    /// <summary>
    /// ViewModel for Registration.xaml
    /// </summary>
    public class RegistrationVM : BaseVM
    {
   
        private string _errorText;
        private UserAccontRepository _accontRepository;
        private string _pw;
        private UserAccont _userAccont;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                NotifyPropertyChanged("ErrorText");
            }
        }
        public string Password
        {
            get { return _pw; }
            set
            {
                _pw = value;
                NotifyPropertyChanged("Password");
            }
        }
        public UserAccont UserAccont => _userAccont;
        public ICommand RegisterCommand { get; set; }
       

        //constructor
        public RegistrationVM()
        {
            _accontRepository = new UserAccontRepository(new TrackPlaceDbContext());
            RegisterCommand = new RelayCommand(ExecuteMethodRegister);
            _userAccont = new UserAccont()
            {
                Password = new Password(),
                Person = new Person()
            };
        }

        /// <summary>
        /// Command for registering user
        /// </summary>
        /// <param name="obj">take sin view object</param>
        private void ExecuteMethodRegister(object obj)
        {
            UserAccont newUser = new UserAccont()
            {
                Password = new Password()
                {
                    PasswordName = UserAccont.Password.PasswordName
                },
                Person = new Person()
                {
                    FirstName = UserAccont.Person.FirstName,
                    LastName = UserAccont.Person.LastName,
                    EMailAddress = UserAccont.Person.EMailAddress
                }
            };
            if (Password != _userAccont.Password.PasswordName)
            {
                ErrorText = "Salsõnad ei ühti";
                return;
            }

            else if (_accontRepository.IfExsists(newUser))
            {
                ErrorText = "Selline kasutaja juba on olemas andmebaasis";
                return;
            }
            else if (!Validation(newUser))
            {
                ErrorText = "kõik väljad peavad olema täidetud!";
                return;
            }
           
                Logger.Logger.log($"Kasutaja loodudˇ{DateTime.Now}", $"{newUser.Person.FirstName}_{newUser.Person.LastName}_log_file");
                _accontRepository.Add(newUser);
                _accontRepository.SaveChanges();
                MessageBox.Show("Olete registeeritud!");                
                Window win = obj as Window;
                win.Close();
            }

        /// <summary>
        /// Validates if all fields are entered
        /// </summary>
        /// <param name="newuser">User what will be adding</param>
        /// <returns></returns>
        private bool Validation( UserAccont newuser)
        {
            if (newuser.Person.FirstName != null && newuser.Person.LastName != null && newuser.Person.EMailAddress != null && Password != null)
            {
                return true;
            }

            return false;
        }
    }
}