using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiNengShiHui.AppServices.HeadmasterDTO;
using ShiNengShiHui.Entities.Classes;
using ShiNengShiHui.Entities.Students;
using ShiNengShiHui.Entities.Grades;
using ShiNengShiHui.Entities.Prizes;
using ShiNengShiHui.Entities.Teachers;
using Newtonsoft.Json;
using ShiNengShiHui.Entities.OtherData;
using Abp.Domain.Repositories;

namespace ShiNengShiHui.AppServices.Headmaster
{
    public class HeadmasterAppService : ShiNengShiHuiAppServiceBase, IHeadmasterAppService
    {

        private IClassRepository _classRepository;
        private ITeacherRepository _teacherRepository;
        private IStudentRepository _studentRepository;
        private IGradeRepository _gradeRepository;
        private IPrizeRepository _prizeRepository;
        private IRepository<PrizeItem, Guid> _prizeItemRepository;

        public HeadmasterAppService(IClassRepository classRepository,
            ITeacherRepository teacherRepository,
            IStudentRepository studentRepository,
            IGradeRepository gradeRepository,
            IPrizeRepository prizeRepository,
            IRepository<PrizeItem, Guid> prizeItemReposiotry)
        {
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
            _prizeRepository = prizeRepository;
            _prizeItemRepository = prizeItemReposiotry;
        }

        #region 班级模块

        public ClassShowOutput ClassShow(ClassShowInput classShowInput)
        {
            throw new NotImplementedException();
        }

        #region 分页展示班级
        /// <summary>
        /// 分页展示班级
        /// </summary>
        /// <param name="classShowPageInput"></param>
        /// <returns></returns>
        public ClassShowPageOutput ClassShowPage(ClassShowPageInput classShowPageInput)
        {
            long count = _classRepository.Count();
            classShowPageInput.PageCount = (int)(count / classShowPageInput.ShowCount);
            if (count % classShowPageInput.ShowCount != 0)
            {
                classShowPageInput.PageCount += 1;
            }
            if (classShowPageInput.PageIndex > classShowPageInput.PageCount)
            {
                classShowPageInput.PageIndex = 1;
            }

            var Classes = _classRepository.GetPage(classShowPageInput.PageIndex, classShowPageInput.ShowCount, null);

            ClassShowPageOutput result = ObjectMapper.Map<ClassShowPageOutput>(classShowPageInput);
            result.Classes = Classes.Select(m => ObjectMapper.Map<ClassShowOutput>(m)).ToArray();

            return result;
        }
        #endregion

        #endregion

        #region 成绩模块

        public GradeShowOutput GradeShow(GradeShowInput gradeShowInput)
        {
            throw new NotImplementedException();
        }

