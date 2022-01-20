using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Core.Models;
using MyWorkout.Core.Repositories;

namespace MyWorkout.Data.Repositories
{
    public class WorkoutExerciseRepository : Repository<WorkoutExercise>, IWorkoutExerciseRepository
    {
        public WorkoutExerciseRepository(MyWorkoutDbContext context) 
            : base(context)
        { }

        public async Task<WorkoutExercise> GetWithWorkoutAndProgramExerciseByIdAsync(int id)
        {
            return await MyWorkoutDbContext.WorkoutExercises
                .Include(we => we.Workout)
                .ThenInclude(w => w.Program)
                .Include(we => we.ProgramExercise)
                .ThenInclude(pe => pe.Program)
                .SingleOrDefaultAsync(we => we.Id == id);
        }

        public async Task<IEnumerable<WorkoutExercise>> GetAllWithWorkoutAndProgramExerciseByWorkoutIdAsync(int workoutId)
        {
            return await MyWorkoutDbContext.WorkoutExercises
                .Include(we => we.Workout)
                .ThenInclude(w => w.Program)
                .Include(we => we.ProgramExercise)
                .ThenInclude(pe => pe.Program)
                .Where(we => we.WorkoutId == workoutId)
                .ToListAsync();
        }

        public async Task<IEnumerable<WorkoutExercise>> GetAllWithWorkoutAndProgramExerciseByProgramExerciseIdAsync(int programExerciseId)
        {
            return await MyWorkoutDbContext.WorkoutExercises
                .Include(we => we.Workout)
                .ThenInclude(w => w.Program)
                .Include(we => we.ProgramExercise)
                .ThenInclude(pe => pe.Program)
                .Where(we => we.ProgramExerciseId == programExerciseId)
                .ToListAsync();
        }

        private MyWorkoutDbContext MyWorkoutDbContext
        {
            get { return Context as MyWorkoutDbContext; }
        }
    }
}
