using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyWorkout.Core.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public Program Program { get; set; }
        public DateTime Date { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }

        public Workout()
        {
            WorkoutExercises = new Collection<WorkoutExercise>();
        }
    }
}
