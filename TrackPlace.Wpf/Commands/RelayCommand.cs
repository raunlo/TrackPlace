using System;
using System.Windows.Input;

namespace TrackPlace.WPF.Commands
{
    /// <summary>
    /// Command implementation. Source: https://stackoverflow.com/questions/1468791/wpf-icommand-mvvm-implementation
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute", "Execute cannot be null.");
            }

            _Execute = execute;
            _CanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }

        /// <summary>
        /// Can Execute for ICommand
        /// </summary>
        /// <param name="parameter" > takes in object</param>
        /// <returns>-CanExecute</returns>
        public bool CanExecute(object parameter)
        {
            if (_CanExecute == null)
            {
                return true;
            }

            return _CanExecute(parameter);
        }
    }
}