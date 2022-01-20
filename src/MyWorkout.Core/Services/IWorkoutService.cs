using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core.Models;

namespace MyWorkout.Core.Services
{
    public interface IWorkoutService
    {
        Task<Workout> GetWithProgramAndWorkoutExercisesById(int id);
        Task<IEnumerable<Workout>> GetAllWithProgramAndWorkoutExercisesByProgramId(int programId);
        Task<Workout> CreateWorkout(Workout newWorkout);
        Task UpdateWorkout(Workout workoutToBeUpdated, Workout workout);
        Task DeleteWorkout(Workout workout);
    }
}
