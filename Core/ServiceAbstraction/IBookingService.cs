using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTo>> GetAllBookingsAsync();
        Task<BookingDTo?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingDTo>> GetBookingsByUserIdAsync(int userId);
        Task<IEnumerable<BookingDTo>> GetBookingsByEventIdAsync(int eventId);
        Task AddBookingAsync(BookingDTo bookingDto);
        Task CancelBookingAsync(int id);
    }
}
