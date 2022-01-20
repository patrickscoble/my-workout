using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MyWorkout.Api.Resources;
using MyWorkout.Api.Validators;
using MyWorkout.Core.Models;
using MyWorkout.Core.Services;

namespace MyWorkoutExercise.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutExerciseController : ControllerBase
    {
        private readonly IWorkoutExerciseService _workoutExerciseService;
        private readonly IMapper _mapper;

        public WorkoutExerciseController(IWorkoutExerciseService workoutExerciseService, IMapper mapper)
        {
            _workoutExerciseService = workoutExerciseService;
            _mapper = mapper;
        }

        [HttpGet("Workout/{workoutId}")]
        public async Task<ActionResult<IEnumerable<ProgramExerciseResource>>> GetAllByWorkoutId(int workoutId)
        {
            IEnumerable<WorkoutExercise> workoutExercises = await _workoutExerciseService.GetAllWithWorkoutAndProgramExerciseByWorkoutId(workoutId);
            IEnumerable<WorkoutExerciseResource> workoutExerciseResourcses = _mapper.Map<IEnumerable<WorkoutExercise>, IEnumerable<WorkoutExerciseResource>>(workoutExercises);

            return Ok(workoutExerciseResourcses);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkoutExerciseResource>> UpdateWorkoutExercise(int id, [FromBody] SaveWorkoutExerciseResource saveWorkoutExerciseResource)
        {
            if (id == 0)
                return BadRequest();

            SaveWorkoutExerciseResourceValidator validator = new SaveWorkoutExerciseResourceValidator();
            ValidationResult validationResult = await validator.ValidateAsync(saveWorkoutExerciseResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            WorkoutExercise workoutExerciseToBeUpdated = await _workoutExerciseService.GetWithWorkoutAndProgramExerciseById(id);

            if (workoutExerciseToBeUpdated == null)
                return NotFound();

            WorkoutExercise workoutExercise = _mapper.Map<SaveWorkoutExerciseResource, WorkoutExercise>(saveWorkoutExerciseResource);

            await _workoutExerciseService.UpdateWorkoutExercise(workoutExerciseToBeUpdated, workoutExercise);

            WorkoutExercise updatedWorkoutExercise = await _workoutExerciseService.GetWithWorkoutAndProgramExerciseById(id);
            WorkoutExerciseResource updatedWorkoutExerciseResource = _mapper.Map<WorkoutExercise, WorkoutExerciseResource>(updatedWorkoutExercise);

            return Ok(updatedWorkoutExerciseResource);
        }
    }
}
