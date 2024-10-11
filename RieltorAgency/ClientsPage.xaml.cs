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
   
    public partial class ClientsPage : Page
    {
        public RieltorDipEntities _db = new RieltorDipEntities();
        private Client _client;
        //int idClient;
        public ClientsPage()
        {
            InitializeComponent();
            //_client = _db.Client.ToList();
            DataGridClients.ItemsSource = _db.Client.ToList();
        }

        private void BtnApart_Click(object sender, RoutedEventArgs e)
        {
            _client = (sender as Button).DataContext as Client;
           //idClient = Convert.ToInt32(_client.Id);

            _db.Client.Remove(_client);
            _db.SaveChanges();

            DataGridClients.ItemsSource = _db.Client.ToList();
        }
    }
}
