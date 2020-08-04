using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Anonymized.Assessment.Data.Helpers
{
    class GuidSringValueGenerator : ValueGenerator<string>
    {
        public override string Next(EntityEntry entry) =>
            Guid.NewGuid().ToString();

        public override bool GeneratesTemporaryValues { get; } = false;
    }
}