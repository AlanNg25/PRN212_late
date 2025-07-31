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
        public enum UserRole
        {
            Admin,
            Manager,
        }

        public UserAccount? user { get; set; }
        public int Role { get; set; }

        private bool havePermission = true; // Biến này có thể được sử dụng để kiểm tra quyền truy cập
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userName.Content = user?.Username;
            SetPermission(this.Role);
        }

        private void SetPermission(int role)
        {
            if (role == (int)UserRole.Admin)
            {
                roletxt.Text = "Quản trị viên";
            }
            else if (role == (int)UserRole.Manager)
            {
                roletxt.Text = "Quản lý";
                havePermission = false; // Manager không có quyền chỉnh sửa trong UserManage
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homepage = new HomeWindow();
            homepage.Show();
            this.Close();
        }
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new DashboardView(); // Gọi màn hình Dashboard
        }
        private void btnUserManage_Click(object sender, RoutedEventArgs e)
        {
            // mainContent.Content = new UserManage(havePermission);
            var userManagementWindow = new UserManagement();
            userManagementWindow.Show();
        }
        private void btnBlogManage_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new BlogManageView(havePermission);
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            // Xóa session
            Application.Current.Properties["UserLog"] = null;

            // Mở lại màn hình Login
            var homepage = new HomeWindow();
            homepage.Show();

            // Đóng cửa sổ hiện tại (ví dụ Dashboard, MainWindow, etc.)
            this.Close();
        }
    }
}
