using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IEventService
    {
        Task AddEventAsync(EventDTo eventDto);

        Task UpdateEventAsync(int id, EventDTo eventDto);

        Task DeleteEventAsync(int id);

        Task<IEnumerable<EventDTo>> GetAllEventsAsync();

        Task<EventDTo?> GetEventByIdAsync(int id);

        Task<IEnumerable<EventDTo>> SearchEventsAsync(string keyword);

        Task<IEnumerable<EventDTo>> GetEventsByDateRangeAsync(DateTime start, DateTime end);
    }
}
