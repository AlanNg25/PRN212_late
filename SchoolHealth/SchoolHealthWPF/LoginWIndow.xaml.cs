using BLL.Service;
using DAL.Entities;
using SchoolHealthWPF.AdminPages;
using SchoolHealthWPF.NursePages;
using SchoolHealthWPF.ParentPages;
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

namespace SchoolHealthWPF
{
    /// <summary>
    /// Interaction logic for LoginWIndow.xaml
    /// </summary>
    public partial class LoginWIndow : Window
    {
        private readonly AccountService _service;
        public LoginWIndow()
        {
            InitializeComponent();
            _service = new AccountService();
        }

        public enum Role
        {
            Admin,
            Manager,
            Nurse,
            Parent
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;
            try
            {
                var user = (UserAccount)_service.Login(userNameTxt.Text, Passwordtxt.Password);

                if (user != null)
                {
                    Application.Current.Properties["UserLog"] = user;
                    if (user.Role == Role.Admin.ToString()
                        || user.Role == Role.Manager.ToString())
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.user = user;
                        adminWindow.Role = user.Role == Role.Admin.ToString() ? 0 : 1;
                        adminWindow.Show();
                        this.Close();
                    }
                    if (user.Role == Role.Nurse.ToString())
                    {
                        NurseWindow nurseWindow = new NurseWindow();
                        nurseWindow.Show();
                        this.Close();
                    }
                    if (user.Role == Role.Parent.ToString())
                    {
                        ParentWindow parentWindow = new ParentWindow();
                        parentWindow.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(userNameTxt.Text)
            || string.IsNullOrWhiteSpace(Passwordtxt.Password))
            {
                MessageBox.Show("Fill all the fields!!");
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow home = new HomeWindow();
            home.Show();
            this.Close();
        }
    }
}
