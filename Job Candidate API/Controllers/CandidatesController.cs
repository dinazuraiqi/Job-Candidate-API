using Job_Candidate_API.Dtos;
using Job_Candidate_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Job_Candidate_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("AddOrUpdateCandidat")]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDto candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _candidateService.AddOrUpdateCandidate(candidate);
            return Ok();
        }
    }
}
