using Job_Candidate_API.Dtos;
using Job_Candidate_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Job_Candidate_API.Controllers.Tests
{
    [TestClass()]
    public class CandidatesControllerTests
    {
        private readonly CandidatesController _controller;
        private readonly Mock<ICandidateService> _mockCandidateService;

        public CandidatesControllerTests()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _controller = new CandidatesController(_mockCandidateService.Object);
        }

        [Fact]
        public async Task UpsertCandidate_ShouldReturnOk_WhenCandidateIsValid()
        {
            var candidateDto = new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Sample comment"
            };

            var result = await _controller.AddOrUpdateCandidate(candidateDto);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpsertCandidate_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            var candidateDto = new CandidateDto(); // Invalid candidate
            _controller.ModelState.AddModelError("Email", "Email is required");

            var result = await _controller.AddOrUpdateCandidate(candidateDto);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = actionResult.Value as SerializableError;
            Assert.NotNull(badRequestResult);
            Assert.True(badRequestResult.ContainsKey("Email"));
        }

        [Fact]
        public async Task UpsertCandidate_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            var candidateDto = new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Sample comment"
            };

            _mockCandidateService
                .Setup(service => service.AddOrUpdateCandidate(It.IsAny<CandidateDto>()))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.AddOrUpdateCandidate(candidateDto);

            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, actionResult.StatusCode);
            Assert.Equal("Database error", actionResult.Value);
        }
    }
}