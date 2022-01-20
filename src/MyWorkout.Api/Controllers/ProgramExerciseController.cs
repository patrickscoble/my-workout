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
    public class ProgramExerciseController : ControllerBase
    {
        private readonly IProgramExerciseService _programExerciseService;
        private readonly IMapper _mapper;

        public ProgramExerciseController(IProgramExerciseService programExerciseService, IMapper mapper)
        {
            _programExerciseService = programExerciseService;
            _mapper = mapper;
        }

        [HttpGet("Program/{programId}")]
        public async Task<ActionResult<IEnumerable<ProgramExerciseResource>>> GetAllByProgramId(int programId)
        {
            IEnumerable<ProgramExercise> programExercises = await _programExerciseService.GetAllWithProgramByProgramId(programId);
            IEnumerable<ProgramExerciseResource> programExerciseResourcses = _mapper.Map<IEnumerable<ProgramExercise>, IEnumerable<ProgramExerciseResource>>(programExercises);

            return Ok(programExerciseResourcses);
        }

        [HttpPost("")]
        public async Task<ActionResult<ProgramExerciseResource>> CreateProgramExercise([FromBody] SaveProgramExerciseResource saveProgramExerciseResource)
        {
            SaveProgramExerciseResourceValidator validator = new SaveProgramExerciseResourceValidator();
            ValidationResult validationResult = await validator.ValidateAsync(saveProgramExerciseResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            ProgramExercise programExerciseToCreate = _mapper.Map<SaveProgramExerciseResource, ProgramExercise>(saveProgramExerciseResource);

            ProgramExercise newProgramExercise = await _programExerciseService.CreateProgramExercise(programExerciseToCreate);

            ProgramExerciseResource programExerciseResource = _mapper.Map<ProgramExercise, ProgramExerciseResource>(newProgramExercise);

            return Ok(programExerciseResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProgramExerciseResource>> UpdateProgramExercise(int id, [FromBody] SaveProgramExerciseResource saveProgramExerciseResource)
        {
            if (id == 0)
                return BadRequest();

            SaveProgramExerciseResourceValidator validator = new SaveProgramExerciseResourceValidator();
            ValidationResult validationResult = await validator.ValidateAsync(saveProgramExerciseResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            ProgramExercise programExerciseToBeUpdated = await _programExerciseService.GetWithProgramById(id);

            if (programExerciseToBeUpdated == null)
                return NotFound();

            ProgramExercise programExercise = _mapper.Map<SaveProgramExerciseResource, ProgramExercise>(saveProgramExerciseResource);

            await _programExerciseService.UpdateProgramExercise(programExerciseToBeUpdated, programExercise);

            ProgramExercise updatedProgramExercise = await _programExerciseService.GetWithProgramById(id);
            ProgramExerciseResource updatedProgramExerciseResource = _mapper.Map<ProgramExercise, ProgramExerciseResource>(updatedProgramExercise);

            return Ok(updatedProgramExerciseResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramExercise(int id)
        {
            if (id == 0)
                return BadRequest();

            ProgramExercise programExercise = await _programExerciseService.GetWithProgramById(id);

            if (programExercise == null)
                return NotFound();

            await _programExerciseService.DeleteProgramExercise(programExercise);

            return NoContent();
        }
    }
}
