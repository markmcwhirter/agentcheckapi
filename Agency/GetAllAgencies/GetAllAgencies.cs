
namespace AgentCheckApi.Agency;

using FastEndpoints;

public class GetAllAgencies : EndpointWithoutRequest<GetAllAgenciesResponse>
{
    public override void Configure()
    {
        Get("/api/agencies");
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(new()
        {
            Name = "Test Agency",
            TestAgency = true,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now
        });
    }
}
