using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MyWorkout.Api.Resources;
using MyWorkout.Api.Validators;
using MyWorkout.Core.Services;

using Models = MyWorkout.Core.Models;

namespace MyWorkout.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        private readonly IMapper _mapper;

        public ProgramController(IProgramService programService, IMapper mapper)
        {
            _programService = programService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ProgramResource>>> GetAll()
        {
            IEnumerable<Models.Program> programs = await _programService.GetAllWithProgramExercisesAndWorkouts();
            IEnumerable<ProgramResource> programResources = _mapper.Map<IEnumerable<Models.Program>, IEnumerable<ProgramResource>>(programs);

            return Ok(programResources);
        }

        [HttpPost("")]
        public async Task<ActionResult<ProgramResource>> CreateProgram([FromBody] SaveProgramResource saveProgramResource)
        {
            SaveProgramResourceValidator validator = new SaveProgramResourceValidator();
            ValidationResult validationResult = await validator.ValidateAsync(saveProgramResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            Models.Program programToCreate = _mapper.Map<SaveProgramResource, Models.Program>(saveProgramResource);

            Models.Program newProgram = await _programService.CreateProgram(programToCreate);

            ProgramResource programResource = _mapper.Map<Models.Program, ProgramResource>(newProgram);

            return Ok(programResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProgramResource>> UpdateProgram(int id, [FromBody] SaveProgramResource saveProgramResource)
        {
            if (id == 0)
                return BadRequest();

            SaveProgramResourceValidator validator = new SaveProgramResourceValidator();
            ValidationResult validationResult = await validator.ValidateAsync(saveProgramResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            Models.Program programToBeUpdated = await _programService.GetWithProgramExercisesAndWorkoutsById(id);

            if (programToBeUpdated == null)
                return NotFound();

            Models.Program program = _mapper.Map<SaveProgramResource, Models.Program>(saveProgramResource);

            await _programService.UpdateProgram(programToBeUpdated, program);

            Models.Program updatedProgram = await _programService.GetWithProgramExercisesAndWorkoutsById(id);
            ProgramResource updatedProgramResource = _mapper.Map<Models.Program, ProgramResource>(updatedProgram);

            return Ok(updatedProgramResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            if (id == 0)
                return BadRequest();

            Models.Program program = await _programService.GetWithProgramExercisesAndWorkoutsById(id);

            if (program == null)
                return NotFound();

            await _programService.DeleteProgram(program);

            return NoContent();
        }
    }
}
