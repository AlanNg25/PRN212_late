using BLL.Service;
using DAL.Entities;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolHealthWPF.ParentPages
{
    public partial class HealthCheckFormConfirmWindow : Window
    {
        private readonly int _parentId;
        private readonly HealthCheckFormService _service;
        private List<HealthCheckForm> _forms;

        public HealthCheckFormConfirmWindow(int parentId)
        {
            InitializeComponent();
            _parentId = parentId;
            var repo = new HealthCheckFormRepository(new StudentHealthManagementContext());
            _service = new HealthCheckFormService(repo);
            LoadForms();
        }

        private void LoadForms()
        {
            _forms = _service.GetUnconfirmedFormsByParentId(_parentId).ToList();
            dgForms.ItemsSource = _forms;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var selectedForms = dgForms.SelectedItems.Cast<HealthCheckForm>().ToList();

            if (!selectedForms.Any())
            {
                MessageBox.Show("Vui lòng chọn ít nhất một phiếu để xác nhận.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var form in selectedForms)
            {
                _service.ConfirmForm(form.FormId);
            }

            MessageBox.Show("Xác nhận thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadForms(); // Refresh lại sau khi xác nhận
        }
    }
}
