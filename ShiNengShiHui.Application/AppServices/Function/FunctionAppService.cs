using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiNengShiHui.AppServices.Dto;
using Abp.Domain.Repositories;
using ShiNengShiHui.Entities.Function;

namespace ShiNengShiHui.AppServices
{
    public class FunctionAppService : ShiNengShiHuiAppServiceBase,IFunctionAppService
    {
        private readonly IFunctionRepository _functionRepository;

        public FunctionAppService(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }

        public FunctionGetOfRoleOutput FunctionOfRoleGetAll()
        {
            var functions=_functionRepository.GetFunctionOfRoles();
            if (functions==null)
            {
                return null;
            }
            
            var result = functions.Select<Function, FunctionDto>(m => ObjectMapper.Map<FunctionDto>(m));
            return new FunctionGetOfRoleOutput() { Functions = result.ToList<FunctionDto>() };
        }
    }
}
