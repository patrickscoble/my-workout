using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core.Models;

namespace MyWorkout.Core.Repositories
{
    public interface IWorkoutExerciseRepository : IRepository<WorkoutExercise>
    {
        Task<WorkoutExercise> GetWithWorkoutAndProgramExerciseByIdAsync(int id);
        Task<IEnumerable<WorkoutExercise>> GetAllWithWorkoutAndProgramExerciseByWorkoutIdAsync(int workoutId);
        Task<IEnumerable<WorkoutExercise>> GetAllWithWorkoutAndProgramExerciseByProgramExerciseIdAsync(int programExerciseId);
    }
}
