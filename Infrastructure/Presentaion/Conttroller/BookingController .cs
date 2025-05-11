using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Conttroller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController(IBookingService _bookingService) : ControllerBase
    {

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            return booking is null ? NotFound() : Ok(booking);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);
            return Ok(bookings);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetByEventId(int eventId)
        {
            var bookings = await _bookingService.GetBookingsByEventIdAsync(eventId);
            return Ok(bookings);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BookingDTo bookingDto)
        {
            await _bookingService.AddBookingAsync(bookingDto);
            return Ok();
        }

        [HttpDelete("Cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _bookingService.CancelBookingAsync(id);
            return Ok();
        }
    }
}