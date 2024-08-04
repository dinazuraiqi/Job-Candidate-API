using Job_Candidate_API.Data;
using Job_Candidate_API.Dtos;
using Job_Candidate_API.Middleware;
using Job_Candidate_API.Repositories;
using Job_Candidate_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Job_Candidate_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CandidateContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
               sqlServerOptionsAction: sqlOptions =>
               {
                sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5, // Number of retry attempts
                maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
                errorNumbersToAdd: null); // Add specific SQL error numbers if needed
                }));

            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICandidateService, CandidateService>();

            services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new ValidationError
                        {
                            Name = e.Key,
                            Message = e.Value.Errors.First().ErrorMessage
                        }).ToArray();

                    var errorResponse = new ValidationErrorResponse { Errors = errors };
                    return new BadRequestObjectResult(errorResponse);
                };
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.TypeInfoResolver = AppJsonContext.Default;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
