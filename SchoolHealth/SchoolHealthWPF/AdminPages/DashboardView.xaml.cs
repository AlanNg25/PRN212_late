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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolHealthWPF.AdminPages
{
    public partial class DashboardView : UserControl
    {
        private List<MedicalEventViewModel> allMedicalEvents;
        private readonly StudentService _studentService;

        public DashboardView()
        {
            InitializeComponent();
            _studentService = new StudentService();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // Static data (replace with database queries)
            var parents = new List<Parent>
            {
                new Parent { ParentId = 1, FullName = "Nguyễn Văn A" },
                new Parent { ParentId = 2, FullName = "Lê Thị B" }
            };

            var students = _studentService.GetAllStudents();

            allMedicalEvents = new List<MedicalEventViewModel>
            {
                new MedicalEventViewModel { StudentId = 1, Date = DateTime.Parse("2024-10-01 09:00"), Description = "Đau bụng sau giờ ăn", StudentName = "Nguyễn Minh Khoa" },
                new MedicalEventViewModel { StudentId = 2, Date = DateTime.Parse("2024-11-15 14:00"), Description = "Té ngã nhẹ trong giờ ra chơi", StudentName = "Lê Mai Linh" }
            };

            var medicalSupplies = new List<MedicalSupply>
            {
                new MedicalSupply { Name = "Băng cá nhân", Quantity = 200, ExpirationDate = DateOnly.Parse("2026-01-01") },
                new MedicalSupply { Name = "Cồn y tế 70%", Quantity = 50, ExpirationDate = DateOnly.Parse("2025-12-31") }
            };

            var blogs = new List<Blog>
            {
                new Blog { Title = "Làm sao để trẻ không sợ tiêm?", Type = 1 },
                new Blog { Title = "Cách chăm sóc sức khỏe mùa thi", Type = 2 }
            };

            // Update statistics
            ParentCount.Text = parents.Count.ToString();
            StudentCount.Text = students.Count.ToString();
            MedicalEventCount.Text = allMedicalEvents.Count.ToString();
            MedicalSupplyCount.Text = medicalSupplies.Count.ToString();

            // Update lists
            RecentMedicalEvents.ItemsSource = allMedicalEvents.OrderByDescending(e => e.Date).Take(5).ToList();
            RecentBlogs.ItemsSource = blogs.Select(b => new BlogViewModel
            {
                Title = b.Title,
                TypeName = b.Type == 1 ? "Chia sẻ kiến thức y tế" : "Hướng dẫn sinh hoạt"
            }).Take(5).ToList();
        }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm...") txtSearch.Text = "";
        }

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text)) txtSearch.Text = "Tìm kiếm...";
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (searchText == "Tìm kiếm..." || string.IsNullOrEmpty(searchText))
            {
                RecentMedicalEvents.ItemsSource = allMedicalEvents.OrderByDescending(e => e.Date).Take(5).ToList();
            }
            else
            {
                RecentMedicalEvents.ItemsSource = allMedicalEvents
                    .Where(me => me.StudentName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                 me.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(e => e.Date)
                    .Take(5)
                    .ToList();
            }
        }
    }

    // View models for UI
    public class MedicalEventViewModel
    {
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string StudentName { get; set; }
    }

    public class BlogViewModel
    {
        public string Title { get; set; }
        public string TypeName { get; set; }
    }
}