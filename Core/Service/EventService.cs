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
    public class EventService(IUnitOfWork _unitOfWork) : IEventService
    {
        public async Task AddEventAsync(EventDTo eventDto)
        {
            var repo = _unitOfWork.GetRepository<Event, int>();

            var existing = (await repo.GetAllAsync())
                           .FirstOrDefault(e => e.Titel == eventDto.Titel && e.Date == eventDto.Date);

            if (existing is not null)
                throw new Exception("Event already exists on this date.");

            var newEvent = new Event
            {
                
                Titel = eventDto.Titel,
                Description = eventDto.Description,
                Category = eventDto.Category,
                Location = eventDto.Location,
                Date = eventDto.Date,
                Price = eventDto.Price,
                PictureUrl = eventDto.PictureUrl
            };

            await repo.AddAsync(newEvent);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteEventAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Event, int>();

            var ev = await repo.GetByIdAsync(id);

            if (ev is null)
                throw new Exception("Event not found.");

            repo.Remove(ev);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventDTo>> GetAllEventsAsync()
        {
            var repo = _unitOfWork.GetRepository<Event, int>();
            var events = await repo.GetAllAsync();

            var result = events.Select(e => new EventDTo
            {
                Id = e.Id,
                Titel = e.Titel,
                Description = e.Description,
                Location = e.Location,
                Category = e.Category,
                Date = e.Date,
                PictureUrl = e.PictureUrl,
                Price = e.Price
            });

            return result;
        }


        public async Task<EventDTo?> GetEventByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Event, int>();
            var ev = await repo.GetByIdAsync(id);

            if (ev == null) return null;

            return new EventDTo
            {
                Id = ev.Id,
                Titel = ev.Titel,
                Description = ev.Description,
                Location = ev.Location,
                Category = ev.Category,
                Date = ev.Date,
                Price = ev.Price,
                PictureUrl = ev.PictureUrl
            };
        }


        public async Task<IEnumerable<EventDTo>> GetEventsByDateRangeAsync(DateTime start, DateTime end)
        {
            var repo = _unitOfWork.GetRepository<Event, int>();
            var events = await repo.GetAllAsync();

            var filtered = events
                .Where(e => e.Date >= start && e.Date <= end)
                .Select(e => new EventDTo
                {
                    Id = e.Id,
                    Titel = e.Titel,
                    Description = e.Description,
                    Location = e.Location,
                    Category = e.Category,
                    Date = e.Date,
                    Price = e.Price,
                    PictureUrl = e.PictureUrl
                });

            return filtered;
        }


        public async Task<IEnumerable<EventDTo>> SearchEventsAsync(string keyword)
        {
            var repo = _unitOfWork.GetRepository<Event, int>();
            var events = await repo.GetAllAsync();

            var filtered = events
                .Where(e =>
                    (!string.IsNullOrEmpty(e.Titel) && e.Titel.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(e.Description) && e.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                .Select(e => new EventDTo
                {
                    Id = e.Id,
                    Titel = e.Titel,
                    Description = e.Description,
                    Location = e.Location,
                    Category = e.Category,
                    Date = e.Date,
                    Price = e.Price,
                    PictureUrl = e.PictureUrl
                });

            return filtered;
        }


        public async Task UpdateEventAsync(int id, EventDTo eventDto)
        {
            var repo = _unitOfWork.GetRepository<Event, int>();
            var existing = await repo.GetByIdAsync(id);

            if (existing == null)
                throw new Exception("Event not found");

            existing.Titel = eventDto.Titel;
            existing.Description = eventDto.Description;
            existing.Location = eventDto.Location;
            existing.Category = eventDto.Category;
            existing.Date = eventDto.Date;
            existing.Price = eventDto.Price;
            existing.PictureUrl = eventDto.PictureUrl;

            repo.Update(existing);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
