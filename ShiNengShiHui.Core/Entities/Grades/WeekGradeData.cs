﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Entities.Grades
{
    public class WeekGradeData
    {
        public int[] Grades { get; set; }

        #region 十项分数
        /// <summary>
        /// 敬
        /// </summary>
        public int Respect
        {
            get { return Grades[0]; }
        }
        /// <summary>
        /// 善
        /// </summary>
        public int Kind
        {
            get { return Grades[1]; }
        }
        /// <summary>
        /// 净
        /// </summary>
        public int Clean
        {
            get { return Grades[2]; }
        }
        /// <summary>
        /// 捡
        /// </summary>
        public int Saves
        {
            get { return Grades[3]; }
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
            get { return Grades[5]; }
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
            get { return Grades[9]; }
        }
        #endregion
    }
}