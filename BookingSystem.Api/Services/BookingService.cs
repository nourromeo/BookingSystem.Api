using BookingSystem.Api.Data;
using BookingSystem.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Api.Services
{
    public class BookingService
    {
        private readonly BookingSystemApiContext _context;

        public BookingService(BookingSystemApiContext context)
        {
            _context = context;
        }

        // Kontrollera tillgänglighet
        public async Task<bool> IsAvailable(string roomName, DateTime start, DateTime end)
        {
            return !await _context.booking.AnyAsync(b =>
                b.RoomName == roomName &&
                b.StartTime < end &&
                start < b.EndTime);
        }

        // Skapa bokning
        public async Task<booking> CreateBooking(string roomName, DateTime start, DateTime end)
        {
            if (!await IsAvailable(roomName, start, end))
                throw new InvalidOperationException("Det finns en överlappande bokning.");

            var newBooking = new booking
            {
                RoomName = roomName,
                StartTime = start,
                EndTime = end
            };

            _context.booking.Add(newBooking);
            await _context.SaveChangesAsync();

            return newBooking;
        }
 
        // Lista bokningar för ett rum
        public async Task<List<booking>> GetBookingsForRoom(string roomName)
        {
            return await _context.booking
                .Where(b => b.RoomName == roomName)
                .ToListAsync();
        }

        public async Task<List<booking>> GetAllBookings()
        {
            return await _context.booking.ToListAsync();
        }
    }
}
