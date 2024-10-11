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
    
    public partial class ObjectInfoWindow : Window
    {
        public RieltorDipEntities _db = new RieltorDipEntities();
        private Apartament _apartament;
        public City city;
        private SalePage _salePage;
        public ObjectInfoWindow(RieltorDipEntities db, object o, SalePage salePage)
        {
            InitializeComponent();

            _apartament = (o as Button).DataContext as Apartament;
            _db = db;
            _salePage = salePage;


            TxtType_Of_House.Text = _apartament.Type_Of_House.Name;


            CmbCity.SelectedIndex = _apartament.Id_City - 1;

            CmbDistrict.SelectedIndex = Convert.ToInt32(_apartament.Id_District) -1;

            CmbMetro.SelectedIndex = Convert.ToInt32(_apartament.Id_Metro)-1;

            CmbTypeOfRooms.SelectedIndex = Convert.ToInt32(_apartament.Id_Type_Of_Rooms) - 1;

            CmbTypeOfToilet.SelectedIndex = Convert.ToInt32(_apartament.Id_Type_Of_Toilet) - 1;

            CmbTypeOfParking.SelectedIndex = Convert.ToInt32(_apartament.Id_Type_Of_Parking) - 1;

            TxtStreet.Text = _apartament.Street;
            TxtHouse.Text = _apartament.House;
            TxtFlat.Text = _apartament.Flat.ToString();
            TxtArea.Text = _apartament.Area.ToString();
            TxtRooms.Text = _apartament.Rooms.ToString();
            TxtFloor.Text = _apartament.Floor.ToString();

            TxtPrice.Text = _apartament.Price.ToString();
            
            if (_apartament.Repair == true)
            {
                CmbRepair.SelectedItem = RepairY;
            }
            else if(_apartament.Repair == false)
            {
                CmbRepair.SelectedItem = RepairN;
            }


            TxtHighOfRoof.Text = _apartament.High_Of_Roof.ToString();

            TxtFloorInHouse.Text = _apartament.Floor_In_House.ToString();

            if (_apartament.Lift)
                CmbLiftYorN.SelectedItem = LiftY;
            else if (_apartament.Lift == false)
                CmbLiftYorN.SelectedItem = LiftN;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CmbCity.ItemsSource = _db.City.ToList();
            CmbTypeOfRooms.ItemsSource = _db.Type_Of_Rooms.ToList();
            CmbTypeOfParking.ItemsSource = _db.Type_Of_Parking.ToList();
            CmbTypeOfToilet.ItemsSource = _db.Type_Of_Toilet.ToList();
        }

        private void CmbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City city = CmbCity.SelectedItem as City;
            CmbDistrict.ItemsSource = _db.Districts.Where(d => d.City.Id == city.Id).ToList();
            CmbMetro.ItemsSource = _db.Metro.Where(c => c.City.Id == city.Id).ToList();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            _apartament.Street = TxtStreet.Text;
            _apartament.House = TxtHouse.Text;
            _apartament.Flat = Convert.ToInt32(TxtFlat.Text);
            _apartament.Area = Convert.ToInt32(TxtArea.Text);
            _apartament.Rooms = Convert.ToInt32(TxtRooms.Text);
            _apartament.Floor = Convert.ToInt32(TxtFloor.Text);
            _apartament.Price = Convert.ToInt32(TxtPrice.Text);
            _apartament.High_Of_Roof = float.Parse(TxtHighOfRoof.Text);
            _apartament.Floor_In_House = Convert.ToInt32(TxtFloorInHouse.Text);
            //_apartament.Id_City = CmbCity.SelectedIndex;
            //_apartament.Id_District = CmbDistrict.SelectedIndex;
            //_apartament.Id_Metro = CmbMetro.SelectedIndex;
            //_apartament.Id_Type_Of_Rooms = CmbTypeOfRooms.SelectedIndex;
            //_apartament.Id_Type_Of_Toilet = CmbTypeOfToilet.SelectedIndex;
            //_apartament.Id_Type_Of_Parking = CmbTypeOfParking.SelectedIndex;

            //if (CmbLiftYorN.SelectedItem == LiftY)
            //    _apartament.Lift = true;
            //else if (CmbLiftYorN.SelectedItem == LiftN)
            //    _apartament.Lift = false;

            //if (CmbRepair.SelectedItem == RepairY)
            //    _apartament.Repair = true;
            //else if (CmbRepair.SelectedItem == RepairN)
            //    _apartament.Repair = false;

            _db.SaveChanges();
            _salePage.RefreshApart();
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            _db.Apartament.Remove(_apartament);
            
            _db.SaveChanges();
            _salePage.RefreshApart();
            this.Close();
        }

       

        private void CmbTypeOfRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CmbTypeOfToilet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }

        private void CmbTypeOfParking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }
    }
}
