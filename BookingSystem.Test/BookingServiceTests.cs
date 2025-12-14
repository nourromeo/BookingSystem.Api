using BookingSystem.Api.Data;
using BookingSystem.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Test
{
    public class BookingServiceTests
    {

        private BookingSystemApiContext CreateInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<BookingSystemApiContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new BookingSystemApiContext(options);
        }

        // 1) Skapa bokning
        [Fact]
        public async Task CreateBookinggTest()
        {
            var db = CreateInMemoryDb();
            var service = new BookingService(db); 

            var start = DateTime.Now.AddHours(1);
            var end = start.AddHours(1);

            var booking = await service.CreateBooking("RoomA", start, end);

            Assert.NotNull(booking);
            Assert.Equal("RoomA", booking.RoomName);
            Assert.Equal(1, db.booking.Count());
        }

        // 2) Överlappande bokningar nekas
        [Fact]
        public async Task CreateBookingOverlappingTest()
        {
            var db = CreateInMemoryDb();
            var service = new BookingService(db);

            var start = DateTime.Now.AddHours(1);
            var end = start.AddHours(1);

            await service.CreateBooking("RoomA", start, end);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.CreateBooking("RoomA",
                    start.AddMinutes(30),
                    end.AddMinutes(30))
            );

            Assert.NotNull(exception);
        }

        // 3) Kontrollera tillgänglighet
        [Fact]
        public async Task IsAvailableTest() 
        {
            var db = CreateInMemoryDb();
            var service = new BookingService(db);

            var start = DateTime.Now.AddHours(1);
            var end = start.AddHours(1);

            await service.CreateBooking("RoomA", start, end);

            var available = await service.IsAvailable(
                "RoomA",
                start.AddMinutes(10),
                end.AddMinutes(10));

            Assert.False(available);
        }
    }
}
