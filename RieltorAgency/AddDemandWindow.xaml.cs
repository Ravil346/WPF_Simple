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
    /// Логика взаимодействия для AddDemandWindow.xaml
    /// </summary>
    public partial class AddDemandWindow : Window
    {
        public RieltorDipEntities _db = new RieltorDipEntities();
        Client client;
        private Worker _worker;
        public AddDemandWindow(Worker worker)
        {
            InitializeComponent();
            _worker = worker;
        }

        private void CmbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City city = CmbCity.SelectedItem as City;
            //CmbDistrict.ItemsSource = _db.Districts.Where(d => d.City.Id == city.Id).ToList();
            //CmbMetro.ItemsSource = _db.Metro.Where(c => c.City.Id == city.Id).ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CmbCity.ItemsSource = _db.City.ToList();
            CmbTypeOFApart.ItemsSource = _db.Type_Of_House.ToList();
            CmbTypeOfOperation.ItemsSource = _db.Type_Of_Operation.ToList();
        }

        private void BtnMoveToClient_Click(object sender, RoutedEventArgs e)
        {
            InfoClient.Visibility = Visibility.Hidden;
            DemandScroll.Visibility = Visibility.Visible;
            LblInfo.Text = "Введите информацию о запросе";
            client = new Client()
            {
                Surname = TxtSurname.Text,
                Name = TxtName.Text,
                SecondName = TxtSecondName.Text,
                Password = Passport.Text,
                Phone = TxtPhone.Text,
                Email = TxtEmail.Text
            };
            _db.Client.Add(client);
            _db.SaveChanges();
        }

        private void AddFinalDemand_Click(object sender, RoutedEventArgs e)
        {
            Demands demands = new Demands()
            {
                City = CmbCity.SelectedItem as City,
                
                MinPrise = Convert.ToInt32(TxtMinPrice.Text),
                MaxPrise = Convert.ToInt32(TxtMaxPrice.Text),
                MinArea = Convert.ToInt32(TxtMinArea.Text),
                MaxArea = Convert.ToInt32(TxtMaxArea.Text),
                MinRooms = Convert.ToInt32(TxtMinRooms.Text),
                MaxRooms = Convert.ToInt32(TxtMaxRooms.Text),
                MinFloor = Convert.ToInt32(TxtMinFloor.Text),
                MaxFloor = Convert.ToInt32(TxtMaxFloor.Text),
                Type_Of_House = CmbTypeOFApart.SelectedItem as Type_Of_House,
                Type_Of_Operation = CmbTypeOfOperation.SelectedItem as Type_Of_Operation,



                
                Id_Client = client.Id,
                Id_Rieltor = _worker.Id,
                
                

            };
            _db.Demands.Add(demands);
            _db.SaveChanges();

            int count = 0;
            foreach (var i in _db.Demands)
            {
                count++;
            }

           

            _db.SaveChanges();
        

            if (demands != null)
                MessageBox.Show("Запрос добавлены");
            else
                MessageBox.Show("Ti loh");
        }

        private void CmbTypeOFApart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbTypeOFApart.SelectedIndex == 1 || CmbTypeOFApart.SelectedIndex == 2)
            {
                Floor.Visibility = Visibility.Hidden;
                MinFloor.Visibility = Visibility.Hidden;
                MaxFloor.Visibility = Visibility.Hidden;
            }
            else if (CmbTypeOFApart.SelectedIndex == 0)
            {
                Floor.Visibility = Visibility.Visible;
                MinFloor.Visibility = Visibility.Visible;
                MaxFloor.Visibility = Visibility.Visible;
            }
        }
    }
}
