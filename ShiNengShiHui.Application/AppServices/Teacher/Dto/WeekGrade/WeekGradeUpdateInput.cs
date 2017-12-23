using Abp.AutoMapper;

namespace ShiNengShiHui.AppServices.TeacherDTO
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