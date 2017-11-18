using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiNengShiHui.AppServices.Return;
using ShiNengShiHui.AppServices.WeekGradeDTO;
using Abp.Domain.Repositories;
using ShiNengShiHui.Entities.WeekGrades;
using ShiNengShiHui.Entities.Students;
using Newtonsoft.Json;

namespace ShiNengShiHui.AppServices
{
    public class WeekGradeAppService : ShiNengShiHuiAppServiceBase, IWeekGradeAppService
    {
        private readonly IRepository<WeekGrade, long> _weekGradeRepository;
        private readonly IRepository<GroupWeekGrade, long> _groupWeekGradeRepository;
        private readonly IStudentRepository _studentRepository;

        public WeekGradeAppService(IRepository<WeekGrade,long> weekGradeRepository,
            IRepository<GroupWeekGrade,long> groupWeekGradeRepository,
            IStudentRepository studentRepository)
        {
            _weekGradeRepository = weekGradeRepository;
            _groupWeekGradeRepository = groupWeekGradeRepository;
            _studentRepository = studentRepository;
        }

        private List<Student> StudentsGroupGet(int groupId)
        {
            List<Student> result = _studentRepository.GetAllList(m => m.Group == groupId);
            return result;
        }

        #region 优胜组
        public ReturnVal GroupWeekGradeCreate(GroupWeekGradeCreateInput groupWeekGradeCreateInput)
        {
            WeekDate date = new WeekDate()
            {
                SchoolYear = groupWeekGradeCreateInput.SchoolYear,
                Week = groupWeekGradeCreateInput.Week,
                Semester = groupWeekGradeCreateInput.Semester
            };
            String dateString = JsonConvert.SerializeObject(date);

            //不允许，同一时间点，同一组的数据被创建两次及以上
            if (_groupWeekGradeRepository.GetAllList(m => m.DateJson == dateString && m.Group == groupWeekGradeCreateInput.Group).Count >= 1)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }

            _groupWeekGradeRepository.Insert(new GroupWeekGrade()
            {
                DateJson = dateString,
                IsWell = groupWeekGradeCreateInput.IsWell,
                Group = groupWeekGradeCreateInput.Group
            });
            return new ReturnVal(ReturnStatu.Success);
        }

        public ReturnVal GroupWeekGradeUpdate(GroupWeekGradeUpdate groupWeekGradeUpdate)
        {
            var groupWeek = _groupWeekGradeRepository.GetAllList(m => m.Id == groupWeekGradeUpdate.Id).FirstOrDefault();
            if (groupWeek == null)
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
            groupWeek.IsWell = groupWeekGradeUpdate.IsWell;
            _groupWeekGradeRepository.Update(groupWeek);
            return new ReturnVal(ReturnStatu.Success);
        }

        public GroupWeekGradeShowOutput GroupWeekGradeShow(GroupWeekGradeShowInput groupWeekGradeShowInput)
        {
            List<GroupWeekGradeShow> result = new List<WeekGradeDTO.GroupWeekGradeShow>();
            if (groupWeekGradeShowInput.Id != null)
            {
                var groupWeek = _groupWeekGradeRepository.GetAllList(m => m.Id == (long)groupWeekGradeShowInput.Id).FirstOrDefault();
                if (groupWeek == null)
                {
                    return null;
                }
                else
                {
                    result.Add(new WeekGradeDTO.GroupWeekGradeShow()
                    {
                        Id = groupWeek.Id,
                        Group = groupWeek.Group,
                        IsWell = groupWeek.IsWell,
                        Date = JsonConvert.DeserializeObject<WeekDate>(groupWeek.DateJson)
                    });
                }
            }
            else
            {
                var groupWeeks = _groupWeekGradeRepository.GetAllList();
                groupWeeks.ForEach(m =>
                {
                    result.Add(new WeekGradeDTO.GroupWeekGradeShow()
                    {
                        Id = m.Id,
                        Group = m.Group,
                        IsWell = m.IsWell,
                        Date = JsonConvert.DeserializeObject<WeekDate>(m.DateJson)
                    });
                });
            }
            return new GroupWeekGradeShowOutput() { GroupWeekGradeShows = result.ToArray() };
        }
        #endregion

        #region 周成绩
        public ReturnVal WeekGradeCreate(WeekGradeCreateInput weekGradeCreateInput)
        {
            WeekDate date = ObjectMapper.Map<WeekDate>(weekGradeCreateInput);
            String dateString = JsonConvert.SerializeObject(date);
            var students = _studentRepository.GetAll("testStudents").ToList();

            //判断添加的成绩是否已经被添加
            //if (_weekGradeRepository.GetAllList(m => m.DateJson == dateString).FirstOrDefault() != null)
            //{
            //    return new ReturnVal(ReturnStatu.Failure);
            //}

            //添加数据
            foreach (WeekGradeCreate item in weekGradeCreateInput.StudentWeekGrades)
            {
                if (students.FirstOrDefault(m => m.Id == item.SID) != null)
                {
                    if (_weekGradeRepository.GetAllList(m => m.DateJson == dateString && m.SID == item.SID).FirstOrDefault() == null)
                    {
                        var temp = ObjectMapper.Map<WeekGrade>(item);
                        temp.GradeDataJson = JsonConvert.SerializeObject(new WeekGradeData() { Grades = item.Grades });
                        temp.DateJson = dateString;

                        _weekGradeRepository.Insert(temp);
                    }
                }
            }

            return new ReturnVal(ReturnStatu.Success);
        }

        public WeekGradeShowOutput WeekGradeShow(WeekGradeShowInput weekGradeShowInput)
        {
            WeekDate date = ObjectMapper.Map<WeekDate>(weekGradeShowInput);
            String dateString = JsonConvert.SerializeObject(date);

            var students = StudentsGroupGet(weekGradeShowInput.GroupId);
            var weekGrades = _weekGradeRepository.GetAllList(m => m.DateJson == dateString);

            if (weekGrades.Count <= 0)
            {
                return null;
            }

            List<WeekGradeShow> result = new List<WeekGradeDTO.WeekGradeShow>();
            foreach (WeekGrade item in weekGrades)
            {
                var tempStudent = students.FirstOrDefault(m => m.Id == item.SID);
                if (tempStudent != null)
                {
                    result.Add(new WeekGradeDTO.WeekGradeShow()
                    {
                        Id = item.Id,
                        Name = tempStudent.Name,
                        Grades = JsonConvert.DeserializeObject<WeekGradeData>(item.GradeDataJson).Grades
                    });
                }
            }

            var groupIsWell = _groupWeekGradeRepository.GetAllList(m => m.DateJson == dateString && m.Group == weekGradeShowInput.GroupId).FirstOrDefault();
            bool isWell = false;
            if (groupIsWell != null)
            {
                isWell = groupIsWell.IsWell;
            }
            return new WeekGradeShowOutput() { WeekGradeShows = result.ToArray(), IsWellGroup = isWell};
        }

        public ReturnVal WeekGradeUpdate(WeekGradeUpdateInput weekGradeUpdateInput)
        {
            foreach (WeekGradeUpdate item in weekGradeUpdateInput.StudentWeekGrades)
            {
                var temp = _weekGradeRepository.GetAllList(m => m.Id == item.Id).FirstOrDefault();
                if (temp == null)
                {
                    continue;
                }
                temp.GradeDataJson = JsonConvert.SerializeObject(new WeekGradeData() { Grades = item.Grades });
                _weekGradeRepository.Update(temp);
            }

            return new ReturnVal(ReturnStatu.Success);
        }
        #endregion
    }
}
