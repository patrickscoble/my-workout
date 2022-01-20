using Microsoft.EntityFrameworkCore;
using MyWorkout.Core.Models;
using MyWorkout.Data.Configurations;

namespace MyWorkout.Data
{
    public class MyWorkoutDbContext : DbContext
    {
        public DbSet<Program> Programs { get; set; }
        public DbSet<ProgramExercise> ProgramExercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        public MyWorkoutDbContext(DbContextOptions<MyWorkoutDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new ProgramConfiguration());

            builder
                .ApplyConfiguration(new ProgramExerciseConfiguration());

            builder
                .ApplyConfiguration(new WorkoutConfiguration());

            builder
                .ApplyConfiguration(new WorkoutExerciseConfiguration());
        }
    }
}
