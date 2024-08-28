namespace AgentCheckApi.Agency;

using FastEndpoints;

public class AddAgency : Endpoint<AddAgencyRequest,AddAgencyResponse>
{
    public override void Configure()
    {
        Post("/api/agency/add");
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddAgencyRequest r, CancellationToken ct)
    {
        await SendAsync(new AddAgencyResponse
        {
            Id = 1,
            Name = "Test Agency",
            TestAgency = true,
            DateCreated = DateTime.Now
        });
    }
}
