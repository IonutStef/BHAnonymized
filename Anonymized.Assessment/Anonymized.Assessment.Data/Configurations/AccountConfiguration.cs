using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anonymized.Assessment.Data.Configurations
{
    class AccountConfiguration : IEntityTypeConfiguration<AccountDto>
    {
        public void Configure(EntityTypeBuilder<AccountDto> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired()
                .HasValueGenerator<GuidSringValueGenerator>();

            builder.Property(a => a.CustomerId)
                .IsRequired();
        }
    }
}