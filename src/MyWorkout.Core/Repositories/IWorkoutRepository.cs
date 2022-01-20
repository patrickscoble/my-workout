using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core.Models;

namespace MyWorkout.Core.Repositories
{
    public interface IWorkoutRepository : IRepository<Workout>
    {
        Task<Workout> GetWithProgramAndWorkoutExercisesByIdAsync(int id);
        Task<IEnumerable<Workout>> GetAllWithProgramAndWorkoutExercisesByProgramIdAsync(int programId);
    }
}
