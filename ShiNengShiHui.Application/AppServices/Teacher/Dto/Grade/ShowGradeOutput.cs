using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.TeacherDTO
{
    public class ShowGradeOutput
    {
        public long Id { get; set; }

        public string StudentName { get; set; }

        public int[] Grades { get; set; }

        public string PenaltyReason { get; set; }

        public DateTime DateTime { get; set; }

        public int SchoolYead { get; set; }

        public int Semester { get; set; }

        public int Week { get; set; }

        public string SchoolYearAndMore { get; set; }

        #region 十项分数
        /// <summary>
        /// 敬
        /// </summary>
        public int Respect
        {
            get { return Grades[0] ; }
        }
        /// <summary>
        /// 善
        /// </summary>
        public int Kind
        {
            get { return Grades[1] ; }
        }
        /// <summary>
        /// 净
        /// </summary>
        public int Clean
        {
            get { return Grades[2] ; }
        }
        /// <summary>
        /// 捡
        /// </summary>
        public int Saves
        {
            get { return Grades[3] ; }
        }
        /// <summary>
        /// 勤
        /// </summary>
        public int Industrious
        {
            get { return Grades[4]; }
        }
        /// <summary>
        /// 静
        /// </summary>
        public int Quiet
        {
            get { return Grades[5] ; }
        }
        /// <summary>
        /// 厚
        /// </summary>
        public int Honest
        {
            get { return Grades[6]; }
        }
        /// <summary>
        /// 乐
        /// </summary>
        public int Enjoy
        {
            get { return Grades[7]; }
        }
        /// <summary>
        /// 跑
        /// </summary>
        public int Vigour
        {
            get { return Grades[8]; }
        }
        /// <summary>
        /// 勇
        /// </summary>
        public int Brave
        {
            get { return Grades[9] ; }
        }
        /// <summary>
        /// 总分
        /// </summary>
        public int Sums
        {
            get
            {
                int sum = 0;
                for (int i = 0; i < 10; i++)
                {
                    sum += Grades[i];
                }
                return sum;
            }
        }
        #endregion
    }
}
