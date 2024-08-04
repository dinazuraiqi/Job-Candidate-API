using Job_Candidate_API.Dtos;

namespace Job_Candidate_API.Services
{
    public interface ICandidateService
    {
        Task AddOrUpdateCandidate(CandidateDto candidate);
    }
}
