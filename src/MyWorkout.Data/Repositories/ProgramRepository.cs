using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Core.Models;
using MyWorkout.Core.Repositories;

namespace MyWorkout.Data.Repositories
{
    public class ProgramRepository : Repository<Program>, IProgramRepository
    {
        public ProgramRepository(MyWorkoutDbContext context) 
            : base(context)
        { }

        public async Task<IEnumerable<Program>> GetAllWithProgramExercisesAndWorkoutsAsync()
        {
            return await MyWorkoutDbContext.Programs
                .Include(p => p.ProgramExercises)
                .Include(p => p.Workouts)
                .ToListAsync();
        }

        public async Task<Program> GetWithProgramExercisesAndWorkoutsByIdAsync(int id)
        {
            return await MyWorkoutDbContext.Programs
                .Include(p => p.ProgramExercises)
                .Include(p => p.Workouts)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        private MyWorkoutDbContext MyWorkoutDbContext
        {
            get { return Context as MyWorkoutDbContext; }
        }
    }
}
