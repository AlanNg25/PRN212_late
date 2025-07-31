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

namespace SchoolHealthWPF.AdminPages
{
    /// <summary>
    /// Interaction logic for BlogDialog.xaml
    /// </summary>
    public partial class BlogDialog : Window
    {
        private readonly AccountService _accountService;
        private readonly BlogService _blogService;
        public Blog Blog { get; set; }
        public BlogDialog()
        {
            InitializeComponent();
            _accountService = new AccountService();
            _blogService = new BlogService();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;
            if (Blog == null)
            {
                try
                {
                    Blog = new Blog
                    {
                        Title = TitleBox.Text,
                        Content = ContentBox.Text,
                        DatePosted = DatePostedPicker.SelectedDate ?? DateTime.Now,
                        AuthorId = (int)AuthorComboBox.SelectedValue,
                        Type = (int)TypeComboBox.SelectedValue
                    };
                    _blogService.CreateBlog(Blog);
                    this.Close();
                    MessageBox.Show("Blog created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating blog: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    Blog.Title = TitleBox.Text;
                    Blog.Content = ContentBox.Text;
                    Blog.DatePosted = DatePostedPicker.SelectedDate ?? DateTime.Now;
                    Blog.AuthorId = (int)AuthorComboBox.SelectedValue;
                    Blog.Type = (int)TypeComboBox.SelectedValue;
                    _blogService.UpdateBlog(Blog);
                    this.Close();
                    MessageBox.Show("Blog updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating blog: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboboxData();
            if (Blog != null)
            {
                TitleBox.Text = Blog.Title;
                ContentBox.Text = Blog.Content;
                DatePostedPicker.SelectedDate = Blog.DatePosted;
                AuthorComboBox.SelectedValue = Blog.AuthorId;
                TypeComboBox.SelectedValue = Blog.Type;
            }
        }

        private void LoadComboboxData()
        {
            AuthorComboBox.ItemsSource = _accountService.GetAllUsers();
            AuthorComboBox.DisplayMemberPath = "Username";
            AuthorComboBox.SelectedValuePath = "AccountId";

            TypeComboBox.ItemsSource = _blogService.GetAllBlogs()
                .Select(blog => new
                {
                    Type = blog.Type,
                    TypeName = blog.Type == 1 ? "Blog" :
                               blog.Type == 2 ? "Medical" : "Unknown"
                })
                .DistinctBy(item => item.Type);
            TypeComboBox.DisplayMemberPath = "TypeName";
            TypeComboBox.SelectedValuePath = "Type";
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Title cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(ContentBox.Text))
            {
                MessageBox.Show("Content cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (AuthorComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select an author.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TypeComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
