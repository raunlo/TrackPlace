using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TrackPlace.WPF.Helper
{
    /// <summary>
    /// ApplicationController. Contains method to switch from login.xaml to applicationwindows.xaml and reverse.
    /// </summary>
    public static class ApplicationController
    {
        /// <summary>
        /// Main wndows reference
        /// </summary>
       public static MainWindow switcher { get; set; }

        /// <summary>
        /// Method to navigate between usercontrols
        /// </summary>
        /// <param name="userControl"></param>
        public static void nav(UserControl userControl)
        {        
            switcher.Content = userControl;
        }        
    }
}
