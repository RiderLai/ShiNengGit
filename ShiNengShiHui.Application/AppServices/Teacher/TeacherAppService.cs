using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiNengShiHui.AppServices.Teacher.Dto;
using Abp.Domain.Repositories;
using ShiNengShiHui.Entities.Students;
using ShiNengShiHui.Entities.Grades;
using ShiNengShiHui.Entities.Prizes;
using Newtonsoft.Json;
using ShiNengShiHui.Entities;
using ShiNengShiHui.Entities.Classes;
using ShiNengShiHui.AppServices.Return;
using Abp.Timing;
using ShiNengShiHui.Entities.OtherData;

namespace ShiNengShiHui.AppServices.Teacher
{
    public class TeacherAppService : ShiNengShiHuiAppServiceBase, ITeacherAppService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IPrizeRepository _prizeRepository;


        public TeacherAppService(IStudentRepository studentRepository,
            IGradeRepository gradeRepository,
            IPrizeRepository prizeRepository)
        {
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
            _prizeRepository = prizeRepository;
        }

        #region 添加
        public ReturnVal CreateGrade(CreateGradeInput createGradeInput)
        {
            var flag = _gradeRepository.FirstOrDefault(g => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(g.DateJson).Date.DayOfYear == createGradeInput.Datetime.DayOfYear && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(g.DateJson).Date.Year == createGradeInput.Datetime.Year && g.Student.Id == createGradeInput.StudentId);
            if (flag == null)
            {
                Grade grade = new Grade()
                {
                    CreationTime = Clock.Now,
                    CreatorUserId = AbpSession.UserId,
                    StudentId = createGradeInput.StudentId,
                    DateJson = JsonConvert.SerializeObject(new GradeOrPrizeDateTime()
                    {
                        Date = createGradeInput.Datetime,
                        SchoolYear = createGradeInput.SchoolYead,
                        Semester = createGradeInput.Semester,
                        Week = createGradeInput.Week
                    }),
                    GradeStringJson = JsonConvert.SerializeObject(new GradeData() { G = createGradeInput.Grades })
                };
                _gradeRepository.Insert(grade);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }

        }

        public ReturnVal CreateGradeRange(CreateGradeRangeInput createGradeRangeInput)
        {
            throw new NotImplementedException();
        }

        public ReturnVal CreateStudent(CreateStudentInput createStudentInput)
        {
            var flag = _studentRepository.FirstOrDefault(s => s.Name.Equals(createStudentInput.Name));
            if (flag == null)
            {
                Student student = ObjectMapper.Map<Student>(createStudentInput);
                _studentRepository.Insert(student);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
        }

        public ReturnVal CreateStudentRange(CreateStudentRangeInput createStudentRangeInput)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region 删除
        public ReturnVal DeleteGrade(DeleteGradeInput deleteGradeInput)
        {
            var flag = _gradeRepository.FirstOrDefault(deleteGradeInput.Id);
            if (flag != null)
            {
                _gradeRepository.Delete(flag);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
        }

        public ReturnVal DeleteGradeRange(DeleteGradeRangeInput deleteGradeRangeInput)
        {
            throw new NotImplementedException();
        }

        public ReturnVal DeleteStudent(DeleteStudentInput deleteStudentInput)
        {
            var flag = _studentRepository.FirstOrDefault(deleteStudentInput.Id);
            if (flag != null)
            {
                _studentRepository.Delete(flag);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
        }

        public ReturnVal DeleteStudentRange(DeleteStudentRangeInput deleteStudentRangeInput)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region 展示
        public ShowGradeOutput ShowGrade(ShowGradeInput showGradeInput)
        {
            throw new NotImplementedException();
        }

        public ShowPrizeOutput ShowPrize(ShowPrizeInput showPrizeInput)
        {
            throw new NotImplementedException();
        }

        public ShowStudentOutput ShowStudent(ShowStudentInput showStudentInput)
        {
            throw new NotImplementedException();
        }

        public ShowPageGradeOutput ShowPageGrade(ShowPageGradeInput showPageGradeInput)
        {
            throw new NotImplementedException();
        }

        public ShowPagePrizeOutput ShowPagePrize(ShowPagePrizeInput showPagePrizeInput)
        {
            throw new NotImplementedException();
        }

        public ShowPageStudentOutput ShowPageStudent(ShowPageStudentInput showPageStudentInput)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 更新
        public ReturnVal UpdateGrade(UpdateGradeInput updateGradeInput)
        {
            var flag = _gradeRepository.FirstOrDefault(updateGradeInput.Id);
            if (flag != null)
            {
                Grade grade = flag;
                grade.StudentId = updateGradeInput.StudentId;
                grade.DateJson = JsonConvert.SerializeObject(new GradeData() { G = updateGradeInput.Grades });
                grade.GradeStringJson = JsonConvert.SerializeObject(new GradeOrPrizeDateTime() { Date = updateGradeInput.Datetime, SchoolYear = updateGradeInput.SchoolYead, Semester = updateGradeInput.Semester, Week = updateGradeInput.Week });
                _gradeRepository.Update(grade);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
        }

        public ReturnVal UpdateGradeRange(UpdateGradeRangeInput updateGradeRangeInput)
        {
            throw new NotImplementedException();
        }

        public ReturnVal UpdateStudent(UpdateStudentInput updateStudentInput)
        {
            var flag = _studentRepository.FirstOrDefault(updateStudentInput.Id);
            if (flag != null)
            {
                ObjectMapper.Map<UpdateStudentInput, Student>(updateStudentInput, flag);
                _studentRepository.Update(flag);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
        }

        public ReturnVal UpdateStudentRange(UpdateStudentRangeInput updateStudentRangeInput)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
