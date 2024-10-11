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
using Microsoft.Win32;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

namespace RieltorAgency
{
    /// <summary>
    /// Логика взаимодействия для AddRentWindow.xaml
    /// </summary>
    public partial class AddRentWindow : Window
    {
        public RieltorDipEntities _db = new RieltorDipEntities();
        Client client;
        private Worker _worker;
        public AddRentWindow(Worker worker)
        {
            InitializeComponent();
            _worker = worker;
        }
        public string url;

        private void BtnMoveToClient_Click(object sender, RoutedEventArgs e)
        {
            InfoClient.Visibility = Visibility.Hidden;
            InfoFlat.Visibility = Visibility.Visible;
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
        byte[] image_byte;

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(); // показываем

            if (openFileDialog.ShowDialog() == true)
            {
                Image image = new Image();
                Uri fileUri = new Uri(openFileDialog.FileName);
                url = fileUri.ToString();
                Img.Source = new BitmapImage(fileUri);
                image_byte = File.ReadAllBytes(openFileDialog.FileName);


                Img.Visibility = Visibility.Visible;
                AddImage.Visibility = Visibility.Hidden;
            }
        }

        private void CmbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City city = CmbCity.SelectedItem as City;
            CmbDistrict.ItemsSource = _db.Districts.Where(d => d.City.Id == city.Id).ToList();
            CmbMetro.ItemsSource = _db.Metro.Where(c => c.City.Id == city.Id).ToList();
        }

        private void AddFinalApart_Click(object sender, RoutedEventArgs e)
        {
            Apartament apartament = new Apartament()
            {
                City = CmbCity.SelectedItem as City,
                Districts = CmbDistrict.SelectedItem as Districts,
                Metro = CmbMetro.SelectedItem as Metro,
                Street = TxtStreet.Text,
                House = TxtHouse.Text,
                Flat = Convert.ToInt32(TxtFlat.Text),
                Area = Convert.ToInt32(TxtArea.Text),
                Rooms = Convert.ToInt32(TxtCountOfRooms.Text),
                Floor = Convert.ToInt32(TxtFloor.Text),
                Type_Of_House = CmbTypeOFApart.SelectedItem as Type_Of_House,
                Price = Convert.ToInt32(TxtPrice.Text),
                Repair = CmbRepair.SelectedIndex == 0,
                Type_Of_Rooms = CmbTypeOfRooms.SelectedItem as Type_Of_Rooms,
                High_Of_Roof = float.Parse(TxtHighOfRoof.Text),
                Type_Of_Toilet = CmbTypeOfToilet.SelectedItem as Type_Of_Toilet,
                Floor_In_House = Convert.ToInt32(TxtFloorInHouse.Text),
                Lift = CmbLiftYorN.SelectedIndex == 0,
                Type_Of_Parking = CmbTypeOfParking.SelectedItem as Type_Of_Parking,
                Id_Client = client.Id,
                Id_Rieltor = _worker.Id,
                Type_Of_Operation = _db.Type_Of_Operation.First(t => t.Name == "Аренда"),
                Sold = false
            };
            _db.Apartament.Add(apartament);
            _db.SaveChanges();

            int count = 0;
            foreach (var i in _db.Apartament)
            {
                count++;
            }

            //Image image = new Image()
            //{
            //    Image1 = image_byte,
            //    Id_Apartament = count
            //};
            //_db.Image.Add(image);

            _db.SaveChanges();
            //_salePage.RefreshApart();

            if (apartament != null)
                MessageBox.Show("Апартаменты добавлены");
            else
                MessageBox.Show("Апартаменты не добавлены");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CmbCity.ItemsSource = _db.City.ToList();
            CmbTypeOFApart.ItemsSource = _db.Type_Of_House.ToList();
            CmbTypeOfParking.ItemsSource = _db.Type_Of_Parking.ToList();
            CmbTypeOfRooms.ItemsSource = _db.Type_Of_Rooms.ToList();
            CmbTypeOfToilet.ItemsSource = _db.Type_Of_Toilet.ToList();
        }
    }
}
