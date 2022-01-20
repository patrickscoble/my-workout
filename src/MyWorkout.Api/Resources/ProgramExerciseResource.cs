namespace MyWorkout.Api.Resources
{
    public class ProgramExerciseResource
    {
        public int Id { get; set; }
        public ProgramResource Program { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public string Repetitions { get; set; }
        public string RestPeriod { get; set; }
    }
}
