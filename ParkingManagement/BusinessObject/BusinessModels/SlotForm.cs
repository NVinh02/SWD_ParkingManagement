using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.BusinessModels
{
    public class SlotForm
    {
        public int SlotId { get; set; }
        public int Floor { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }
    }
}
