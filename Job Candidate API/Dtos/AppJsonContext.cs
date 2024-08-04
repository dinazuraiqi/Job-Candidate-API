using System.Text.Json.Serialization;

namespace Job_Candidate_API.Dtos
{
    [JsonSerializable(typeof(CandidateDto))]
    [JsonSerializable(typeof(ErrorResponse))]
    [JsonSerializable(typeof(ValidationError))]
    [JsonSerializable(typeof(ValidationErrorResponse))]
    public partial class AppJsonContext : JsonSerializerContext
    {
    }
}
