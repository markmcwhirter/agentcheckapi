using FastEndpoints;

namespace AgentCheckApi.Authentication;
sealed class Endpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/protected");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync("you are authorized!");
    }
}