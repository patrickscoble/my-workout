using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core;
using MyWorkout.Core.Models;
using MyWorkout.Core.Services;

namespace MyWorkout.Services
{
    public class WorkoutExerciseService : IWorkoutExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutExerciseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutExercise> GetWithWorkoutAndProgramExerciseById(int id)
        {
            return await _unitOfWork.WorkoutExercises
                .GetWithWorkoutAndProgramExerciseByIdAsync(id);
        }

        public async Task<IEnumerable<WorkoutExercise>> GetAllWithWorkoutAndProgramExerciseByWorkoutId(int workoutId)
        {
            return await _unitOfWork.WorkoutExercises
                .GetAllWithWorkoutAndProgramExerciseByWorkoutIdAsync(workoutId);
        }

        public async Task<IEnumerable<WorkoutExercise>> GetAllWithWorkoutAndProgramExerciseByProgramExerciseId(int programExerciseId)
        {
            return await _unitOfWork.WorkoutExercises
                .GetAllWithWorkoutAndProgramExerciseByProgramExerciseIdAsync(programExerciseId);
        }

        public async Task<WorkoutExercise> CreateWorkoutExercise(WorkoutExercise newWorkout)
        {
            await _unitOfWork.WorkoutExercises.AddAsync(newWorkout);
            await _unitOfWork.CommitAsync();
            return newWorkout;
        }

        public async Task UpdateWorkoutExercise(WorkoutExercise workoutExerciseToBeUpdated, WorkoutExercise workoutExercise)
        {
            workoutExerciseToBeUpdated.WorkoutId = workoutExercise.WorkoutId;
            workoutExerciseToBeUpdated.ProgramExerciseId = workoutExercise.ProgramExerciseId;
            workoutExerciseToBeUpdated.Weight = workoutExercise.Weight;
            workoutExerciseToBeUpdated.MaxedOut = workoutExercise.MaxedOut;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteWorkoutExercise(WorkoutExercise workoutExercise)
        {
            _unitOfWork.WorkoutExercises.Remove(workoutExercise);
            await _unitOfWork.CommitAsync();
        }
    }
}
