using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core.Models;

namespace MyWorkout.Core.Services
{
    public interface IProgramExerciseService
    {
        Task<ProgramExercise> GetWithProgramById(int id);
        Task<IEnumerable<ProgramExercise>> GetAllWithProgramByProgramId(int programId);
        Task<ProgramExercise> CreateProgramExercise(ProgramExercise newProgramExercise);
        Task UpdateProgramExercise(ProgramExercise programExerciseToBeUpdated, ProgramExercise programExercise);
        Task DeleteProgramExercise(ProgramExercise programExercise);
    }
}
