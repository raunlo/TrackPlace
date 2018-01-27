using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using TrackPlace.Models;
using TrackPlace.Repository;
using TrackPlace.WPF.Commands;
using TrackPlace.WPF.Helper;
using PrintDialog = System.Windows.Controls.PrintDialog;


namespace TrackPlace.WPF.ViewModels
{
    /// <summary>
    /// Class for makeing order invoice. Can save it as .pdf and print it
    /// </summary>
    public class InvoiceVM : BaseVM
    {
        private int _price;
        private BillRepository _billRepository;

        public int price
        {
            get => _price;
            set
            {
                _price = value;
                NotifyPropertyChanged("price");
            }
        }

        public ICommand PrintCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        private TruckInOrder _printInOrder;

        public TruckInOrder PrintOrder
        {
            get => _printInOrder;
            set
            {
                _printInOrder = value;
                NotifyPropertyChanged("PrintOrder");
            }
        }

        //construktor
        public InvoiceVM()
        {
            PrintOrder = OrdersVM.CurrentOrder;
            PrintCommand = new RelayCommand(ExecutePrint);
            SaveCommand = new RelayCommand(ExecuteSave);
            _billRepository = new BillRepository(new TrackPlaceDbContext());
            price = Geocoder.calcDis(
                        OrdersVM.CurrentOrder.Order.UnloadingCounty + OrdersVM.CurrentOrder.Order.UnloadingCity +
                        OrdersVM.CurrentOrder.Order.UnloadingAddress + OrdersVM.CurrentOrder.Order.UnloadingHouseNumber,
                        OrdersVM.CurrentOrder.Order.LoadingCounty + OrdersVM.CurrentOrder.Order.LoadingCity +
                        OrdersVM.CurrentOrder.Order.LoadingAddress + OrdersVM.CurrentOrder.Order.LoadingHouseNumber) /
                    1000;

            Bill bill = new Bill()
            {
                Order = OrdersVM.CurrentOrder.Order,
                BillNumber = OrdersVM.CurrentOrder.OrderId,
                BillDateTime = OrdersVM.CurrentOrder.Order.UnloadingDateTime,
                UserAccont = LoginVM.User,
                Price = price
            };
        }

        /// <summary>
        /// http://www.c-sharpcorner.com/blogs/printing-wpf-window-to-printer-and-fit-on-a-page1
        /// Execute logic for printing a order
        /// </summary>
        /// <param name="o"></param>
        private void ExecutePrint(Object o)
        {
            //grid what will be printed
            Grid grid = o as Grid;
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                //get selected printer capabilities
                System.Printing.PrintCapabilities capabilities =
                    printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);
                //get scale of the print wrt to screen of WPF visual
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / grid.ActualWidth,
                    capabilities.PageImageableArea.ExtentHeight / grid.ActualHeight);
                //Transform the Visual to scale
                grid.LayoutTransform = new ScaleTransform(scale, scale);
                //get the size of the printer page
                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth,
                    capabilities.PageImageableArea.ExtentHeight);
                //update the layout of the visual to the printer page size.
                grid.Measure(sz);

                grid.Arrange(new Rect(
                    new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight),
                    sz));
                //now print the visual to printer to fit on the one page.
                printDlg.PrintVisual(grid, "First Fit to Page WPF Print");
            }
        }

        /// <summary>
        /// Execute for saveing order to pdf
        /// </summary>
        /// <param name="o"></param>
        private void ExecuteSave(Object o)
        {
            //grid what will be saved
            Grid grid = o as Grid;
            // filename
            string dummyFileName =
                $"{PrintOrder.Order.UserAccont.Person.FirstName}_{PrintOrder.Order.UserAccont.Person.LastName}_{PrintOrder.OrderId}_Arve";

            SaveFileDialog sf = new SaveFileDialog();
            // Feed the dummy name to the save dialog
            sf.FileName = dummyFileName;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                // Now here's our save folder
                string savePath = Path.GetDirectoryName(sf.FileName) + "\\" +
                                  $"{PrintOrder.Order.UserAccont.Person.FirstName}_{PrintOrder.Order.UserAccont.Person.LastName}_{PrintOrder.OrderId}_Arve" +
                                  ".pdf";
                MemoryStream mem = new MemoryStream();
                {
                    Package package = Package.Open(mem, FileMode.Create);
                    XpsDocument doc = new XpsDocument(package);
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                    writer.Write(grid);
                    doc.Close();
                    package.Close();
                }
                var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(mem);
                PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, savePath, 0);
            }
        }
    }
}