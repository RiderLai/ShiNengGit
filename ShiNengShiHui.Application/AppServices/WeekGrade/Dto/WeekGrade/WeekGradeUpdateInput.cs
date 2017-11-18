using Abp.AutoMapper;
using ShiNengShiHui.Entities.WeekGrades;

namespace ShiNengShiHui.AppServices.WeekGradeDTO
{
    public class WeekGradeUpdateInput
    {
        public WeekGradeUpdate[] StudentWeekGrades { get; set; }
    }

    public class WeekGradeUpdate
    {
        public long Id { get; set; }

        public int[] Grades { get; set; }
    }
}