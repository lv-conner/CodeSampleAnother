using System;
using System.Net.Http;
using System.Threading.Tasks;
using CodeSampleGrpc;
using Google.Protobuf;
using Grpc.Net.Client;
using System.Linq;
using Grpc.Core;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await UserRpcServiceTest();
            await ProjectTest();
            var httpClient = new HttpClient();
            // The port number(5001) must match the port of the gRPC server.
            httpClient.BaseAddress = new Uri("https://localhost:5001");
            var client = GrpcClient.Create<Greeter.GreeterClient>(httpClient);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            var versionReply = await client.GetVersionAsync(new VersionRequest() { Version = 10 });

            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("VersionReply: " + versionReply.Message);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        static async Task ChannelCall()
        {
            var channel = new Channel("localhost:50051", ChannelCredentials.Insecure);
        }
        static async Task ProjectTest()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:5001");
            var client = GrpcClient.Create<ProjectRpcService.ProjectRpcServiceClient>(httpClient);
            var reply = await client.GetProjectNameAsync(new ProjectRequest()
            {
                ProjectId = 1
            });
            Console.WriteLine(string.Format("Project Name is \t{0}", reply.ProjectName));
            Console.ReadKey();
        }

        static async Task UserRpcServiceTest()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:5001");
            var client = GrpcClient.Create<UserRpcService.UserRpcServiceClient>(httpClient);
            var reply = await client.GetUserNameAsync(new UserNameRequest()
            {
                UserId = "0001"
            });
            Console.WriteLine(string.Format("Project Name is \t{0}", reply.UserName));
            var image = reply.Images.ToList().First().ToStringUtf8();
            var bodyReply = await client.GetUserBodyAsync(new UserBodyRequest()
            {
                Id = "1"
            }); 
            Console.ReadKey();
        }
    }
}