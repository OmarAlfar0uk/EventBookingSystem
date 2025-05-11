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
    public class EventConttroller(IEventService _eventService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev is null)
                return NotFound("Event not found");
            return Ok(ev);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] EventDTo eventDto)
        {
            await _eventService.AddEventAsync(eventDto);
            return Ok("Event added successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EventDTo eventDto)
        {
            await _eventService.UpdateEventAsync(id, eventDto);
            return Ok("Event updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return Ok("Event deleted successfully");
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var result = await _eventService.SearchEventsAsync(keyword);
            return Ok(result);
        }

        [HttpGet("daterange")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var result = await _eventService.GetEventsByDateRangeAsync(start, end);
            return Ok(result);
        }
    }
}
