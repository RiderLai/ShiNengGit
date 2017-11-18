using Abp.AutoMapper;
using ShiNengShiHui.Entities.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.FunctionDTO
{
    [AutoMapFrom(typeof(Function))]
    public class FunctionDto
    {

        public int Id { get; set; }

        public int PID { get; set; }
        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Action
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 控制器名
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 小图标
        /// </summary>
        public string ICon { get; set; }

        public List<FunctionDto> CFunctions { get; set; }
    }
}
