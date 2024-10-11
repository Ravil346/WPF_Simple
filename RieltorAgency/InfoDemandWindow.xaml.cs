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
    /// <summary>
    /// Логика взаимодействия для InfoDemandWindow.xaml
    /// </summary>
    public partial class InfoDemandWindow : Window
    {
        public RieltorDipEntities _db = new RieltorDipEntities();
        private Demands _demands;
        private DemandPage _demandPage;
        public InfoDemandWindow(RieltorDipEntities db, object o, DemandPage demandPage)
        {
            InitializeComponent();
            _demands = (o as Button).DataContext as Demands;
            _db = db;
            _demandPage = demandPage;

            CmbCity.SelectedIndex = _demands.Id_City - 1;
            CmbTypeOfOperation.SelectedIndex = Convert.ToInt32(_demands.Id_Type_Of_Operation) - 1;
            CmbTypeOFApart.SelectedIndex = Convert.ToInt32(_demands.Id_Type_Of_House) - 1;

            TxtMaxArea.Text = _demands.MaxArea.ToString();
            TxtMinArea.Text = _demands.MinArea.ToString();
            TxtMinPrice.Text = _demands.MinPrise.ToString();
            TxtMaxPrice.Text = _demands.MaxPrise.ToString();
            TxtMinRooms.Text = _demands.MinRooms.ToString();
            TxtMaxRooms.Text = _demands.MaxRooms.ToString();
            TxtMaxFloor.Text = _demands.MaxFloor.ToString();
            TxtMinFloor.Text = _demands.MinFloor.ToString();



            if (CmbTypeOFApart.SelectedIndex == 1 || CmbTypeOFApart.SelectedIndex == 2)
            {
                Floor.Visibility = Visibility.Collapsed;
                MinFloor.Visibility = Visibility.Collapsed;
                MaxFloor.Visibility = Visibility.Collapsed;
                
            }
            else if (CmbTypeOFApart.SelectedIndex == 0)
            {
                Floor.Visibility = Visibility.Visible;
                MinFloor.Visibility = Visibility.Visible;
                MaxFloor.Visibility = Visibility.Visible;
            }

            if (CmbTypeOFApart.SelectedIndex == 2)
            {
                Rooms.Visibility = Visibility.Collapsed;
                MinRooms.Visibility = Visibility.Collapsed;
                MaxRooms.Visibility = Visibility.Collapsed;
            }
            else if (CmbTypeOFApart.SelectedIndex == 0)
            {
                Rooms.Visibility = Visibility.Visible;
                MinRooms.Visibility = Visibility.Visible;
                MaxRooms.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CmbCity.ItemsSource = _db.City.ToList();
            CmbTypeOFApart.ItemsSource = _db.Type_Of_House.ToList();
            CmbTypeOfOperation.ItemsSource = _db.Type_Of_Operation.ToList();
        }

        private void UpdateDemand_Click(object sender, RoutedEventArgs e)
        {
            _demands.MinPrise = Convert.ToInt32(TxtMinPrice.Text);
            _demands.MaxPrise = Convert.ToInt32(TxtMaxPrice.Text);

            _demands.MinArea = Convert.ToInt32(TxtMinArea.Text);
            _demands.MaxArea = Convert.ToInt32(TxtMaxArea.Text);

            _demands.MinRooms = Convert.ToInt32(TxtMinRooms.Text);
            _demands.MaxRooms = Convert.ToInt32(TxtMaxRooms.Text);

            _demands.MinFloor = Convert.ToInt32(TxtMinFloor.Text);
            _demands.MaxFloor = Convert.ToInt32(TxtMaxFloor.Text);

            //_demands.Id_City = CmbCity.SelectedIndex;
            //_demands.Id_Type_Of_House = CmbTypeOFApart.SelectedIndex;
            //_demands.Id_Type_Of_Operation = CmbTypeOfOperation.SelectedIndex;
            //_demandPage.RefreshDemand();
            _db.SaveChanges();
            
            this.Close();
        }

        private void DeleteDemand_Click(object sender, RoutedEventArgs e)
        {
            _db.Demands.Remove(_demands);

            _db.SaveChanges();
            _demandPage.RefreshDemand();
            this.Close();
        }

        

        private void CmbTypeOFApart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbTypeOFApart.SelectedIndex == 1 || CmbTypeOFApart.SelectedIndex == 2)
            {
                Floor.Visibility = Visibility.Collapsed;
                MinFloor.Visibility = Visibility.Collapsed;
                MaxFloor.Visibility = Visibility.Collapsed;

            }
            else if (CmbTypeOFApart.SelectedIndex == 0)
            {
                Floor.Visibility = Visibility.Visible;
                MinFloor.Visibility = Visibility.Visible;
                MaxFloor.Visibility = Visibility.Visible;
            }

            if (CmbTypeOFApart.SelectedIndex == 2)
            {
                Rooms.Visibility = Visibility.Collapsed;
                MinRooms.Visibility = Visibility.Collapsed;
                MaxRooms.Visibility = Visibility.Collapsed;
            }
            else if (CmbTypeOFApart.SelectedIndex == 0)
            {
                Rooms.Visibility = Visibility.Visible;
                MinRooms.Visibility = Visibility.Visible;
                MaxRooms.Visibility = Visibility.Visible;
            }
        }
    }
}
