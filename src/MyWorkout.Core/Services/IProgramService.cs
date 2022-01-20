using System.Collections.Generic;
using System.Threading.Tasks;
using MyWorkout.Core.Models;

namespace MyWorkout.Core.Services
{
    public interface IProgramService
    {
        Task<IEnumerable<Program>> GetAllWithProgramExercisesAndWorkouts();
        Task<Program> GetWithProgramExercisesAndWorkoutsById(int id);
        Task<Program> CreateProgram(Program newProgram);
        Task UpdateProgram(Program programToBeUpdated, Program program);
        Task DeleteProgram(Program program);
    }
}
