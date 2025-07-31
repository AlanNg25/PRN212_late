using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class MedicalSupplyService
    {
        private readonly StudentHealthManagementContext _context;

        public MedicalSupplyService(StudentHealthManagementContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả thuốc
        public List<MedicalSupply> GetAllSupplies()
        {
            return _context.MedicalSupplies.ToList();
        }

        // Nhập kho - tăng số lượng
        public bool IncreaseQuantity(int supplyId, int amount)
        {
            var supply = _context.MedicalSupplies.Find(supplyId);
            if (supply == null) return false;

            supply.Quantity += amount;
            _context.SaveChanges();
            return true;
        }

        // Xuất kho - giảm số lượng
        public bool DecreaseQuantity(int supplyId, int amount)
        {
            var supply = _context.MedicalSupplies.Find(supplyId);
            if (supply == null || supply.Quantity < amount) return false;

            supply.Quantity -= amount;
            _context.SaveChanges();
            return true;
        }
    }
}
