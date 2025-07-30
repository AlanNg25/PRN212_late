using BLL.Service;
using DAL.Entities;
using DAL.Repo;
using System;
using System.Windows;

namespace SchoolHealthWPF
{
    public partial class HealthRecordWindow : Window
    {
        private readonly HealthRecordService _healthRecordService;
        private readonly StudentService _studentService;

        public HealthRecordWindow()
        {
            InitializeComponent();

            // Khởi tạo repository và service đúng 3-layer
            var healthRepo = new HealthRecordRepository();
            var studentRepo = new StudentRepository();

            _healthRecordService = new HealthRecordService(healthRepo);
            _studentService = new StudentService(studentRepo);

            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                var students = _studentService.GetAllStudents();
                StudentComboBox.ItemsSource = students;
                StudentComboBox.DisplayMemberPath = "FullName";
                StudentComboBox.SelectedValuePath = "StudentId";
                StudentComboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học sinh: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra đã chọn học sinh chưa
            if (StudentComboBox.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Lấy thông tin từ form
            int studentId = (int)StudentComboBox.SelectedValue;
            string allergies = AllergiesBox.Text.Trim();
            string chronicDiseases = ChronicDiseasesBox.Text.Trim();
            string vaccination = VaccinationHistoryBox.Text.Trim();
            string vision = VisionBox.Text.Trim();
            string hearing = HearingBox.Text.Trim();


            var record = new HealthRecord
            {
                StudentId = studentId,
                Allergy = allergies,
                ChronicDisease = chronicDiseases,
                MedicalHistory = vaccination,
                Vision = vision,
                Hearing = hearing

            };

            try
            {
                _healthRecordService.CreateHealthRecord(record);
                MessageBox.Show("Gửi hồ sơ thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hồ sơ:\n" + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            StudentComboBox.SelectedIndex = -1;
            AllergiesBox.Clear();
            ChronicDiseasesBox.Clear();
            VaccinationHistoryBox.Clear();
            VisionBox.Clear();
            HearingBox.Clear();

        }
    }
}
