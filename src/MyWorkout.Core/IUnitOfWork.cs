using System;
using System.Threading.Tasks;
using MyWorkout.Core.Repositories;

namespace MyWorkout.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProgramRepository Programs { get; }
        IProgramExerciseRepository ProgramExercises { get; }
        IWorkoutRepository Workouts { get; }
        IWorkoutExerciseRepository WorkoutExercises { get; }
        Task<int> CommitAsync();
    }
}
