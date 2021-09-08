using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto hotelRoomDto)
        {
            var hotelRoom = _mapper.Map<HotelRoomDto, HotelRoom>(hotelRoomDto);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom, HotelRoomDto>(addedHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int id)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(id);
            if (roomDetails == null) return 0;
            _db.HotelRooms.Remove(roomDetails);
            return await _db.SaveChangesAsync();
        }

        public async Task<List<HotelRoomDto>> GetAllHotelRooms()
        {
            try
            {
                var hotelRoomDTOs =
                    _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDto>>(await _db.HotelRooms.ToListAsync());
                return (List<HotelRoomDto>)hotelRoomDTOs;
            }
            catch { return null; }
        }

        public async Task<HotelRoomDto> GetHotelRoom(int roomId)
        {
            try
            {
                return _mapper
                    .Map<HotelRoom, HotelRoomDto>(await _db.HotelRooms.FirstOrDefaultAsync(r => r.Id == roomId));
            } 
            catch { return null; }
        }

        // if unique returns null else returns the room object
        public async Task<HotelRoomDto> IsRoomUnique(string name)
        {
            try
            {
                return _mapper
                    .Map<HotelRoom, HotelRoomDto>(await _db.HotelRooms.FirstOrDefaultAsync(r => r.Name == name.ToLower()));
            }
            catch { return null; }
        }

        public async Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    var roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    var room = _mapper.Map<HotelRoomDto, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdatedTime = DateTime.Now;
                    room.UpdatedBy = "";
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDto>(updatedRoom.Entity);
                }
                else return null; 
            }
            catch { return null; }
        }
    }
}
