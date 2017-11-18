using System.Linq;
using ShiNengShiHui.Entities.Function;
using ShiNengShiHui.AppServices.FunctionDTO;
using System.Collections.Generic;

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

            functions.GroupBy(m => m.Order);
            var temp = functions.Select<Function, FunctionDto>(m => ObjectMapper.Map<FunctionDto>(m)).ToList();

            List<FunctionDto> result = new List<FunctionDto>();
            temp.ForEach(m =>
            {
                m.CFunctions = new List<FunctionDto>();
                if (m.PID == 0)
                {
                    result.Add(m);
                }
                else
                {
                    result.FirstOrDefault(n => n.Id == m.PID).CFunctions.Add(m);
                }
            });

            return new FunctionGetOfRoleOutput() { Functions = result.ToList<FunctionDto>() };
        }
    }
}
