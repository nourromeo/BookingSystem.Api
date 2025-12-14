using Microsoft.AspNetCore.Mvc;
using BookingSystem.Api.Services;
using BookingSystem.Api.Model;

namespace BookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _service;

        public BookingsController(BookingService service)
        {
            _service = service;
        }

        // 1) Skapa bokning
        [HttpPost]
        public async Task<ActionResult<booking>> CreateBooking(string roomName, DateTime startTime, DateTime endTime)
        {
            try
            {
                var result = await _service.CreateBooking(roomName, startTime, endTime);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 2) Lista bokningar för ett rum
        [HttpGet("room/{roomName}")]
        public async Task<ActionResult<IEnumerable<booking>>> GetBookingsForRoom(string roomName)
        {
            var bookings = await _service.GetBookingsForRoom(roomName);
            return Ok(bookings);
        }

        // 3) Kontrollera tillgänglighet
        [HttpGet("available")]
        public async Task<ActionResult<bool>> CheckAvailability(string roomName, DateTime startTime, DateTime endTime)
        {
            try
            {
                var available = await _service.IsAvailable(roomName, startTime, endTime);
                return Ok(available);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // hämta alla bokningar - detta är för min referens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<booking>>> GetAllBookings()
        {
            var bookings = await _service.GetAllBookings();
            return Ok(bookings);
        }

    }
}
