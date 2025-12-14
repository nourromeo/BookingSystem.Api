using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Api.Model;

namespace BookingSystem.Api.Data
{
    public class BookingSystemApiContext : DbContext
    {
        public BookingSystemApiContext (DbContextOptions<BookingSystemApiContext> options)
            : base(options)
        {
        }

        public DbSet<booking> booking { get; set; } = default!;
    }
}
