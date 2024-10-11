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
    /// Логика взаимодействия для DemandPage.xaml
    /// </summary>
    public partial class DemandPage : Page
    {
        public RieltorDipEntities _db = new RieltorDipEntities();
        private List<Demands> _demands = new List<Demands>();
        private Worker _worker;
        public DemandPage(Worker worker)
        {
            InitializeComponent();
            _worker = worker;

            this._demands = _db.Demands.OrderByDescending(t => t.Id).ToList();
            ListDemand.ItemsSource = _demands;
        }

        private void BtnAddDemand_Click(object sender, RoutedEventArgs e)
        {
            AddDemandWindow addDemandWindow = new AddDemandWindow(_worker);
            addDemandWindow.Show();

            

        }

        private void ListDemand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnDemandInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoDemandWindow infoDem = new InfoDemandWindow(_db, sender, this);
            infoDem.ShowDialog();
        }

        public void RefreshDemand()
        {
            _demands = _db.Demands.OrderByDescending(t => t.Id).ToList();
            ListDemand.ItemsSource = _demands;
        }
    }
}
