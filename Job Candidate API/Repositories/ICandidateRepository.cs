using Job_Candidate_API.Models;

namespace Job_Candidate_API.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetCandidateByEmailAsync(string email);
        Task AddCandidateAsync(Candidate candidate);
        Task UpdateCandidateAsync(Candidate candidate);
        Task SaveChangesAsync();
    }
}
