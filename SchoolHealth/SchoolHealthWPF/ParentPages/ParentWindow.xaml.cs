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

namespace SchoolHealthWPF.ParentPages
{
    /// <summary>
    /// Interaction logic for ParentWindow.xaml
    /// </summary>
    public partial class ParentWindow : Window
    {
        public ParentWindow()
        {
            InitializeComponent();
        }

        private void btnHealthRecord_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ hồ sơ sức khỏe
            var healthRecordWindow = new HealthRecordWindow();
            healthRecordWindow.ShowDialog();
        }

        private void btnSendMedicine_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Mở cửa sổ gửi thuốc (cần tạo window tương ứng)
            MessageBox.Show("Tính năng gửi thuốc đang phát triển.");
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Thực hiện xác nhận (có thể mở window hoặc xử lý logic)
            MessageBox.Show("Tính năng xác nhận đang phát triển.");
        }
    }
}
