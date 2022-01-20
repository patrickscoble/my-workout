using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MyWorkout.Api.Resources;
using MyWorkout.Api.Validators;
using MyWorkout.Core.Models;
using MyWorkout.Core.Services;

namespace MyWorkout.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IMapper _mapper;

        public WorkoutController(IWorkoutService workoutService, IMapper mapper)
        {
            _workoutService = workoutService;
            _mapper = mapper;
        }

        [HttpGet("Program/{programId}")]
        public async Task<ActionResult<IEnumerable<ProgramExerciseResource>>> GetAllByProgramId(int programId)
        {
            IEnumerable<Workout> workouts = await _workoutService.GetAllWithProgramAndWorkoutExercisesByProgramId(programId);
            IEnumerable<WorkoutResource> workoutResourcses = _mapper.Map<IEnumerable<Workout>, IEnumerable<WorkoutResource>>(workouts);

            return Ok(workoutResourcses);
        }

        [HttpPost("")]
        public async Task<ActionResult<WorkoutResource>> CreateWorkout([FromBody] SaveWorkoutResource saveWorkoutResource)
        {
            SaveWorkoutResourceValidator validator = new SaveWorkoutResourceValidator();
            ValidationResult validationResult = await validator.ValidateAsync(saveWorkoutResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            Workout workoutToCreate = _mapper.Map<SaveWorkoutResource, Workout>(saveWorkoutResource);

            Workout newWorkout = await _workoutService.CreateWorkout(workoutToCreate);

            WorkoutResource workoutResource = _mapper.Map<Workout, WorkoutResource>(newWorkout);

            return Ok(workoutResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkoutResource>> UpdateWorkout(int id, [FromBody] SaveWorkoutResource saveWorkoutResource)
        {
            if (id == 0)
                return BadRequest();

            SaveWorkoutResourceValidator validator = new SaveWorkoutResourceValidator();
            ValidationResult validationResult = await validator.ValidateAsync(saveWorkoutResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            Workout workoutToBeUpdated = await _workoutService.GetWithProgramAndWorkoutExercisesById(id);

            if (workoutToBeUpdated == null)
                return NotFound();

            Workout workout = _mapper.Map<SaveWorkoutResource, Workout>(saveWorkoutResource);

            await _workoutService.UpdateWorkout(workoutToBeUpdated, workout);

            Workout updatedWorkout = await _workoutService.GetWithProgramAndWorkoutExercisesById(id);
            WorkoutResource updatedWorkoutResource = _mapper.Map<Workout, WorkoutResource>(updatedWorkout);

            return Ok(updatedWorkoutResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            if (id == 0)
                return BadRequest();

            Workout workout = await _workoutService.GetWithProgramAndWorkoutExercisesById(id);

            if (workout == null)
                return NotFound();

            await _workoutService.DeleteWorkout(workout);

            return NoContent();
        }
    }
}
