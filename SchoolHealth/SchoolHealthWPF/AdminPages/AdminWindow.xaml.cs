using DAL.Entities;
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

namespace SchoolHealthWPF.AdminPages
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public UserAccount? user { get; set; }
        public int Role { get; set; }
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetPermission(this.Role);
        }

        private void SetPermission(int role)
        {
            if (role == 0)
            {
                Console.Write("");
            }
            else
            {
                Console.Write("al");
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void btnUserManage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
