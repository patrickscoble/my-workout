using System.Threading.Tasks;
using MyWorkout.Core;
using MyWorkout.Core.Repositories;
using MyWorkout.Data;
using MyWorkout.Data.Repositories;

namespace MyWorkout.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyWorkoutDbContext _context;
        private ProgramRepository _programRepository;
        private ProgramExerciseRepository _programExerciseRepository;
        private WorkoutRepository _workoutRepository;
        private WorkoutExerciseRepository _workoutExerciseRepository;

        public UnitOfWork(MyWorkoutDbContext context)
        {
            this._context = context;
        }

        public IProgramRepository Programs => _programRepository = _programRepository ?? new ProgramRepository(_context);

        public IProgramExerciseRepository ProgramExercises => _programExerciseRepository = _programExerciseRepository ?? new ProgramExerciseRepository(_context);

        public IWorkoutRepository Workouts => _workoutRepository = _workoutRepository ?? new WorkoutRepository(_context);

        public IWorkoutExerciseRepository WorkoutExercises => _workoutExerciseRepository = _workoutExerciseRepository ?? new WorkoutExerciseRepository(_context);


        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
