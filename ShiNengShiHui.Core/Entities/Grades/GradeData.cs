using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Grades
{
    public class GradeData
    {
        public int[] G;

        #region 十项分数
        /// <summary>
        /// 敬
        /// </summary>
        public int Respect
        {
            get { return G[0] + G[1] + G[2]; }
        }
        /// <summary>
        /// 善
        /// </summary>
        public int Kind
        {
            get { return G[3] + G[4] + G[5]; }
        }
        /// <summary>
        /// 净
        /// </summary>
        public int Clean
        {
            get { return G[6] + G[7] + G[8]; }
        }
        /// <summary>
        /// 捡
        /// </summary>
        public int Saves
        {
            get { return G[9] + G[10] + G[11]; }
        }
        /// <summary>
        /// 勤
        /// </summary>
        public int Industrious
        {
            get { return G[12] + G[13] + G[14]; }
        }
        /// <summary>
        /// 静
        /// </summary>
        public int Quiet
        {
            get { return G[15] + G[16] + G[17]; }
        }
        /// <summary>
        /// 厚
        /// </summary>
        public int Honest
        {
            get { return G[18] + G[19] + G[20]; }
        }
        /// <summary>
        /// 乐
        /// </summary>
        public int Enjoy
        {
            get { return G[21] + G[22] + G[23]; }
        }
        /// <summary>
        /// 跑
        /// </summary>
        public int Vigour
        {
            get { return G[24] + G[25] + G[26]; }
        }
        /// <summary>
        /// 勇
        /// </summary>
        public int Brave
        {
            get { return G[27] + G[28] + G[29]; }
        }
        /// <summary>
        /// 总分
        /// </summary>
        public int Sums
        {
            get
            {
                int sum = 0;
                for (int i = 0; i < 30; i++)
                {
                    sum += G[i];
                }
                return sum;
            }
        }
        #endregion
    }
}
