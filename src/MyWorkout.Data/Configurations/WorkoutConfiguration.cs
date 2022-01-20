using MyWorkout.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyWorkout.Data.Configurations
{
    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder
                .HasKey(w => w.Id);

            builder
                .Property(w => w.Id)
                .UseIdentityColumn();

            builder
                .Property(w => w.Date)
                .IsRequired();

            builder
                .HasOne(w => w.Program)
                .WithMany(p => p.Workouts)
                .HasForeignKey(w => w.ProgramId);

            builder
                .ToTable("Workout");
        }
    }
}
