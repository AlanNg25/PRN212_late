using BLL.Service;
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
using System.Windows.Shapes;

namespace SchoolHealthWPF
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private readonly BlogService _blogService;
        public HomeWindow()
        {
            InitializeComponent();
            _blogService = new BlogService();
        }

        private void CheckLoggedIn()
        {
            var user = Application.Current.Properties["UserLog"] as UserAccount;
            if (user == null)
            {
                btnLogin.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Collapsed;
            }else
            {
                btnLogin.Visibility = Visibility.Collapsed;
                btnLogout.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckLoggedIn();
            LoadItemControll();
        }

        private void LoadItemControll()
        {
            var list1 = _blogService.GetAllBlogs().Where(bl => bl.Type == 1);
            var list2 = _blogService.GetAllBlogs().Where(bl => bl.Type == 2);

            itemCntrolBlog.ItemsSource = list1;
            itemCntrolBlogMedical.ItemsSource = list2;
        }

        // Header Button
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homepage = new HomeWindow();
            homepage.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWIndow loginWIndow = new LoginWIndow();
            loginWIndow.Show();
            this.Close();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            // Xóa session
            Application.Current.Properties["UserLog"] = null;

            // Mở lại màn hình Login
            var homepage= new HomeWindow();
            homepage.Show();

            // Đóng cửa sổ hiện tại (ví dụ Dashboard, MainWindow, etc.)
            this.Close();
        }
    }
}
