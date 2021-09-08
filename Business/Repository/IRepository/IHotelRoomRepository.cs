using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto hotelRoomDTO);

        public Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDTO);

        public Task<HotelRoomDto> GetHotelRoom(int roomId);

        public Task<IEnumerable<HotelRoomDto>> GetAllHotelRooms();

        public Task<HotelRoomDto> IsRoomUnique(string name);
       
        public Task<int> DeleteHotelRoom(int id);
    }
}
