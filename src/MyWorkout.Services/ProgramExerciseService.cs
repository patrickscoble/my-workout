using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core;
using MyWorkout.Core.Models;
using MyWorkout.Core.Services;

namespace MyWorkout.Services
{
    public class ProgramExerciseService : IProgramExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkoutExerciseService _workoutExerciseService;

        public ProgramExerciseService(IUnitOfWork unitOfWork, IWorkoutExerciseService workoutExerciseService)
        {
            _unitOfWork = unitOfWork;
            _workoutExerciseService = workoutExerciseService;
        }

        public async Task<ProgramExercise> GetWithProgramById(int id)
        {
            return await _unitOfWork.ProgramExercises
                .GetWithProgramByIdAsync(id);
        }

        public async Task<IEnumerable<ProgramExercise>> GetAllWithProgramByProgramId(int programId)
        {
            return await _unitOfWork.ProgramExercises
                .GetAllWithProgramByProgramIdAsync(programId);
        }

        public async Task<ProgramExercise> CreateProgramExercise(ProgramExercise newProgramExercise)
        {
            await _unitOfWork.ProgramExercises.AddAsync(newProgramExercise);
            await _unitOfWork.CommitAsync();

            IEnumerable<Workout> workouts = await _unitOfWork.Workouts.GetAllWithProgramAndWorkoutExercisesByProgramIdAsync(newProgramExercise.ProgramId);
            foreach (Workout workout in workouts)
            {
                WorkoutExercise workoutExercise = new WorkoutExercise()
                {
                    WorkoutId = workout.Id,
                    ProgramExerciseId = newProgramExercise.Id,
                    Weight = string.Empty
                };

                await _workoutExerciseService.CreateWorkoutExercise(workoutExercise);
            }

            return newProgramExercise;
        }

        public async Task UpdateProgramExercise(ProgramExercise programExerciseToBeUpdated, ProgramExercise programExercise)
        {
            programExerciseToBeUpdated.ProgramId = programExercise.ProgramId;
            programExerciseToBeUpdated.Name = programExercise.Name;
            programExerciseToBeUpdated.Sets = programExercise.Sets;
            programExerciseToBeUpdated.Repetitions = programExercise.Repetitions;
            programExerciseToBeUpdated.RestPeriod = programExercise.RestPeriod;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteProgramExercise(ProgramExercise programExercise)
        {
            IEnumerable<WorkoutExercise> workoutExercises = await _workoutExerciseService.GetAllWithWorkoutAndProgramExerciseByProgramExerciseId(programExercise.Id);
            foreach (WorkoutExercise workoutExercise in workoutExercises)
            {
                await _workoutExerciseService.DeleteWorkoutExercise(workoutExercise);
            }

            _unitOfWork.ProgramExercises.Remove(programExercise);
            await _unitOfWork.CommitAsync();
        }
    }
}
