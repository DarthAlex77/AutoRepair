using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReactiveUI;

namespace AutoRepair.Behaviors
{
    public abstract class ReactiveObjectConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
            where TEntity : ReactiveObject
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Ignore(e => e.Changed);
            builder.Ignore(e => e.Changing);
            builder.Ignore(e => e.ThrownExceptions);
        }
    }
}