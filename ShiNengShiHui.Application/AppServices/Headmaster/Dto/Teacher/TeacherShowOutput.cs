using System;

namespace ShiNengShiHui.AppServices.HeadmasterDTO
{
    public class TeacherShowOutput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public DateTime? BirthDay { get; set; }

        public int ClassId { get; set; }

        public string ClassName { get; set; }
    }
}