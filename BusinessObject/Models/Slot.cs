using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Slot
    {
        public Slot()
        {
            ParkingInfos = new HashSet<ParkingInfo>();
        }

        public int SlotId { get; set; }
        public int Floor { get; set; }
        public string Position { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsBook { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ParkingInfo> ParkingInfos { get; set; }
    }
}
