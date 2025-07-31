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
        private readonly AccountService _accountService;
        private readonly BlogService _blogService;
        private readonly MedicalEventService _medicalEventService;
        private readonly MedicalSupplyService _medicalSupplyService;

        public DashboardView()
        {
            InitializeComponent();
            var context = new StudentHealthManagementContext();
            _studentService = new StudentService();
            _accountService = new AccountService();
            _blogService = new BlogService();
            _medicalEventService = new MedicalEventService();
            _medicalSupplyService = new MedicalSupplyService(context);
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // Static data (replace with database queries)
            var parents = _accountService.GetParents();

            var students = _studentService.GetAllStudents();

            allMedicalEvents = _medicalEventService.GetMedicalEvents()
                .Select(me => new MedicalEventViewModel
                {
                    StudentId = me.StudentId,
                    Date = me.Date,
                    Description = me.Description,
                    StudentName = me.Student?.FullName ?? "Unknown"
                }).ToList();

            var medicalSupplies = _medicalSupplyService.GetAllSupplies()
                .Select(s => new MedicalSupply
                {
                    SupplyId = s.SupplyId,
                    Name = s.Name,
                    Quantity = s.Quantity
                }).ToList();

            var blogs = _blogService.GetAllBlogs();

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
                TypeName = b.Type == 1 ? "Blog" : "Medical"
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