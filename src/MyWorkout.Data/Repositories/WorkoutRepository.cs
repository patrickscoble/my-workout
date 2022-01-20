using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Core.Models;
using MyWorkout.Core.Repositories;

namespace MyWorkout.Data.Repositories
{
    public class WorkoutRepository : Repository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(MyWorkoutDbContext context) 
            : base(context)
        { }

        public async Task<Workout> GetWithProgramAndWorkoutExercisesByIdAsync(int id)
        {
            return await MyWorkoutDbContext.Workouts
                .Include(w => w.Program)
                .Include(w => w.WorkoutExercises)
                .SingleOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<Workout>> GetAllWithProgramAndWorkoutExercisesByProgramIdAsync(int programId)
        {
            return await MyWorkoutDbContext.Workouts
                .Include(w => w.Program)
                .Include(w => w.WorkoutExercises)
                .Where(w => w.ProgramId == programId)
                .ToListAsync();
        }

        private MyWorkoutDbContext MyWorkoutDbContext
        {
            get { return Context as MyWorkoutDbContext; }
        }
    }
}
