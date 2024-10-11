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
    /// <summary>
    /// Логика взаимодействия для DealPage.xaml
    /// </summary>
    public partial class DealPage : Page
    {
        public RieltorDipEntities _db = new RieltorDipEntities();
        private Apartament _apartament;
        private Demands _demands;
        int idApart, idDemand;
        public DealPage()
        {
            InitializeComponent();
            DataGridApart.ItemsSource = _db.Apartament.Where(p => p.Sold == false).ToList();
            DataGridDemand.ItemsSource = _db.Demands.ToList();
            ListDemands.ItemsSource = _db.Deal.ToList();
        }

        private void BtnApart_Click(object sender, RoutedEventArgs e)
        {
            object o;
            _apartament = (sender as Button).DataContext as Apartament;
            TextIdApart.Text = _apartament.Id.ToString();
            idApart = _apartament.Id;
        }

        private void BtnDemandFinal_Click(object sender, RoutedEventArgs e)
        {
            Deal deal = new Deal()
            {
                Id_Apartament = idApart,
                Id_Demand = idDemand
            };
            _db.Deal.Add(deal);

            _apartament.Sold = true;
            //_db.Demands.Remove(_demands);

            _db.SaveChanges();
        }

        private void BtnDemand_Click(object sender, RoutedEventArgs e)
        {
            _demands = (sender as Button).DataContext as Demands;
            TxtIdDemand.Text = _demands.Id.ToString();
            idDemand = _demands.Id;
        }
    }
}
