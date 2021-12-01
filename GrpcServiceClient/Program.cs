using Grpc.Net.Client;
using GrpcServiceClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7172");
var client = new HelloCommunityService.HelloCommunityServiceClient(channel);
var reply = await client.SayHelloAsync(new RequestMessage { Name = "Community member" });
Console.WriteLine("Greeting from Server: " + reply.Message);

reply = await client.SayByeAsync(new RequestMessage { Name = "Community member" });
Console.WriteLine("Greeting from : " + reply.Message);

Console.WriteLine("Press any key to exit...");
Console.ReadKey();