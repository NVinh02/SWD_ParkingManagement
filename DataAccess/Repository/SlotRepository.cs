using BusinessObject.BusinessModels;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SlotRepository : ISlotRepository
    {
        public Task ChangeSlotActiveStatus(Slot slot) 
            => SlotDAO.Instance.ChangeSlotActiveStatus(slot);

        public Task ChangeSlotBookingStatus(Slot slot)
            => SlotDAO.Instance.ChangeSlotBookStatus(slot);

        public Task ChangeSlotOccupiedStatus(Slot slot)
            => SlotDAO.Instance.ChangeSlotOccupiedStatus(slot);

        public Task<Slot> GetSlotById(int? id)
            => SlotDAO.Instance.GetSlotById(id);

        public Task<IEnumerable<Slot>> GetSlots()
            => SlotDAO.Instance.GetSlots();

        public Task<IEnumerable<Slot>> GetSlotsByFloor(int? floor)
            => SlotDAO.Instance.GetSlotsByFloor(floor);

        public Task<IEnumerable<SlotForm>> GetSlotsByFloorType1(string carType)
            => SlotDAO.Instance.GetSlotsByFloorType1(carType);

        public Task<IEnumerable<SlotForm>> GetSlotsByFloorType2(string carType, DateTime startDate, DateTime endDate)
            => SlotDAO.Instance.GetSlotsByFloorType2(carType, startDate, endDate);

        public Task<IEnumerable<SlotForm>> GetSlotsByFloorType3(string carType, DateTime startDate, DateTime endDate)
            => SlotDAO.Instance.GetSlotsByFloorType3(carType, startDate, endDate);

        public Task<IEnumerable<SlotForm>> GetSlotsByFloorType4(string carType, DateTime startDate, DateTime endDate)
            => SlotDAO.Instance.GetSlotsByFloorType4(carType, startDate, endDate);

        public Task<IEnumerable<Slot>> GetSlotsForBooking()
            => SlotDAO.Instance.GetSlotsForBooking();
    }
}
