using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core.Models;

namespace MyWorkout.Core.Services
{
    public interface IWorkoutExerciseService
    {
        Task<WorkoutExercise> GetWithWorkoutAndProgramExerciseById(int id);
        Task<IEnumerable<WorkoutExercise>> GetAllWithWorkoutAndProgramExerciseByWorkoutId(int workoutId);
        Task<IEnumerable<WorkoutExercise>> GetAllWithWorkoutAndProgramExerciseByProgramExerciseId(int programExerciseId);
        Task<WorkoutExercise> CreateWorkoutExercise(WorkoutExercise newWorkoutExercise);
        Task UpdateWorkoutExercise(WorkoutExercise workoutExerciseToBeUpdated, WorkoutExercise workoutExercise);
        Task DeleteWorkoutExercise(WorkoutExercise workoutExercise);
    }
}
