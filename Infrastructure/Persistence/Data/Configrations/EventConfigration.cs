using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configrations
{
    public class EventConfigration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Titel)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.Description)
                   .HasMaxLength(500);

            builder.Property(e => e.Price)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.HasMany(e => e.Bookings)
                   .WithOne(b => b.Event)
                   .HasForeignKey(b => b.EventId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
