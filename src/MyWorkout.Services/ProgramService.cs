using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core;
using MyWorkout.Core.Models;
using MyWorkout.Core.Services;

namespace MyWorkout.Services
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramExerciseService _programExerciseService;
        private readonly IWorkoutService _workoutService;

        public ProgramService(IUnitOfWork unitOfWork, IProgramExerciseService programExerciseService, IWorkoutService workoutService)
        {
            _unitOfWork = unitOfWork;
            _programExerciseService = programExerciseService;
            _workoutService = workoutService;
        }

        public async Task<IEnumerable<Program>> GetAllWithProgramExercisesAndWorkouts()
        {
            return await _unitOfWork.Programs
                .GetAllWithProgramExercisesAndWorkoutsAsync();
        }

        public async Task<Program> GetWithProgramExercisesAndWorkoutsById(int id)
        {
            return await _unitOfWork.Programs
                .GetWithProgramExercisesAndWorkoutsByIdAsync(id);
        }

        public async Task<Program> CreateProgram(Program newProgram)
        {
            await _unitOfWork.Programs.AddAsync(newProgram);
            await _unitOfWork.CommitAsync();
            return newProgram;
        }

        public async Task UpdateProgram(Program programToBeUpdated, Program program)
        {
            programToBeUpdated.Name = program.Name;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteProgram(Program program)
        {
            IEnumerable<Workout> workouts = await _workoutService.GetAllWithProgramAndWorkoutExercisesByProgramId(program.Id);
            foreach (Workout workout in workouts)
            {
                await _workoutService.DeleteWorkout(workout);
            }

            IEnumerable<ProgramExercise> programExercises = await _programExerciseService.GetAllWithProgramByProgramId(program.Id);
            foreach (ProgramExercise programExercise in programExercises)
            {
                await _programExerciseService.DeleteProgramExercise(programExercise);
            }

            _unitOfWork.Programs.Remove(program);
            await _unitOfWork.CommitAsync();
        }
    }
}
