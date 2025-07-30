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
        private readonly List<Student> _students;

        public HealthRecordWindow(List<Student> students)
        {
            InitializeComponent();

            var healthRepo = new HealthRecordRepository();
            _healthRecordService = new HealthRecordService(healthRepo);
            _students = students;

            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                StudentComboBox.ItemsSource = _students;
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
            if (StudentComboBox.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string allergies = AllergiesBox.Text.Trim();
            string chronicDiseases = ChronicDiseasesBox.Text.Trim();
            string vaccination = VaccinationHistoryBox.Text.Trim();
            string vision = VisionBox.Text.Trim();
            string hearing = HearingBox.Text.Trim();

            // Tối thiểu bạn có thể bắt buộc nhập một trong các trường
            if (string.IsNullOrWhiteSpace(allergies) &&
                string.IsNullOrWhiteSpace(chronicDiseases) &&
                string.IsNullOrWhiteSpace(vaccination) &&
                string.IsNullOrWhiteSpace(vision) &&
                string.IsNullOrWhiteSpace(hearing))
            {
                MessageBox.Show("Vui lòng nhập ít nhất một thông tin sức khỏe.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (allergies.Length > 200 || chronicDiseases.Length > 200 ||
                vaccination.Length > 300 || vision.Length > 100 || hearing.Length > 100)
            {
                MessageBox.Show("Một hoặc nhiều trường nhập vượt quá độ dài cho phép.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int studentId = (int)StudentComboBox.SelectedValue;

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
