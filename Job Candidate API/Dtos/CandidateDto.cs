using System.ComponentModel.DataAnnotations;

namespace Job_Candidate_API.Dtos
{
    public class CandidateDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string CallTimeInterval { get; set; }

        public string LinkedInProfileUrl { get; set; }

        public string GitHubProfileUrl { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        public string Comment { get; set; }
    }
}
