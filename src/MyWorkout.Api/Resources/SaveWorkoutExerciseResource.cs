namespace MyWorkout.Api.Resources
{
    public class SaveWorkoutExerciseResource
    {
        public int WorkoutId { get; set; }
        public int ProgramExerciseId { get; set; }
        public string Weight { get; set; }
        public bool MaxedOut { get; set; }
    }
}
