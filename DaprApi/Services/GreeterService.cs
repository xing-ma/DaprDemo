using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Threading.Tasks;

namespace DaprGrpc
{
    public class GreeterService : AppCallback.AppCallbackBase
    {
        public GreeterService()
        {
        }

        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            var response = new InvokeResponse();
            await Task.Delay(10);
            switch (request.Method.ToLower())
            {
                case "sayhello":
                    var input = request.Data.Unpack<HelloRequest>();
                    response.Data = Any.Pack(new HelloReply
                    {
                        Message = input.Name

                    });
                    break;
                default:
                    break;
            }

            return response;
        }
    }
}
