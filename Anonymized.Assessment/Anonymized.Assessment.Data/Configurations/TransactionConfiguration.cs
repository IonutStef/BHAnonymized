using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anonymized.Assessment.Data.Configurations
{
    class TransactionConfiguration : IEntityTypeConfiguration<TransactionDto>
    {
        public void Configure(EntityTypeBuilder<TransactionDto> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired()
                .HasValueGenerator<GuidSringValueGenerator>();

            builder.Property(a => a.AccountId)
                .IsRequired();

            builder.Property(a => a.Amount)
                .IsRequired();
        }
    }
}