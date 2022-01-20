using AutoMapper;
using MyWorkout.Api.Resources;
using MyWorkout.Core.Models;

using Models = MyWorkout.Core.Models;

namespace MyWorkout.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Models.Program, ProgramResource>();
            CreateMap<ProgramExercise, ProgramExerciseResource>();
            CreateMap<Workout, WorkoutResource>();
            CreateMap<WorkoutExercise, WorkoutExerciseResource>();

            // Resource to Domain
            CreateMap<ProgramResource, Models.Program>();
            CreateMap<ProgramExerciseResource, ProgramExercise>();
            CreateMap<WorkoutResource, Workout>();
            CreateMap<WorkoutExerciseResource, WorkoutExercise>();

            CreateMap<SaveProgramResource, Models.Program>();
            CreateMap<SaveProgramExerciseResource, ProgramExercise>();
            CreateMap<SaveWorkoutResource, Workout>();
            CreateMap<SaveWorkoutExerciseResource, WorkoutExercise>();
        }
    }
}
