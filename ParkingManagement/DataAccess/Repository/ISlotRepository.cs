using BusinessObject.BusinessModels;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISlotRepository
    {
        public Task<IEnumerable<Slot>> GetSlots();
        public Task<IEnumerable<Slot>> GetSlotsByFloor(int? floor);
        public Task<IEnumerable<Slot>> GetSlotsForBooking();
        public Task<Slot> GetSlotById(int? id);
        public Task ChangeSlotOccupiedStatus(Slot slot); 
        public Task ChangeSlotActiveStatus(Slot slot); 
        public Task ChangeSlotBookingStatus(Slot slot);
        public Task<IEnumerable<SlotForm>> GetSlotsByFloorType1(string carType);
        public Task<IEnumerable<SlotForm>> GetSlotsByFloorType2(string carType, DateTime startDate, DateTime endDate);
        public Task<IEnumerable<SlotForm>> GetSlotsByFloorType3(string carType, DateTime startDate, DateTime endDate);
        public Task<IEnumerable<SlotForm>> GetSlotsByFloorType4(string carType, DateTime startDate, DateTime endDate);
    }
}
