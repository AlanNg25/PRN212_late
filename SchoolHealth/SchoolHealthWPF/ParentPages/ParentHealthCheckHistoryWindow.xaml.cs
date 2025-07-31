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

namespace SchoolHealthWPF.ParentPages
{
    /// <summary>
    /// Interaction logic for ParentHealthCheckHistoryWindow.xaml
    /// </summary>
    public partial class ParentHealthCheckHistoryWindow : Window
    {
        private readonly StudentHealthManagementContext _context;
        private readonly int _parentId;

        public ParentHealthCheckHistoryWindow(int parentId)
        {
            InitializeComponent();
            _context = new StudentHealthManagementContext();
            _parentId = parentId;
            LoadStudents();
        }

        private void LoadStudents()
        {
            var students = _context.Students
                .Where(s => s.ParentId == _parentId)
                .Select(s => new
                {
                    s.StudentId,
                    FullName = s.FullName
                })
                .ToList();

            cbStudents.ItemsSource = students;

            if (students.Count > 0)
            {
                cbStudents.SelectedIndex = 0;
            }
        }

        private void cbStudents_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbStudents.SelectedValue is int studentId)
            {
                var healthChecks = _context.HealthChecks
                    .Where(h => h.StudentId == studentId)
                    .Select(h => new
                    {
                        h.Date,
                        h.Result,
                        h.DoctorNotes
                    })
                    .ToList();

                dgHealthChecks.ItemsSource = healthChecks;
            }
        }
    }
}
