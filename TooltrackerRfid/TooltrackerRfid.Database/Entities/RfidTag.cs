using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smoerfugl.Database.Postgres.BaseEntities;

namespace TooltrackerRfid.Database.Entities
{
    public class RfidTag : IBaseEntity<Guid>
    {
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid Id { get; set; }
        public string Epc { get; set; }
        public string Rssi { get; set; }
        public int ReadCount { get; set; }
    }
    
    public class RfidTagConfiguration : IEntityTypeConfiguration<RfidTag>
    {
        public void Configure(EntityTypeBuilder<RfidTag> builder)
        {
            builder.HasKey(con => con.Id);
        }
    }
}