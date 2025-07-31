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
        public Blog Blog { get; set; }
        public BlogDialog()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (Blog == null)
            {
                Blog = new Blog
                {
                    //Title = txtTitle.Text,
                    //Content = txtContent.Text,
                    //AuthorId = int.Parse(txtAuthorId.Text),
                    //CreatedAt = DateTime.Now
                };
            }
            else
            {
                //Blog.Title = txtTitle.Text;
                //Blog.Content = txtContent.Text;
                //Blog.AuthorId = int.Parse(txtAuthorId.Text);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Blog != null)
            {
                TitleBox.Text = Blog.Title;
                ContentBox.Text = Blog.Content;
                AuthorComboBox.SelectedValue = Blog.AuthorId;
            }
        }

        private void LoadComboboxData()
        {
            // Load authors into the AuthorComboBox
            // This is a placeholder for actual data loading logic
            //AuthorComboBox.ItemsSource = _authorService.GetAllAuthors();
            //AuthorComboBox.DisplayMemberPath = "Name"; // Assuming Author has a Name property
            //AuthorComboBox.SelectedValuePath = "Id"; // Assuming Author has an Id property
        }
    }
}
