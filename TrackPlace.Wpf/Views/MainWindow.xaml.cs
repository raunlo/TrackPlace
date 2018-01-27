using System.Windows;
using TrackPlace.WPF.Helper;
using TrackPlace.WPF.ViewModels;
using TrackPlace.WPF.Views;


namespace TrackPlace.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   //initing page switcher, and mapping windows size to be same as usercontrol's
            InitializeComponent();
            this.Content = new Login();
            ApplicationController.switcher = this;
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}
