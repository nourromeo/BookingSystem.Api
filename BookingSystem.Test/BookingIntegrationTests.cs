using BookingSystem.Api.Data;
using BookingSystem.Api.Services;
using BookingSystem.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Test
{
    public class BookingIntegrationTests
    {
        // Create SQLite Database In-Memory
        private BookingSystemApiContext CreateSQLiteDb()
        {
            var options = new DbContextOptionsBuilder<BookingSystemApiContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new BookingSystemApiContext(options);
            
            // SQLite
            context.Database.OpenConnection();   
            context.Database.EnsureCreated();    

            return context;
        }

        // Testa att bokningar sparas korrekt i databasen.
        [Fact]
        public async Task BookingSavedToDatabase()
        {
            using var context = CreateSQLiteDb();
            var service = new BookingService(context);

            var start = DateTime.Now.AddHours(1);
            var end = start.AddHours(1);

            var booking = await service.CreateBooking("RoomA", start, end);

            Assert.Equal(1, context.booking.Count());
            Assert.Equal("RoomA", booking.RoomName);
        }

        // 2️⃣ Testa att bokningar kan hämtas från databasen.
        [Fact]
        public async Task BookingGetFromDatabase()
        {
            using var context = CreateSQLiteDb();
            var service = new BookingService(context);

            var start = DateTime.Now.AddHours(1);
            var end = start.AddHours(1);

            await service.CreateBooking("RoomA", start, end);

            var result = await service.GetBookingsForRoom("RoomA");

            Assert.Single(result);
            Assert.Equal("RoomA", result[0].RoomName);
        }

        // 3️⃣ Testa att systemet fungerar med en riktig databas (SQLite)
        [Fact]
        public async Task RealSQLiteDatabaseTest()
        {
            using var context = CreateSQLiteDb();
            var service = new BookingService(context);

            var start = DateTime.Now.AddHours(1);
            var end = start.AddHours(1); 

            var booking = await service.CreateBooking("RoomX", start, end);

            Assert.NotNull(booking);
            Assert.Equal("RoomX", booking.RoomName);
            Assert.Equal(1, context.booking.Count()); 
        }
    }
}