        #region 分页展示成绩
        /// <summary>
        /// 分页展示成绩
        /// </summary>
        /// <param name="gradeShowPageInput"></param>
        /// <returns></returns>
        public GradeShowPageOutput GradeShowPage(GradeShowPageInput gradeShowPageInput)
        {
            var Class = _classRepository.FirstOrDefault(gradeShowPageInput.ClassId);
            if (Class == null)
            {
                return null;
            }

            long count;
            switch (gradeShowPageInput.ScreenCondition)
            {
                case ScreenEnum.No:
                    count = _gradeRepository.GetAll(Class.GradesTable).Count();
                    break;
                //case ScreenEnum.Day:
                //    count = _gradeRepository.GetAll(Class.GradesTable).Count(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.Year == gradeShowPageInput.DateTime.Year &&
                //                                                                  JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.DayOfYear == gradeShowPageInput.DateTime.DayOfYear);
                //    break;
                case ScreenEnum.Month:
                    count = _gradeRepository.GetAll(Class.GradesTable).Count(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.Year == gradeShowPageInput.DateTime.Year &&
                                                                                  JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.Month == gradeShowPageInput.DateTime.Month);
                    break;
                default:
                    count = _gradeRepository.GetAll(Class.GradesTable).Count();
                    break;
            }
            gradeShowPageInput.PageCount = (int)(count / gradeShowPageInput.ShowCount);
            if (count % gradeShowPageInput.ShowCount != 0)
            {
                gradeShowPageInput.PageCount += 1;
            }
            if (gradeShowPageInput.PageIndex > gradeShowPageInput.PageCount)
            {
                gradeShowPageInput.PageIndex = 1;
            }

            Grade[] grades;
            switch (gradeShowPageInput.ScreenCondition)
            {
                case ScreenEnum.No:
                    grades = _gradeRepository.GetPageFromTable(Class.GradesTable, gradeShowPageInput.PageIndex, gradeShowPageInput.ShowCount, null);
                    break;
                //case ScreenEnum.Day:
                //    grades = _gradeRepository.GetPageFromTable(Class.GradesTable, gradeShowPageInput.PageIndex, gradeShowPageInput.ShowCount,
                //                                                m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.Year == gradeShowPageInput.DateTime.Year &&
                //                                                    JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.DayOfYear == gradeShowPageInput.DateTime.DayOfYear);
                //    break;
                case ScreenEnum.Month:
                    grades = _gradeRepository.GetPageFromTable(Class.GradesTable, gradeShowPageInput.PageIndex, gradeShowPageInput.ShowCount,
                                                                m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.Year == gradeShowPageInput.DateTime.Year &&
                                                                    JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.Month == gradeShowPageInput.DateTime.Month);
                    break;
                default:
                    grades = _gradeRepository.GetPageFromTable(Class.GradesTable, gradeShowPageInput.PageIndex, gradeShowPageInput.ShowCount, null);
                    break;
            }
            var students = _studentRepository.GetAll(Class.StudentsTable).ToList();

            GradeShowPageOutput result = ObjectMapper.Map<GradeShowPageOutput>(gradeShowPageInput);
            result.Grades = grades.Select(m =>
            {
                var date = JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson);
                var grade = JsonConvert.DeserializeObject<GradeData>(m.GradeStringJson);
                string studentName = students.FirstOrDefault(item => item.Id == m.StudentId).Name;
                return new GradeShowOutput()
                {
                    Id = m.Id,
                    StudentName = studentName,
                    Grades = grade.Grades,
                    PenaltyReason = grade.PenaltyReason,
                    DateTime = date.Date,
                    SchoolYead = date.SchoolYear,
                    Semester = date.Semester,
                    Week = date.Week,
                    SchoolYearAndMore = date.SchoolYear + " " + date.Semester + " " + date.Week
                };
            }).ToArray();

            return result;
        }
        #endregion

        #endregion

        #region 奖项模块

        public PrizeShowOutput PrizeShow(PrizeShowInput prizeShowInput)
        {
            throw new NotImplementedException();
        }

