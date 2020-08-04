using Anonymized.Assessment.Data.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anonymized.Assessment.Data.Configurations
{
    class CustomerConfiguration : IEntityTypeConfiguration<CustomerDto>
    {
        public void Configure(EntityTypeBuilder<CustomerDto> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired();
        }
    }
}