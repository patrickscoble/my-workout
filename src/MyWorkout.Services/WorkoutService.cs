using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core;
using MyWorkout.Core.Models;
using MyWorkout.Core.Services;

namespace MyWorkout.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkoutExerciseService _workoutExerciseService;

        public WorkoutService(IUnitOfWork unitOfWork, IWorkoutExerciseService workoutExerciseService)
        {
            _unitOfWork = unitOfWork;
            _workoutExerciseService = workoutExerciseService;
        }

        public async Task<Workout> GetWithProgramAndWorkoutExercisesById(int id)
        {
            return await _unitOfWork.Workouts
                .GetWithProgramAndWorkoutExercisesByIdAsync(id);
        }

        public async Task<IEnumerable<Workout>> GetAllWithProgramAndWorkoutExercisesByProgramId(int programId)
        {
            return await _unitOfWork.Workouts
                .GetAllWithProgramAndWorkoutExercisesByProgramIdAsync(programId);
        }

        public async Task<Workout> CreateWorkout(Workout newWorkout)
        {
            await _unitOfWork.Workouts.AddAsync(newWorkout);
            await _unitOfWork.CommitAsync();

            IEnumerable<ProgramExercise> programExercises = await _unitOfWork.ProgramExercises.GetAllWithProgramByProgramIdAsync(newWorkout.ProgramId);
            foreach (ProgramExercise programExercise in programExercises)
            {
                WorkoutExercise workoutExercise = new WorkoutExercise()
                {
                    WorkoutId = newWorkout.Id,
                    ProgramExerciseId = programExercise.Id,
                    Weight = string.Empty
                };

                await _workoutExerciseService.CreateWorkoutExercise(workoutExercise);
            }

            return newWorkout;
        }

        public async Task UpdateWorkout(Workout workoutToBeUpdated, Workout workout)
        {
            workoutToBeUpdated.ProgramId = workout.ProgramId;
            workoutToBeUpdated.Date = workout.Date;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteWorkout(Workout workout)
        {
            IEnumerable<WorkoutExercise> workoutExercises = await _workoutExerciseService.GetAllWithWorkoutAndProgramExerciseByWorkoutId(workout.Id);
            foreach (WorkoutExercise workoutExercise in workoutExercises)
            {
                await _workoutExerciseService.DeleteWorkoutExercise(workoutExercise);
            }

            _unitOfWork.Workouts.Remove(workout);
            await _unitOfWork.CommitAsync();
        }
    }
}
