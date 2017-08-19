using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Dto
{
    public class PageBaseDto:IShouldNormalize
    {
        /// <summary>
        /// 一页的数量
        /// </summary>
        public int ShowCount { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        public void Normalize()
        {
            if (this.ShowCount <= 0)
            {
                this.ShowCount = 20;
            }

            if (this.PageIndex<=0)
            {
                this.PageIndex = 1;
            }
        }
    }
}
