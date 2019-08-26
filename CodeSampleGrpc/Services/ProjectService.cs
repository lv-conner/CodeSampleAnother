using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace CodeSampleGrpc
{
    public class ProjectService: ProjectRpcService.ProjectRpcServiceBase
    {
        public override Task<ProjectResponse> GetProjectName(ProjectRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ProjectResponse()
            {
                ProjectName = "tim lv Project"
            });
        }
    }
}