        #region 分页展示奖项
        /// <summary>
        /// 分页展示奖项
        /// </summary>
        /// <param name="prizeShowPageInput"></param>
        /// <returns></returns>
        public PrizeShowPageOutput PrizeShowPage(PrizeShowPageInput prizeShowPageInput)
        {
            var Class = _classRepository.FirstOrDefault(prizeShowPageInput.ClassId);
            if (Class == null)
            {
                return null;
            }

            Guid zhouMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.ZhouMoFanSheng)).Id;
            Guid yueMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.YueMoFanSheng)).Id;
            Guid xiaoMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.XiaoMoFanSheng)).Id;

            long count;
            switch (prizeShowPageInput.ScreenCondition)
            {
                case ScreenEnum.No:
                    count = _prizeRepository.GetAll(Class.PrizesTable).Count();
                    break;
                case ScreenEnum.Week:
                    count = _prizeRepository.GetAll(Class.PrizesTable).Count(m => m.PrizeItemId == zhouMoFanShengId);
                    break;
                case ScreenEnum.Month:
                    count = _prizeRepository.GetAll(Class.PrizesTable).Count(m => m.PrizeItemId == yueMoFanShengId);
                    break;
                case ScreenEnum.Xiao:
                    count = _prizeRepository.GetAll(Class.PrizesTable).Count(m => m.PrizeItemId == xiaoMoFanShengId);
                    break;
                default:
                    count = _prizeRepository.GetAll(Class.PrizesTable).Count();
                    break;
            }
            prizeShowPageInput.PageCount = (int)(count / prizeShowPageInput.ShowCount);
            if (count % prizeShowPageInput.ShowCount != 0)
            {
                prizeShowPageInput.PageCount += 1;
            }
            if (prizeShowPageInput.PageIndex > prizeShowPageInput.PageCount)
            {
                prizeShowPageInput.PageCount = 1;
            }

            Prize[] prizes;
            switch (prizeShowPageInput.ScreenCondition)
            {
                case ScreenEnum.No:
                    prizes = _prizeRepository.GetPageFromTable(Class.PrizesTable, prizeShowPageInput.PageIndex, prizeShowPageInput.ShowCount, null);
                    break;
                case ScreenEnum.Week:
                    prizes = _prizeRepository.GetPageFromTable(Class.PrizesTable, prizeShowPageInput.PageIndex, prizeShowPageInput.ShowCount,
                                                                m => m.PrizeItemId == zhouMoFanShengId);
                    break;
                case ScreenEnum.Month:
                    prizes = _prizeRepository.GetPageFromTable(Class.PrizesTable, prizeShowPageInput.PageIndex, prizeShowPageInput.ShowCount,
                                                                m => m.PrizeItemId == yueMoFanShengId);
                    break;
                case ScreenEnum.Xiao:
                    prizes = _prizeRepository.GetPageFromTable(Class.PrizesTable, prizeShowPageInput.PageIndex, prizeShowPageInput.ShowCount,
                                                               m => m.PrizeItemId == xiaoMoFanShengId);
                    break;
                default:
                    prizes = _prizeRepository.GetPageFromTable(Class.PrizesTable, prizeShowPageInput.PageIndex, prizeShowPageInput.ShowCount, null);
                    break;
            }
            var students = _studentRepository.GetAll(Class.StudentsTable).ToList();
            var prizeItems = _prizeItemRepository.GetAll().ToList();

            PrizeShowPageOutput result = ObjectMapper.Map<PrizeShowPageOutput>(prizeShowPageInput);
            result.Prizes = prizes.Select(m =>
            {
                var date = JsonConvert.DeserializeObject<WeekDate>(m.DateJosn);
                string studentName = students.FirstOrDefault(item => item.Id == m.StudentId).Name;
                string prizeName = prizeItems.FirstOrDefault(item => item.Id == m.PrizeItemId).Name;
                return new PrizeShowOutput()
                {
                    StudentName = studentName,
                    PrizeName = prizeName,
                    SchoolYearAndMore = date.SchoolYear + "学年  " + date.Semester + "学期  " + date.Week + "周"
                };
            }).ToArray();

            return result;
        }
        #endregion

        #endregion

        #region 学生模块

        public StudentShowOutput StudentShow(StudentShowInput studentShowInput)
        {
            throw new NotImplementedException();
        }

        #region 分页展示学生
        /// <summary>
        /// 分页展示学生
        /// </summary>
        /// <param name="studentShowPageInput"></param>
        /// <returns></returns>
        public StudentShowPageOutput StudentShowPage(StudentShowPageInput studentShowPageInput)
        {
            var Class = _classRepository.FirstOrDefault(studentShowPageInput.ClassId);
            if (Class == null)
            {
                return null;
            }
            long count = _studentRepository.GetAll(Class.StudentsTable).Count();
            studentShowPageInput.PageCount = (int)(count / studentShowPageInput.ShowCount);
            if (count % studentShowPageInput.ShowCount != 0)
            {
                studentShowPageInput.PageCount += 1;
            }
            if (studentShowPageInput.PageIndex > studentShowPageInput.PageCount)
            {
                studentShowPageInput.PageIndex = 1;
            }

            var students = _studentRepository.GetPageFromTable(Class.StudentsTable, studentShowPageInput.PageIndex, studentShowPageInput.ShowCount, m => true);
            StudentShowPageOutput result = ObjectMapper.Map<StudentShowPageOutput>(studentShowPageInput);
            result.Students = students.Select(m => ObjectMapper.Map<StudentShowOutput>(m)).ToArray();
            return result;
        }
        #endregion

        #endregion

        #region 教师模块

        public TeacherShowOutput TeacherShow(TeacherShowInput teacherShowInput)
        {
            throw new NotImplementedException();
        }

        #region 分页展示教师
        /// <summary>
        /// 分页展示教师
        /// </summary>
        /// <param name="teacherShowPageInput"></param>
        /// <returns></returns>
        public TeacherShowPageOutput TeacherShowPage(TeacherShowPageInput teacherShowPageInput)
        {
            long count = _teacherRepository.Count();
            teacherShowPageInput.PageCount = (int)(count / teacherShowPageInput.ShowCount);
            if (count % teacherShowPageInput.ShowCount != 0)
            {
                teacherShowPageInput.PageCount += 1;
            }
            if (teacherShowPageInput.PageIndex > teacherShowPageInput.PageCount)
            {
                teacherShowPageInput.PageIndex = 1;
            }

            Teacher[] teachers = _teacherRepository.GetPage(teacherShowPageInput.PageIndex, teacherShowPageInput.ShowCount, m => true);

            TeacherShowPageOutput result = ObjectMapper.Map<TeacherShowPageOutput>(teacherShowPageInput);
            result.Teachers = teachers.Select(m => ObjectMapper.Map<TeacherShowOutput>(m)).ToArray();
            return result;
        }
        #endregion

        #endregion
    }
}
