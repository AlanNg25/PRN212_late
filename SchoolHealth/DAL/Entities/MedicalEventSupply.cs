using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class MedicalEventSupply
    {
        public int MedicalEventSupplyId { get; set; }
        public int MedicalEventId { get; set; }
        public int MedicalSupplyId { get; set; }
        public int QuantityUsed { get; set; }

        public virtual MedicalEvent MedicalEvent { get; set; }
        public virtual MedicalSupply MedicalSupply { get; set; }
    }
}
