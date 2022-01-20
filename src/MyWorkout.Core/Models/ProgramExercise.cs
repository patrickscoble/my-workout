using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyWorkout.Core.Models
{
    public class ProgramExercise
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public Program Program { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public string Repetitions { get; set; }
        public string RestPeriod { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }

        public ProgramExercise()
        {
            WorkoutExercises = new Collection<WorkoutExercise>();
        }
    }
}
