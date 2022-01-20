namespace MyWorkout.Api.Resources
{
    public class WorkoutExerciseResource
    {
        public int Id { get; set; }
        public WorkoutResource Workout { get; set; }
        public ProgramExerciseResource ProgramExercise { get; set; }
        public string Weight { get; set; }
        public bool MaxedOut { get; set; }
    }
}
