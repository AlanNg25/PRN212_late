using System;
using System.Windows;
using System.Windows.Controls;

namespace SchoolHealthWPF.AdminPages
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public AddUserWindow()
        {
            InitializeComponent();
            RoleComboBox.SelectedIndex = 0;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Username = UsernameTextBox.Text.Trim();
            Password = PasswordTextBox.Text;
            Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Role))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
