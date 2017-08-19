using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.Teacher.Dto
{
    public class ShowGradeOutput
    {
        public string StudentName { get; set; }

        public int[] Grades { get; set; }

        public DateTime DateTime { get; set; }

        public string SchoolYearAndMore { get; set; }

        #region 十项分数
        /// <summary>
        /// 敬
        /// </summary>
        public int Respect
        {
            get { return Grades[0] + Grades[1] + Grades[2]; }
        }
        /// <summary>
        /// 善
        /// </summary>
        public int Kind
        {
            get { return Grades[3] + Grades[4] + Grades[5]; }
        }
        /// <summary>
        /// 净
        /// </summary>
        public int Clean
        {
            get { return Grades[6] + Grades[7] + Grades[8]; }
        }
        /// <summary>
        /// 捡
        /// </summary>
        public int Saves
        {
            get { return Grades[9] + Grades[10] + Grades[11]; }
        }
        /// <summary>
        /// 勤
        /// </summary>
        public int Industrious
        {
            get { return Grades[12] + Grades[13] + Grades[14]; }
        }
        /// <summary>
        /// 静
        /// </summary>
        public int Quiet
        {
            get { return Grades[15] + Grades[16] + Grades[17]; }
        }
        /// <summary>
        /// 厚
        /// </summary>
        public int Honest
        {
            get { return Grades[18] + Grades[19] + Grades[20]; }
        }
        /// <summary>
        /// 乐
        /// </summary>
        public int Enjoy
        {
            get { return Grades[21] + Grades[22] + Grades[23]; }
        }
        /// <summary>
        /// 跑
        /// </summary>
        public int Vigour
        {
            get { return Grades[24] + Grades[25] + Grades[26]; }
        }
        /// <summary>
        /// 勇
        /// </summary>
        public int Brave
        {
            get { return Grades[27] + Grades[28] + Grades[29]; }
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
                    sum += Grades[i];
                }
                return sum;
            }
        }
        #endregion
    }
}
