using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RieltorAgency
{
    
    public partial class RieltorWindow : Window
    {
        private Worker _worker;
        public RieltorWindow(Worker worker)
        {
            InitializeComponent();
            _worker = worker;
            SaleFrame.Navigate(new SalePage(worker));
            RentFrame.Navigate(new RentPage(worker));
            ClientFrame.Navigate(new ClientsPage());
            DemandFrame.Navigate(new DemandPage(worker));
            DealFrame.Navigate(new DealPage()); 
        }


        private void BtnSale_Click(object sender, RoutedEventArgs e)
        {
            SaleFrame.Visibility = Visibility.Visible;
            RentFrame.Visibility = Visibility.Hidden;
            ClientFrame.Visibility = Visibility.Hidden;
            DemandFrame.Visibility = Visibility.Hidden;
            DealFrame.Visibility = Visibility.Hidden;
        }

        private void BtnRent_Click(object sender, RoutedEventArgs e)
        {
            RentFrame.Visibility = Visibility.Visible;
            SaleFrame.Visibility = Visibility.Hidden;
            ClientFrame.Visibility = Visibility.Hidden;
            DemandFrame.Visibility = Visibility.Hidden;
            DealFrame.Visibility = Visibility.Hidden;
        }

        private void BtnDemand_Click(object sender, RoutedEventArgs e)
        {
            ClientFrame.Visibility = Visibility.Hidden;
            RentFrame.Visibility = Visibility.Hidden;
            SaleFrame.Visibility = Visibility.Hidden;
            DemandFrame.Visibility = Visibility.Visible;
            DealFrame.Visibility = Visibility.Hidden;
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            DemandFrame.Visibility = Visibility.Hidden;
            ClientFrame.Visibility = Visibility.Visible;
            RentFrame.Visibility = Visibility.Hidden;
            SaleFrame.Visibility = Visibility.Hidden;
            DealFrame.Visibility = Visibility.Hidden;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();

            }
        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Height = 720;
                    this.Width = 1080;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
                { }
            }
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            TxtName.Text = _worker.Name;
            TxtSurName.Text = _worker.Surname;
            TxtCountOfSale.Text = Convert.ToString(_worker.Count_Of_Sale);
            TxtSallary.Text = _worker.Sallary.ToString();
        }

        private void BtnDeal_Click(object sender, RoutedEventArgs e)
        {
            DealFrame.Visibility = Visibility.Visible;
            DemandFrame.Visibility = Visibility.Hidden;
            ClientFrame.Visibility = Visibility.Hidden;
            RentFrame.Visibility = Visibility.Hidden;
            SaleFrame.Visibility = Visibility.Hidden;
            
        }
    }
}
