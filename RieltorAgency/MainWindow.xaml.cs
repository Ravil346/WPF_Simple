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
  
    public partial class MainWindow : Window
    {
        public static RieltorDipEntities db = new RieltorDipEntities();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var user = db.Worker.FirstOrDefault(q => q.Login == LoginTxt.Text && q.Password == PasswordTxt.Password.ToString());
            if (user != null)
            {

                RieltorWindow worker = new RieltorWindow(user);
                worker.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неудача");
                
            }
        }

        private void WinClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WinCollapse_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();

            }
        }
    }
}
