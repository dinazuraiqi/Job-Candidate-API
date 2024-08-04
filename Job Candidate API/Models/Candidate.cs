using System.ComponentModel.DataAnnotations;

namespace Job_Candidate_API.Models
{
    public class Candidate
    {
        [Key]
        public Guid CandidateId { get; set; }  

        [Required]
        [EmailAddress]    
        public string Email { get; set; }  

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string CallTimeInterval { get; set; }

        public string LinkedInProfileUrl { get; set; }

        public string GitHubProfileUrl { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
