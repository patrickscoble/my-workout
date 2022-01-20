namespace MyWorkout.Core.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public int ProgramExerciseId { get; set; }
        public ProgramExercise ProgramExercise { get; set; }
        public string Weight { get; set; }
        public bool MaxedOut { get; set; }
    }
}
