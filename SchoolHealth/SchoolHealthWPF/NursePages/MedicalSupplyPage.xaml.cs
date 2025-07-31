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
    /// Interaction logic for MedicalSupplyPage.xaml
    /// </summary>
    public partial class MedicalSupplyPage : Window
    {
        private readonly MedicalSupplyService _service;

        public MedicalSupplyPage()
        {
            InitializeComponent();
            var context = new StudentHealthManagementContext();
            _service = new MedicalSupplyService(context);
            LoadData();
        }

        private void LoadData()
        {
            dgSupplies.ItemsSource = _service.GetAllSupplies();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            if (dgSupplies.SelectedItem is MedicalSupply selected)
            {
                if (InputDialog("Nhập số lượng nhập:", out int amount) && amount > 0)
                {
                    _service.IncreaseQuantity(selected.SupplyId, amount);
                    LoadData();
                }
            }
            else MessageBox.Show("Vui lòng chọn thuốc để nhập kho.");
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (dgSupplies.SelectedItem is MedicalSupply selected)
            {
                if (InputDialog("Nhập số lượng xuất:", out int amount) && amount > 0)
                {
                    if (!_service.DecreaseQuantity(selected.SupplyId, amount))
                        MessageBox.Show("Không đủ số lượng để xuất.");
                    LoadData();
                }
            }
            else MessageBox.Show("Vui lòng chọn thuốc để xuất kho.");
        }

        private bool InputDialog(string prompt, out int result)
        {
            result = 0;
            var input = Microsoft.VisualBasic.Interaction.InputBox(prompt, "Nhập số lượng", "1");
            return int.TryParse(input, out result);
        }
    }
}
