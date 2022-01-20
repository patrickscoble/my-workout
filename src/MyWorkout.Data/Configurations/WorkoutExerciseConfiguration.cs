using MyWorkout.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyWorkout.Data.Configurations
{
    public class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
        {
            builder
                .HasKey(we => we.Id);

            builder
                .Property(we => we.Id)
                .UseIdentityColumn();

            builder
                .Property(we => we.Weight)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .Property(we => we.MaxedOut)
                .IsRequired();

            builder
                .HasOne(we => we.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutId)
                .OnDelete(DeleteBehavior.Restrict); // To prevent cycles or multiple cascade paths.

            builder
                .HasOne(we => we.ProgramExercise)
                .WithMany(pe => pe.WorkoutExercises)
                .HasForeignKey(we => we.ProgramExerciseId);

            builder
                .ToTable("WorkoutExercise");
        }
    }
}
