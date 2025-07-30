using BLL.Service;
using DAL.Entities;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SchoolHealthWPF.ParentPages
{
    public partial class SendMedicineWindow : Window
    {
        private readonly MedicineRequestService _medicineService;
        private readonly StudentService _studentService;
        private readonly List<Student> _students;

        public SendMedicineWindow(List<Student> students)
        {
            InitializeComponent();
            _students = students;

            
            cbStudents.ItemsSource = _students;
            cbStudents.DisplayMemberPath = "FullName";
            cbStudents.SelectedValuePath = "StudentId";
        }

        private void LoadStudents()
        {
            try
            {
                var students = _studentService.GetAllStudents();
                cbStudents.ItemsSource = students;
                cbStudents.DisplayMemberPath = "FullName";
                cbStudents.SelectedValuePath = "StudentId";
                cbStudents.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học sinh: " + ex.Message);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (cbStudents.SelectedItem is not Student selectedStudent)
            {
                MessageBox.Show("Vui lòng chọn học sinh.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string name = txtMedicineName.Text.Trim();
            string dosage = txtDosage.Text.Trim();
            string instructions = txtInstructions.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên thuốc không được để trống.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(dosage))
            {
                MessageBox.Show("Liều lượng không được để trống.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(instructions))
            {
                MessageBox.Show("Hướng dẫn sử dụng không được để trống.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (name.Length > 100 || dosage.Length > 100 || instructions.Length > 300)
            {
                MessageBox.Show("Một hoặc nhiều trường nhập vượt quá độ dài cho phép.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var request = new MedicineSent
            {
                StudentId = selectedStudent.StudentId,
                MedicineName = name,
                Dosage = dosage,
                Instruction = instructions
            };

            try
            {
                _medicineService.SendRequest(request);
                MessageBox.Show("Đã gửi thông tin thuốc cho trường.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi thông tin thuốc: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
