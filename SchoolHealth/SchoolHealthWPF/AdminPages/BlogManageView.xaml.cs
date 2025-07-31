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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolHealthWPF.AdminPages
{
    /// <summary>
    /// Interaction logic for BlogManageView.xaml
    /// </summary>
    public partial class BlogManageView : UserControl
    {
        private readonly BlogService _blogService;

        private bool _havePermission = true;

        public BlogManageView(bool havePermission)
        {
            InitializeComponent();
            _havePermission = havePermission;
            _blogService = new BlogService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetPermission();
            LoadData();
        }

        private void SetPermission()
        {
            if (_havePermission)
            {
                // Nếu có quyền, cho phép chỉnh sửa
                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                // Nếu không có quyền, vô hiệu hóa các nút chỉnh sửa
                btnCreate.IsEnabled = false; btnCreate.Foreground = new SolidColorBrush(Colors.Gray);
                btnUpdate.IsEnabled = false; btnUpdate.Foreground = new SolidColorBrush(Colors.Gray);
                btnDelete.IsEnabled = false; btnDelete.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void LoadData()
        {
            try
            {
                var blogs = _blogService.GetAllBlogs();
                dgBlogs.ItemsSource = blogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading blogs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedBlog = dgBlogs.SelectedItem as Blog;
            if (selectedBlog != null)
            {
                var updateBlogView = new BlogDialog();
                updateBlogView.Blog = selectedBlog;
                updateBlogView.ShowDialog();
                LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bài viết để cập nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var createBlogView = new BlogDialog();
            createBlogView.ShowDialog();
            LoadData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedBlog = dgBlogs.SelectedItem as Blog;
            if (selectedBlog != null)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bài viết này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _blogService.DeleteBlog(selectedBlog.BlogId);
                        LoadData();
                        MessageBox.Show("Xóa bài viết thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting blog: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bài viết để xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var searchBlogs = txtSearch.Text.ToLower().Trim();
            if (searchBlogs != null)
            {
                var list = _blogService.GetAllBlogs()
                    .Where(b => b.Title.ToLower().Trim().Contains(searchBlogs) || b.Content.ToLower().Trim().Contains(searchBlogs))
                    .ToList();
                dgBlogs.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadData();
            }
        }
    }
}
