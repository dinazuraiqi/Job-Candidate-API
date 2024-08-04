using Job_Candidate_API.Dtos;
using Job_Candidate_API.Models;
using Job_Candidate_API.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Job_Candidate_API.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task AddOrUpdateCandidate(CandidateDto candidate)
        {
            var existingCandidate = await _candidateRepository.GetCandidateByEmailAsync(candidate.Email);
            if (existingCandidate != null)
            {
                await UpdateCandidate(candidate, existingCandidate);
            }
            else
            {
                await AddCandidate(candidate);
            }

            await _candidateRepository.SaveChangesAsync();
        }

        private async Task AddCandidate(CandidateDto candidate)
        {
            var newCandidate = new Candidate()
            {
                CandidateId = Guid.NewGuid(),
                Email = candidate.Email,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Phone = candidate.Phone,
                CallTimeInterval = candidate.CallTimeInterval,
                LinkedInProfileUrl = candidate.LinkedInProfileUrl,
                GitHubProfileUrl = candidate.GitHubProfileUrl,
                Comment = candidate.Comment
            };
            await _candidateRepository.AddCandidateAsync(newCandidate);
        }

        private async Task UpdateCandidate(CandidateDto candidate, Candidate existingCandidate)
        {
            existingCandidate.FirstName = candidate.FirstName;
            existingCandidate.LastName = candidate.LastName;
            existingCandidate.Phone = candidate.Phone;
            existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
            existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
            existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
            existingCandidate.Comment = candidate.Comment;

            await _candidateRepository.UpdateCandidateAsync(existingCandidate);
        }
    }
}
