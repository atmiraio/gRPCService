using Grpc.Core;

namespace GrpcServiceServer.Services
{
    public class HelloCommunityService : GrpcServiceServer.HelloCommunityService.HelloCommunityServiceBase
    {
        private readonly ILogger<HelloCommunityService> _logger;
        public HelloCommunityService(ILogger<HelloCommunityService> logger)
        {
            _logger = logger;
        }

        public override Task<RequestReply> SayHello(RequestMessage request, ServerCallContext context)
        {
            return Task.FromResult(new RequestReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<RequestReply> SayBye(RequestMessage request, ServerCallContext context)
        {
            return Task.FromResult(new RequestReply
            {
                Message = "Bye-bye " + request.Name
            });
        }
    }
}