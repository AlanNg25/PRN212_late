using BLL.Service;
using DAL.Entities;
using DAL.Repo;
using SchoolHealthWPF.NursePages;
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
        private readonly int _parentId;

        public ParentWindow(int parentId)
        {
            InitializeComponent();
            _parentId = parentId;
        }
        private void btnHealthRecord_Click(object sender, RoutedEventArgs e)
        {
            var studentService = new StudentService();

            var students = studentService.GetStudentsByParentId(_parentId);

            if (students == null || !students.Any())
            {
                MessageBox.Show("Không có học sinh nào được liên kết với tài khoản này.");
                return;
            }

            var healthRecordWindow = new HealthRecordWindow(students);
            healthRecordWindow.ShowDialog();
        }


        private void btnSendMedicine_Click(object sender, RoutedEventArgs e)
        {
            var studentService = new StudentService();


            var students = studentService.GetStudentsByParentId(_parentId);

            if (students == null || !students.Any())
            {
                MessageBox.Show("Không có học sinh nào được liên kết với tài khoản này.");
                return;
            }

            // Truyền danh sách vào SendMedicineWindow
            var sendMedicineWindow = new SendMedicineWindow(students);
            sendMedicineWindow.ShowDialog();
        }
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var confirmWindow = new HealthCheckFormConfirmWindow(_parentId);
            confirmWindow.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var homeWindow = new HomeWindow();
            homeWindow.Show();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewHealthHistory_Click(object sender, RoutedEventArgs e)
        {
            // Open the health history window for the parent
            var historyWindow = new ParentHealthCheckHistoryWindow(_parentId);
            historyWindow.Show();
        }
    }

}
