using DomainLayer.Contract;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookingService(IUnitOfWork _unitOfWork) : IBookingService
    {
        public async Task AddBookingAsync(BookingDTo bookingDto)
        {
            var repo = _unitOfWork.GetRepository<Booking, int>();

            var existing = (await repo.GetAllAsync())
                .FirstOrDefault(b => b.UserId == bookingDto.UserId && b.EventId == bookingDto.EventId);

            if (existing is not null)
                throw new Exception("Booking already exists for this user and event.");

            var newBooking = new Booking
            {
                EventId = bookingDto.EventId,
                UserId = bookingDto.UserId,
                BookingDate = DateTime.UtcNow
            };

            await repo.AddAsync(newBooking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelBookingAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Booking, int>();
            var booking = await repo.GetByIdAsync(id);

            if (booking is null)
                throw new Exception("Booking not found.");

            repo.Remove(booking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookingDTo>> GetAllBookingsAsync()
        {
            var repo = _unitOfWork.GetRepository<Booking, int>();
            var bookings = await repo.GetAllAsync();

            return bookings.Select(b => new BookingDTo
            {
                Id = b.Id,
                EventId = b.EventId,
                UserId = b.UserId,
                BookingDate = b.BookingDate
            });
        }

        public async Task<BookingDTo?> GetBookingByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Booking, int>();
            var b = await repo.GetByIdAsync(id);

            if (b is null) return null;

            return new BookingDTo
            {
                Id = b.Id,
                EventId = b.EventId,
                UserId = b.UserId,
                BookingDate = b.BookingDate
            };
        }

        public async Task<IEnumerable<BookingDTo>> GetBookingsByEventIdAsync(int eventId)
        {
            var repo = _unitOfWork.GetRepository<Booking, int>();
            var all = await repo.GetAllAsync();

            var filtered = all.Where(b => b.EventId == eventId);

            return filtered.Select(b => new BookingDTo
            {
                Id = b.Id,
                EventId = b.EventId,
                UserId = b.UserId,
                BookingDate = b.BookingDate
            });
        }

        public async Task<IEnumerable<BookingDTo>> GetBookingsByUserIdAsync(int userId)
        {
            var repo = _unitOfWork.GetRepository<Booking, int>();
            var all = await repo.GetAllAsync();

            var filtered = all.Where(b => b.UserId == userId);

            return filtered.Select(b => new BookingDTo
            {
                Id = b.Id,
                EventId = b.EventId,
                UserId = b.UserId,
                BookingDate = b.BookingDate
            });
        }
    }
}
