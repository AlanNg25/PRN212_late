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

namespace SchoolHealthWPF.NursePages
{
    /// <summary>
    /// Interaction logic for NurseWindow.xaml
    /// </summary>
    public partial class NurseWindow : Window
    {
        private List<Student> students;
        private readonly HealthCheckService _healthCheckService = new HealthCheckService();

        public NurseWindow()
        {
            InitializeComponent();
            Loaded += NurseWindow_Loaded;
            StudentDataGrid.SelectionChanged += StudentDataGrid_SelectionChanged;

            // No need to assign EventDescriptionTextBox and TreatmentTextBox here,
            // they are already initialized by the designer as internal fields.
        }

        private void NurseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                using (var context = new StudentHealthManagementContext())
                {
                    students = context.Students.ToList();
                }
                StudentDataGrid.ItemsSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học sinh: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveHealthCheck_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = StudentDataGrid.SelectedItem as Student;
            if (selectedStudent == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh từ bảng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string note = NoteTextBox.Text.Trim();
            string result = ResultTextBox.Text.Trim();

            // Basic validation: not empty and minimum length
            if (string.IsNullOrWhiteSpace(result))
            {
                MessageBox.Show("Vui lòng nhập kết quả khám sức khỏe.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (result.Length < 5)
            {
                MessageBox.Show("Kết quả khám sức khỏe phải có ít nhất 5 ký tự.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var healthCheck = new HealthCheck
                {
                    StudentId = selectedStudent.StudentId,
                    DoctorNotes = note,
                    Result = result,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                };

                bool saveResult = _healthCheckService.SaveHealthCheck(healthCheck);

                if (saveResult)
                {
                    MessageBox.Show("Lưu kết quả khám sức khỏe thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    NoteTextBox.Clear();
                    ResultTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Không thể lưu dữ liệu. Vui lòng thử lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedStudent = StudentDataGrid.SelectedItem as Student;
            if (selectedStudent == null)
            {
                NoteTextBox.Text = "";
                ResultTextBox.Text = "";
                return;
            }

            // Fetch previous health check for the selected student
            using (var context = new StudentHealthManagementContext())
            {
                var lastHealthCheck = context.HealthChecks
                    .Where(hc => hc.StudentId == selectedStudent.StudentId)
                    .OrderByDescending(hc => hc.Date)
                    .FirstOrDefault();

                if (lastHealthCheck != null)
                {
                    NoteTextBox.Text = lastHealthCheck.DoctorNotes ?? "";
                    ResultTextBox.Text = lastHealthCheck.Result ?? "";
                }
                else
                {
                    NoteTextBox.Text = "";
                    ResultTextBox.Text = "";
                }
            }
        }

        private void SaveMedicalEvent_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = StudentDataGrid.SelectedItem as Student;
            if (selectedStudent == null) return;

            string description = EventDescriptionTextBox.Text.Trim();
            string treatment = TreatmentTextBox.Text.Trim();

            // Ví dụ: giả sử bạn đã có ID vật tư y tế từ người dùng
            int selectedSupplyId = 1; // Bạn cần lấy cái này từ UI thực tế, ví dụ từ ComboBox

            using (var context = new StudentHealthManagementContext())
            {
                // Kiểm tra vật tư y tế có tồn tại
                var supply = context.MedicalSupplies.FirstOrDefault(s => s.SupplyId == selectedSupplyId);
                if (supply == null)
                {
                    MessageBox.Show("Vật tư y tế không tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Trừ kho vật tư nếu cần
                supply.Quantity -= 1;

                var medicalEvent = new MedicalEvent
                {
                    StudentId = selectedStudent.StudentId,
                    Date = DateTime.Now,
                    Description = description,
                    TreatmentGiven = treatment,
                    // Không liên kết trực tiếp với MedicalSupply vì database không hỗ trợ
                };

                context.MedicalEvents.Add(medicalEvent);
                context.SaveChanges();

                MessageBox.Show("Lưu sự kiện y tế và cập nhật kho vật tư thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Add this helper class at the top or in a suitable location
        public class SupplyUsage
        {
            public int SupplyId { get; set; }
            public int Quantity { get; set; }
        }

        // Add this method inside the NurseWindow class
        private List<SupplyUsage> GetSelectedSuppliesFromUI()
        {
            // TODO: Replace with actual logic to get selected supplies from your UI
            // For now, return an empty list to avoid compilation error
            return new List<SupplyUsage>();
        }

        private void OpenMedicalSupplyPage_Click(object sender, RoutedEventArgs e)
        {
            var supplyWindow = new MedicalSupplyPage(); // Cần tạo riêng
            supplyWindow.Show();
        }

        private void OpenHealthCheckHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HealthCheckHistoryWindow(); // Đã có
            historyWindow.Show();
        }
    }
}
