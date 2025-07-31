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
using BLL.Service;
using DAL.Entities;
using DAL.Repo;
using System.Collections.Generic;
using System.Linq;

namespace SchoolHealthWPF.ParentPages
{
    public partial class MedicalHistoryWindow : Window
    {
        public MedicalHistoryWindow(int parentId)
        {
            InitializeComponent();

            var studentService = new StudentService();
            var students = studentService.GetStudentsByParentId(parentId);

            if (students == null || !students.Any())
            {
                MessageBox.Show("Không có học sinh nào được liên kết với tài khoản này.");
                return;
            }

            var context = new StudentHealthManagementContext();
            var healthCheckRepo = new HealthCheckRepository();
            var healthCheckService = new HealthCheckService(healthCheckRepo);

            var allChecks = new List<HealthCheck>();
            foreach (var student in students)
            {
                var checks = healthCheckService.GetByStudentId(student.StudentId);
                allChecks.AddRange(checks);
            }

            dgMedicalHistory.ItemsSource = allChecks
                .OrderByDescending(h => h.Date)
                .ToList();
        }
    }
}

