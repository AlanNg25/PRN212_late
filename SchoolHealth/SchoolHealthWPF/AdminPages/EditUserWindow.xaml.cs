using System;
using System.Windows;
using System.Windows.Controls;
using DAL.Entities;

namespace SchoolHealthWPF.AdminPages
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public EditUserWindow(UserAccount user)
        {
            InitializeComponent();
            UsernameTextBox.Text = user.Username;
            PasswordTextBox.Text = user.PasswordHash;
            RoleComboBox.SelectedIndex = GetRoleIndex(user.Role);
        }

        private int GetRoleIndex(string role)
        {
            switch (role)
            {
                //case "Admin": return 0;
                case "Nurse": return 0;
                case "Manager": return 1;
                case "Parent": return 2;
                default: return 0;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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
