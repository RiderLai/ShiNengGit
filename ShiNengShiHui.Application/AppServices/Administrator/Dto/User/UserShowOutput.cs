namespace ShiNengShiHui.AppServices.AdministratorDTO
{
    public class UserShowOutput
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public int? TeacherId { get; set; }

        public string TeacherName { get; set; }
    }
}