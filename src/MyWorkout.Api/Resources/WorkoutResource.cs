using System;

namespace MyWorkout.Api.Resources
{
    public class WorkoutResource
    {
        public int Id { get; set; }
        public ProgramResource Program { get; set; }
        public DateTime Date { get; set; }
    }
}
