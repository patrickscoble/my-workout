using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Core.Models;
using MyWorkout.Core.Repositories;

namespace MyWorkout.Data.Repositories
{
    public class ProgramExerciseRepository : Repository<ProgramExercise>, IProgramExerciseRepository
    {
        public ProgramExerciseRepository(MyWorkoutDbContext context) 
            : base(context)
        { }

        public async Task<ProgramExercise> GetWithProgramByIdAsync(int id)
        {
            return await MyWorkoutDbContext.ProgramExercises
                .Include(pe => pe.Program)
                .SingleOrDefaultAsync(pe => pe.Id == id);
        }

        public async Task<IEnumerable<ProgramExercise>> GetAllWithProgramByProgramIdAsync(int programId)
        {
            return await MyWorkoutDbContext.ProgramExercises
                .Include(pe => pe.Program)
                .Where(pe => pe.ProgramId == programId)
                .ToListAsync();
        }

        private MyWorkoutDbContext MyWorkoutDbContext
        {
            get { return Context as MyWorkoutDbContext; }
        }
    }
}
