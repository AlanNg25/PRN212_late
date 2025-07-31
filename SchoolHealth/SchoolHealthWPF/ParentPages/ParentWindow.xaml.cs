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
    /// Interaction logic for ParentWindow.xaml
    /// </summary>
    public partial class ParentWindow : Window
    {
        private readonly int _parentId;

        public ParentWindow(int parentId)
        {
            InitializeComponent();
            _parentId = parentId;
        }

        private void ViewHealthHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new ParentHealthCheckHistoryWindow(_parentId);
            historyWindow.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Optionally: go back to login window
            this.Close();
        }
    }
}
