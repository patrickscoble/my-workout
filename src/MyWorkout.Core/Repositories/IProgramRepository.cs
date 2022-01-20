using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core.Models;

namespace MyWorkout.Core.Repositories
{
    public interface IProgramRepository : IRepository<Program>
    {
        Task<IEnumerable<Program>> GetAllWithProgramExercisesAndWorkoutsAsync();
        Task<Program> GetWithProgramExercisesAndWorkoutsByIdAsync(int id);
    }
}
