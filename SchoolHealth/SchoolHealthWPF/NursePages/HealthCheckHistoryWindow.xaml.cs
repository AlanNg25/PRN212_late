using DAL.Entities;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for HealthCheckHistoryWindow.xaml
    /// </summary>
    public partial class HealthCheckHistoryWindow : Window
    {
        private readonly StudentHealthManagementContext _context;

        public HealthCheckHistoryWindow()
        {
            InitializeComponent();
            _context = new StudentHealthManagementContext();
            LoadData();
        }

        private void LoadData(string keyword = "")
        {
            var data = _context.HealthChecks
                .Include(h => h.Student)
                .Where(h => string.IsNullOrEmpty(keyword) || h.Student.FullName.Contains(keyword))
                .OrderByDescending(h => h.Date)
                .Select(h => new
                {
                    StudentName = h.Student.FullName,
                    h.Date,
                    h.Result,
                    h.DoctorNotes
                })
                .ToList();

            dgHealthChecks.ItemsSource = data;
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

    }
}
