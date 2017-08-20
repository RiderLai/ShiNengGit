using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Function
{
    public class Function:Entity
    {
        public virtual int PID { get; set; }
        /// <summary>
        /// 权限名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Order { get; set; }
        /// <summary>
        /// Action
        /// </summary>
        public virtual string Action { get; set; }
        /// <summary>
        /// 控制器名
        /// </summary>
        public virtual string Controller { get; set; }
        /// <summary>
        /// 小图标
        /// </summary>
        public virtual string ICon { get; set; }

        public virtual int RoleId { get; set; }
    }
}
