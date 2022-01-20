using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyWorkout.Core.Models
{
    public class Program
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProgramExercise> ProgramExercises { get; set; }
        public ICollection<Workout> Workouts { get; set; }

        public Program()
        {
            ProgramExercises = new Collection<ProgramExercise>();
            Workouts = new Collection<Workout>();
        }
    }
}
