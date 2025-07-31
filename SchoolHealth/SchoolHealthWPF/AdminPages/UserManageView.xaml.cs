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
    /// Interaction logic for UserManage.xaml
    /// </summary>
    public partial class UserManage : UserControl
    {
        private bool _havePermission = true; // Biến này có thể được sử dụng để kiểm tra quyền truy cập
        public UserManage(bool havePermission)
        {
            InitializeComponent();
            _havePermission = havePermission;
        }

        private void SetPermission()
        {
            if (_havePermission)
            {
                // Nếu có quyền, cho phép chỉnh sửa
                //btnAddUser.IsEnabled = true;
                //btnEditUser.IsEnabled = true;
                //btnDeleteUser.IsEnabled = true;
            }
            else
            {
                // Nếu không có quyền, vô hiệu hóa các nút chỉnh sửa
                //btnAddUser.IsEnabled = false;
                //btnEditUser.IsEnabled = false;
                //btnDeleteUser.IsEnabled = false;
            }
        }
    }
}
