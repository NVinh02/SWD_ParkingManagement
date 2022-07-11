using BusinessObject.BusinessModels;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SlotDAO
    {
        private static SlotDAO instance = null;
        private static readonly object instanceLock = new object();
        private SlotDAO() { }
        public static SlotDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SlotDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<IEnumerable<Slot>> GetSlots()
        {
            List<Slot> slots = null;
            try
            {
                var _context = new ParkingManagementContext();
                slots = await _context.Slots.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return slots;
        }

        public async Task<IEnumerable<Slot>> GetSlotsByFloor(int? floor)
        {
            List<Slot> slots = null;
            try
            {
                var _context = new ParkingManagementContext();
                slots = await _context.Slots.Where(s => s.Floor == floor).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return slots;
        }

        public async Task<IEnumerable<SlotForm>> GetSlotsByFloorType1(string carType)
        {
            List<Slot> slots = null;
            List<SlotForm> availableSlots = new List<SlotForm>();
            try
            {
                var _context = new ParkingManagementContext();
                if (carType.Equals("Bike"))
                {
                    slots = await _context.Slots.Where(s => s.Floor == 1 || s.Floor == 2).ToListAsync();
                    foreach(Slot s in slots)
                    {
                        SlotForm slotForm = null;
                        slotForm = new()
                        {
                            SlotId = s.SlotId,
                            Floor = s.Floor,
                            Position = s.Position,
                            IsActive = s.IsActive,
                            IsAvailable = true
                        };

                        if(_context.ParkingInfos.Any(p => p.SlotId == s.SlotId 
                                                    && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))))
                        {
                            slotForm.IsAvailable = false;
                        }
                        if (!slotForm.IsActive)
                        {
                            slotForm.IsAvailable = false;
                        }

                        availableSlots.Add(slotForm);
                    }
                }
                else if (carType.Equals("Car"))
                {
                    slots = await _context.Slots.Where(s => s.Floor == 3).ToListAsync();
                    foreach (Slot s in slots)
                    {
                        SlotForm slotForm = null;
                        slotForm = new()
                        {
                            SlotId = s.SlotId,
                            Floor = s.Floor,
                            Position = s.Position,
                            IsActive = s.IsActive,
                            IsAvailable = true
                        };

                        if (_context.ParkingInfos.Any(p => p.SlotId == s.SlotId
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))))
                        {
                            slotForm.IsAvailable = false;
                        }
                        if (!slotForm.IsActive)
                        {
                            slotForm.IsAvailable = false;
                        }

                        availableSlots.Add(slotForm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return availableSlots;
        }

        public async Task<IEnumerable<SlotForm>> GetSlotsByFloorType2(string carType, DateTime startDate, DateTime endDate)
        {
            List<Slot> slots = null;
            List<ParkingInfo> tempPK = new List<ParkingInfo>();
            List<SlotForm> availableSlots = new List<SlotForm>();
            try
            {
                var _context = new ParkingManagementContext();
                if (carType.Equals("Bike"))
                {
                    slots = await _context.Slots.Where(s => s.Floor == 1 || s.Floor == 2).ToListAsync();
                    foreach (Slot s in slots)
                    {
                        SlotForm slotForm = null;
                        slotForm = new()
                        {
                            SlotId = s.SlotId,
                            Floor = s.Floor,
                            Position = s.Position,
                            IsActive = s.IsActive,
                            IsAvailable = true
                        };

                        tempPK = _context.ParkingInfos.Where(p => p.SlotId == s.SlotId
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))).ToList();

                        if (tempPK.Count > 0)
                        {
                            foreach (ParkingInfo p in tempPK)
                            {
                                if ((startDate.CompareTo(p.CheckInTime) < 0 && endDate.CompareTo(p.CheckInTime) < 0) ||
                                    (startDate.CompareTo(p.CheckOutTime) > 0 && endDate.CompareTo(p.CheckOutTime) > 0))
                                {
                                    slotForm.IsAvailable = true;
                                }
                                else
                                {
                                    slotForm.IsAvailable = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            slotForm.IsAvailable = true;
                        }

                        if (!slotForm.IsActive)
                        {
                            slotForm.IsAvailable = false;
                        }

                        availableSlots.Add(slotForm);
                    }
                }
                else if (carType.Equals("Car"))
                {
                    slots = await _context.Slots.Where(s => s.Floor == 3).ToListAsync();
                    foreach (Slot s in slots)
                    {
                        SlotForm slotForm = null;
                        slotForm = new()
                        {
                            SlotId = s.SlotId,
                            Floor = s.Floor,
                            Position = s.Position,
                            IsActive = s.IsActive,
                            IsAvailable = true
                        };

                        tempPK = _context.ParkingInfos.Where(p => p.SlotId == s.SlotId
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))).ToList();

                        if (tempPK.Count > 0)
                        {
                            foreach (ParkingInfo p in tempPK)
                            {
                                if ((startDate.CompareTo(p.CheckInTime) < 0 && endDate.CompareTo(p.CheckInTime) < 0) ||
                                    (startDate.CompareTo(p.CheckOutTime) > 0 && endDate.CompareTo(p.CheckOutTime) > 0))
                                {
                                    slotForm.IsAvailable = true;
                                }
                                else
                                {
                                    slotForm.IsAvailable = false;
                                }
                            }
                        }
                        else
                        {
                            slotForm.IsAvailable = true;
                        }

                        if (!slotForm.IsActive)
                        {
                            slotForm.IsAvailable = false;
                        }

                        availableSlots.Add(slotForm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return availableSlots;
        }

        public async Task<IEnumerable<SlotForm>> GetSlotsByFloorType3(string carType, DateTime startDate, DateTime endDate)
        {
            List<Slot> slots = null;
            List<ParkingInfo> tempPK = new List<ParkingInfo>();
            List<SlotForm> availableSlots = new List<SlotForm>();
            try
            {
                var _context = new ParkingManagementContext();
                if (carType.Equals("Bike"))
                {
                    slots = await _context.Slots.Where(s => s.Floor == 1 || s.Floor == 2).ToListAsync();
                    foreach (Slot s in slots)
                    {
                        SlotForm slotForm = null;
                        slotForm = new()
                        {
                            SlotId = s.SlotId,
                            Floor = s.Floor,
                            Position = s.Position,
                            IsActive = s.IsActive,
                            IsAvailable = true
                        };

                        tempPK = _context.ParkingInfos.Where(p => p.SlotId == s.SlotId
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))).ToList();

                        if (tempPK.Count > 0)
                        {
                            foreach (ParkingInfo p in tempPK)
                            {
                                if((startDate.CompareTo(p.CheckInTime) < 0 && endDate.CompareTo(p.CheckInTime) < 0) ||
                                    (startDate.CompareTo(p.CheckOutTime) > 0 && endDate.CompareTo(p.CheckOutTime) > 0))
                                {
                                    slotForm.IsAvailable = true;
                                }
                                else
                                {
                                    slotForm.IsAvailable = false;
                                }
                            }
                        }
                        else
                        {
                            slotForm.IsAvailable = true;
                        }

                        if (!slotForm.IsActive)
                        {
                            slotForm.IsAvailable = false;
                        }

                        availableSlots.Add(slotForm);
                    }
                }
                else if (carType.Equals("Car"))
                {
                    slots = await _context.Slots.Where(s => s.Floor == 3).ToListAsync();
                    foreach (Slot s in slots)
                    {
                        SlotForm slotForm = null;
                        slotForm = new()
                        {
                            SlotId = s.SlotId,
                            Floor = s.Floor,
                            Position = s.Position,
                            IsActive = s.IsActive,
                            IsAvailable = true
                        };

                        tempPK = _context.ParkingInfos.Where(p => p.SlotId == s.SlotId
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))).ToList();

                        if (tempPK.Count > 0)
                        {
                            foreach (ParkingInfo p in tempPK)
                            {
                                if ((startDate.CompareTo(p.CheckInTime) < 0 && endDate.CompareTo(p.CheckInTime) < 0) ||
                                    (startDate.CompareTo(p.CheckOutTime) > 0 && endDate.CompareTo(p.CheckOutTime) > 0))
                                {
                                    slotForm.IsAvailable = true;
                                }
                                else
                                {
                                    slotForm.IsAvailable = false;
                                }
                            }
                        }
                        else
                        {
                            slotForm.IsAvailable = true;
                        }

                        if (!slotForm.IsActive)
                        {
                            slotForm.IsAvailable = false;
                        }

                        availableSlots.Add(slotForm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return availableSlots;
        }

        public async Task<IEnumerable<SlotForm>> GetSlotsByFloorType4(string carType, DateTime startDate, DateTime endDate)
        {
            List<Slot> slots = null;
            List<ParkingInfo> tempPK = new List<ParkingInfo>();
            List<SlotForm> availableSlots = new List<SlotForm>();
            try
            {
                var _context = new ParkingManagementContext();
                if (carType.Equals("Bike"))
                {
                    slots = await _context.Slots.Where(s => s.Floor == 1 || s.Floor == 2).ToListAsync();
                    foreach (Slot s in slots)
                    {
                        SlotForm slotForm = null;
                        slotForm = new()
                        {
                            SlotId = s.SlotId,
                            Floor = s.Floor,
                            Position = s.Position,
                            IsActive = s.IsActive,
                            IsAvailable = true
                        };

                        tempPK = _context.ParkingInfos.Where(p => p.SlotId == s.SlotId
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))).ToList();

                        if (tempPK.Count > 0)
                        {
                            foreach (ParkingInfo p in tempPK)
                            {
                                if ((startDate.CompareTo(p.CheckInTime) < 0 && endDate.CompareTo(p.CheckInTime) < 0) ||
                                    (startDate.CompareTo(p.CheckOutTime) > 0 && endDate.CompareTo(p.CheckOutTime) > 0))
                                {
                                    slotForm.IsAvailable = true;
                                }
                                else
                                {
                                    slotForm.IsAvailable = false;
                                }
                            }
                        }
                        else
                        {
                            slotForm.IsAvailable = true;
                        }

                        if (!slotForm.IsActive)
                        {
                            slotForm.IsAvailable = false;
                        }

                        availableSlots.Add(slotForm);
                    }
                }
                else if (carType.Equals("Car"))
                {
                    slots = await _context.Slots.Where(s => s.Floor == 3).ToListAsync();
                    foreach (Slot s in slots)
                    {
                        SlotForm slotForm = null;
                        slotForm = new()
                        {
                            SlotId = s.SlotId,
                            Floor = s.Floor,
                            Position = s.Position,
                            IsActive = s.IsActive,
                            IsAvailable = true
                        };

                        tempPK = _context.ParkingInfos.Where(p => p.SlotId == s.SlotId
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))).ToList();

                        if (tempPK.Count > 0)
                        {
                            foreach (ParkingInfo p in tempPK)
                            {
                                if ((startDate.CompareTo(p.CheckInTime) < 0 && endDate.CompareTo(p.CheckInTime) < 0) ||
                                    (startDate.CompareTo(p.CheckOutTime) > 0 && endDate.CompareTo(p.CheckOutTime) > 0))
                                {
                                    slotForm.IsAvailable = true;
                                }
                                else
                                {
                                    slotForm.IsAvailable = false;
                                }
                            }
                        }
                        else
                        {
                            slotForm.IsAvailable = true;
                        }

                        if (!slotForm.IsActive)
                        {
                            slotForm.IsAvailable = false;
                        }

                        availableSlots.Add(slotForm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return availableSlots;
        }

        public async Task<IEnumerable<Slot>> GetSlotsForBooking()
        {
            List<Slot> slots = null;
            try
            {
                var _context = new ParkingManagementContext();
                slots = await _context.Slots.Where(s => s.IsActive == true && s.IsOccupied == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return slots;
        }

        public async Task<Slot> GetSlotById(int? id)
        {
            Slot slot = null;
            try
            {
                var _context = new ParkingManagementContext();
                slot = await _context.Slots.FirstOrDefaultAsync(m => m.SlotId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return slot;
        }

        public async Task ChangeSlotOccupiedStatus(Slot slot)
        {
            try
            {
                var _context = new ParkingManagementContext();
                bool occupied = !slot.IsOccupied;
                slot.IsOccupied = occupied;
                _context.Slots.Update(slot);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ChangeSlotActiveStatus(Slot slot)
        {
            try
            {
                var _context = new ParkingManagementContext();
                bool active = !slot.IsActive;
                slot.IsActive = active;
                _context.Slots.Update(slot);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ChangeSlotBookStatus(Slot slot)
        {
            try
            {
                var _context = new ParkingManagementContext();
                bool book = !slot.IsBook;
                slot.IsBook = book;
                _context.Slots.Update(slot);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
