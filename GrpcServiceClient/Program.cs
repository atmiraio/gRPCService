using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceServer;

// The port number must match the port of the gRPC server.
var serverBase = "https://localhost:7172";
var token = await Authenticate("Alice");


//var channel = GrpcChannel.ForAddress(serverBase);
var channel = GrpcChannel.ForAddress(serverBase, new GrpcChannelOptions
{
    Credentials = ChannelCredentials.Create(new SslCredentials(), CallCredentials.FromInterceptor((context, metadata) =>
    {
        metadata.Add("Authorization", $"Bearer {token}");
        return Task.CompletedTask;
    }))
});
var client = new HelloCommunityService.HelloCommunityServiceClient(channel);


try
{
    var reply = await client.SayHelloAsync(new RequestMessage { Name = "Community member" });
    Console.WriteLine("Greeting from Server: " + reply.Message);
}
catch (Exception ex)
{
    Console.WriteLine($"Error trying achieve service {nameof(client.SayHelloAsync)}: {ex.Message}");
}

try
{
    var reply = await client.SayByeAsync(new RequestMessage { Name = "Community member" });
    Console.WriteLine("Greeting from : " + reply.Message);

}
catch (Exception ex)
{
    Console.WriteLine($"Error trying achieve service {nameof(client.SayByeAsync)}: {ex.Message}");
}


Console.WriteLine("Press any key to exit...");
Console.ReadKey();


async Task<string> Authenticate(string name)
{
    using var client = new HttpClient();
    var request = new HttpRequestMessage
    {
        RequestUri = new Uri($"{serverBase}/generateJwtToken?name={name}"),
        Method = HttpMethod.Get,
        Version = new Version(2, 0),
    };

    var response = await client.SendAsync(request);
    response.EnsureSuccessStatusCode();

    return await response.Content.ReadAsStringAsync();
}