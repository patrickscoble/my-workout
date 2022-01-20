using MyWorkout.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyWorkout.Data.Configurations
{
    public class ProgramExerciseConfiguration : IEntityTypeConfiguration<ProgramExercise>
    {
        public void Configure(EntityTypeBuilder<ProgramExercise> builder)
        {
            builder
                .HasKey(pe => pe.Id);

            builder
                .Property(pe => pe.Id)
                .UseIdentityColumn();

            builder
                .Property(pe => pe.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(pe => pe.Sets)
                .IsRequired();

            builder
                .Property(pe => pe.Repetitions)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .Property(pe => pe.RestPeriod)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .HasOne(pe => pe.Program)
                .WithMany(p => p.ProgramExercises)
                .HasForeignKey(pe => pe.ProgramId);

            builder
                .ToTable("ProgramExercise");
        }
    }
}
