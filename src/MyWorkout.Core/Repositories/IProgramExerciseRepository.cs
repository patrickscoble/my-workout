using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core.Models;

namespace MyWorkout.Core.Repositories
{
    public interface IProgramExerciseRepository : IRepository<ProgramExercise>
    {
        Task<ProgramExercise> GetWithProgramByIdAsync(int id);
        Task<IEnumerable<ProgramExercise>> GetAllWithProgramByProgramIdAsync(int programId);
    }
}
