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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RieltorAgency
{
   
    public partial class SalePage : Page
    {
        public RieltorDipEntities _db = new RieltorDipEntities();
        private List<Apartament> _apartament = new List<Apartament>();
        private List<Apartament> _apartFlat = new List<Apartament>();
        private List<Apartament> _apartHouse = new List<Apartament>();
        private List<Apartament> _apartEarth = new List<Apartament>();
        
        private Worker _worker;
        
        public SalePage(Worker worker)
        {
            InitializeComponent();

            

            _worker = worker;
            this._apartament = _db.Apartament.OrderByDescending(t => t.Id).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            this._apartFlat = _db.Apartament.OrderByDescending(t => t.Id).Where(t => t.Id_Type == 1).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            this._apartHouse = _db.Apartament.OrderByDescending(t => t.Id).Where(t => t.Id_Type == 2).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            this._apartEarth = _db.Apartament.OrderByDescending(t => t.Id).Where(t => t.Id_Type == 3).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            ListAparts.ItemsSource = _apartament;
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            MunuClnm.Width = new GridLength(250);
            filterBtnClose.Visibility = Visibility.Visible;
            filterBtn.Visibility = Visibility.Hidden;
        }

        public void RefreshApart()
        {
            _apartament = _db.Apartament.OrderByDescending(t => t.Id).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            ListAparts.ItemsSource = _apartament;
        }

        private void ListAparts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void filterBtnClose_Click(object sender, RoutedEventArgs e)
        {
            MunuClnm.Width = new GridLength(0);
            filterBtn.Visibility = Visibility.Visible;
            filterBtnClose.Visibility = Visibility.Hidden;
        }

        private void BtnAddApart_Click(object sender, RoutedEventArgs e)
        {
            AddApartWindow addApartWindow = new AddApartWindow(_worker);
            addApartWindow.Show();
        }


        bool flagFlat, flagHouse, flagEarth;

        private void BtnFilterFlat_Click(object sender, RoutedEventArgs e)
        {
            flagFlat = true;
            flagHouse = false;
            flagEarth = false;
            _apartament = _apartFlat;
            ListAparts.ItemsSource = _apartament;

            runNameOfObject.Text = "Квартиры";
            MunuClnm.Width = new GridLength(0);
            filterBtn.Visibility = Visibility.Visible;
            filterBtnClose.Visibility = Visibility.Hidden;
        }

        
        private void BtnFilterHouse_Click(object sender, RoutedEventArgs e)
        {
            flagFlat = false;
            flagHouse = true;
            flagEarth = false;
            _apartament = _apartHouse;
            ListAparts.ItemsSource = _apartament;

            runNameOfObject.Text = "Дома";
            MunuClnm.Width = new GridLength(0);
            filterBtn.Visibility = Visibility.Visible;
            filterBtnClose.Visibility = Visibility.Hidden;
        }

       
        private void BntFilterEarth_Click(object sender, RoutedEventArgs e)
        {
            flagFlat = false;
            flagHouse = false;
            flagEarth = true;
            _apartEarth = _apartFlat.OrderByDescending(t => t.Id).Where(t => t.Id_Type == 3).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            _apartament = _apartEarth;
            ListAparts.ItemsSource = _apartEarth;

            runNameOfObject.Text = "Земля";
            MunuClnm.Width = new GridLength(0);
            filterBtn.Visibility = Visibility.Visible;
            filterBtnClose.Visibility = Visibility.Hidden;
        }


        int countSold = 1;
        private void BtnFilterSold_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BntFilterAll_Click(object sender, RoutedEventArgs e)
        {
            flagFlat = false;
            flagHouse = false;
            flagEarth = false;

            _apartament = _db.Apartament.OrderByDescending(t => t.Id).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            ListAparts.ItemsSource = _apartament;

            runNameOfObject.Text = "Объекты";
            MunuClnm.Width = new GridLength(0);
            filterBtn.Visibility = Visibility.Visible;
            filterBtnClose.Visibility = Visibility.Hidden;
        }


        
        private void ChBSold_Checked(object sender, RoutedEventArgs e)
        {
            if (ChBSold.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Sold == true).Where(p => p.Id_Type_Of_Operation == 1).ToList();
                ListAparts.ItemsSource = _apartament;
                
                runNameOfObject.Text = "Проданные объекты";
                MunuClnm.Width = new GridLength(0);
                filterBtn.Visibility = Visibility.Hidden;
                filterBtnClose.Visibility = Visibility.Hidden;
                

                BtnFilterFlat.Visibility = Visibility.Hidden;
                BtnFilterHouse.Visibility = Visibility.Hidden;
                BtnFilterEarth.Visibility = Visibility.Hidden;
                BntFilterAll.Visibility = Visibility.Hidden;
            }
            
        }

        private void BtnObjectInfo_Click(object sender, RoutedEventArgs e)
        {
            ObjectInfoWindow info = new ObjectInfoWindow(_db, sender, this);
            info.ShowDialog();
        }

        private void ChBSold_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ChBSold.IsChecked == false)
            {
                _apartament = _db.Apartament.OrderByDescending(t => t.Id).Where(p => p.Id_Type_Of_Operation == 1).ToList();
                ListAparts.ItemsSource = _apartament;
                runNameOfObject.Text = "Oбъекты";
                MunuClnm.Width = new GridLength(0);
                filterBtn.Visibility = Visibility.Visible;
                filterBtnClose.Visibility = Visibility.Hidden;
               

                BtnFilterFlat.Visibility = Visibility.Visible;
                BtnFilterHouse.Visibility = Visibility.Visible;
                BtnFilterEarth.Visibility = Visibility.Visible;
                BntFilterAll.Visibility = Visibility.Visible;

                
            }

        }

        //Кнопка показать результаты фильтра
        private void showFilter_Click(object sender, RoutedEventArgs e)
        {
            
                if (flagFlat == true && flagHouse == false && flagEarth == false)
                    _apartament = _apartFlat.OrderByDescending(t => t.Id).Where(t => t.Id_Type == 1).Where(p => p.Id_Type_Of_Operation == 1).ToList();
                else if (flagFlat == false && flagHouse == true && flagEarth == false)
                    _apartament = _apartHouse.OrderByDescending(t => t.Id).Where(t => t.Id_Type == 2).Where(p => p.Id_Type_Of_Operation == 1).ToList();
                else if (flagFlat == false && flagHouse == false && flagEarth == true)
                    _apartament = _apartEarth.OrderByDescending(t => t.Id).Where(t => t.Id_Type == 3).Where(p => p.Id_Type_Of_Operation == 1).ToList();
                else if (flagFlat == false && flagHouse == false && flagEarth == false)
                    _apartament = _db.Apartament.OrderByDescending(t => t.Id).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            
            if (ChB1.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Rooms == 1).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            }
            else if (ChB2.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Rooms == 2).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            }
            else if (ChB3.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Rooms == 3).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            }
            else if (ChB4.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Rooms == 4).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            }
            else if (ChB5.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Rooms == 5).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            }
            

            if (txtBoxMaxPrice.Text != "")
            {

                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Price <= Convert.ToInt32(txtBoxMaxPrice.Text)).Where(p => p.Id_Type_Of_Operation == 1).ToList();
                
            }
            if (txtBoxMinPrice.Text != "")
            {

                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Price >= Convert.ToInt32(txtBoxMinPrice.Text)).Where(p => p.Id_Type_Of_Operation == 1).ToList();

            }
            if (txtMinArea.Text != "")
            {

                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Area >= Convert.ToInt32(txtMinArea.Text)).Where(p => p.Id_Type_Of_Operation == 1).ToList();

            }
            if (txtMaxArea.Text != "")
            {

                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Area <= Convert.ToInt32(txtMaxArea.Text)).Where(p => p.Id_Type_Of_Operation == 1).ToList();

            }
            if (txtMinFloor.Text != "")
            {

                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Floor >= Convert.ToInt32(txtMinFloor.Text)).Where(p => p.Id_Type_Of_Operation == 1).ToList();

            }
            if (txtMaxFloor.Text != "")
            {

                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Floor <= Convert.ToInt32(txtMaxFloor.Text)).Where(p => p.Id_Type_Of_Operation == 1).ToList();

            }
            if (tgbfirst.IsChecked == true)
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.High_Of_Roof < 3 && t.High_Of_Roof >= 2.4).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            else if (tgbsec.IsChecked == true)
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.High_Of_Roof < 3.5 && t.High_Of_Roof >= 3).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            else if (tgbthird.IsChecked == true)
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.High_Of_Roof >= 3.5).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            
            if (tgbRepair.IsChecked == true)
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Repair == true).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            else if (tgbNonRepair.IsChecked == true)
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Repair == false).Where(p => p.Id_Type_Of_Operation == 1).ToList();

            if (tgbTogether.IsChecked == true)
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Id_Type_Of_Toilet == 1).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            else if (tgbApart.IsChecked == true)
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Id_Type_Of_Toilet == 2).Where(p => p.Id_Type_Of_Operation == 1).ToList();

            if (txtMinCountFloor.Text != "")
            {

                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Floor_In_House >= Convert.ToInt32(txtMinCountFloor.Text)).Where(p => p.Id_Type_Of_Operation == 1).ToList();

            }
            if (txtMaxCountFloor.Text != "")
            {

                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Floor_In_House <= Convert.ToInt32(txtMaxCountFloor.Text)).Where(p => p.Id_Type_Of_Operation == 1).ToList();

            }

            if (tgbLiftY.IsChecked == true)
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Lift == true).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            else if (tgbLiftN.IsChecked == true)
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Lift == false).Where(p => p.Id_Type_Of_Operation == 1).ToList();


            if (ChBpark1.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Id_Type_Of_Parking == 1).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            }
            else if (ChB2park.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Id_Type_Of_Parking == 2).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            }
            else if (ChB3park.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Id_Type_Of_Parking == 3).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            }
            else if (ChB4park.IsChecked == true)
            {
                _apartament = _apartament.OrderByDescending(t => t.Id).Where(t => t.Id_Type_Of_Parking == 4).Where(p => p.Id_Type_Of_Operation == 1).ToList();
            }

            ListAparts.ItemsSource = _apartament;
        
        }
        
    }
}
