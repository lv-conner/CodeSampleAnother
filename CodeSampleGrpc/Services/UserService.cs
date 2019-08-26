using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;

namespace CodeSampleGrpc
{
    public class UserService:UserRpcService.UserRpcServiceBase
    {
        public override Task<UserNameResponse> GetUserName(UserNameRequest request, ServerCallContext context)
        {
            var res = new UserNameResponse()
            {
                UserName = "tim lv",
            };
            res.Samples.Add(10);
            res.Samples.Add(20);
            var byteString = ByteString.CopyFrom(Encoding.UTF8.GetBytes("Hello"));
            res.Images.Add(byteString);
            return Task.FromResult(res);
        }
        public override Task<UserbodyResponse> GetUserBody(UserBodyRequest request, ServerCallContext context)
        {
            var res = new UserbodyResponse();
            res.Person = new Person()
            {
                UserId = 1,
                UserName = "tim lv",
            };
            res.Person.Sex = Person.Types.Sex.Man;
            return Task.FromResult(res);
        }
    }
}
