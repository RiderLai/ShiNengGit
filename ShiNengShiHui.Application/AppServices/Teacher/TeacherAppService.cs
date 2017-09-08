using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using ShiNengShiHui.Entities.Students;
using ShiNengShiHui.Entities.Grades;
using ShiNengShiHui.Entities.Prizes;
using Newtonsoft.Json;
using ShiNengShiHui.AppServices.Return;
using ShiNengShiHui.Entities.OtherData;
using ShiNengShiHui.AppServices.TeacherDTO;
using ShiNengShiHui.AppServices.ExcelDTO;

namespace ShiNengShiHui.AppServices
{
    public class TeacherAppService : ShiNengShiHuiAppServiceBase, ITeacherAppService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IPrizeRepository _prizeRepository;
        private readonly IRepository<PrizeItem, Guid> _prizeItemRepository;

        private readonly IExcelAppService _excelAppService;

        public TeacherAppService(IStudentRepository studentRepository,
            IGradeRepository gradeRepository,
            IPrizeRepository prizeRepository,
            IRepository<PrizeItem, Guid> prizeItemRepository,
            IExcelAppService excelAppService)
        {
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
            _prizeRepository = prizeRepository;
            _prizeItemRepository = prizeItemRepository;
            _excelAppService = excelAppService;
        }

        #region 添加

        #region 添加成绩
        /// <summary>
        /// 添加成绩
        /// </summary>
        /// <param name="createGradeInput"></param>
        /// <returns></returns>
        public ReturnVal CreateGrade(CreateGradeInput createGradeInput)
        {
            var flag = _gradeRepository.FirstOrDefault(g => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(g.DateJson).Date.DayOfYear == createGradeInput.Datetime.DayOfYear && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(g.DateJson).Date.Year == createGradeInput.Datetime.Year && g.StudentId == createGradeInput.StudentId);
            if (flag == null)
            {
                Grade grade = new Grade()
                {
                    StudentId = createGradeInput.StudentId,
                    GradeStringJson = JsonConvert.SerializeObject(new GradeData() { Grades = createGradeInput.Grades, PenaltyReason = createGradeInput.PenaltyReason }),
                    DateJson = JsonConvert.SerializeObject(new GradeOrPrizeDateTime()
                    {
                        Date = createGradeInput.Datetime,
                        SchoolYear = createGradeInput.SchoolYead,
                        Semester = createGradeInput.Semester,
                        Week = createGradeInput.Week
                    })

                };
                _gradeRepository.Insert(grade);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }

        } 
        #endregion

        #region 批量添加成绩
        /// <summary>
        /// 批量添加成绩
        /// </summary>
        /// <param name="createGradeRangeInput"></param>
        /// <returns></returns>
        public ReturnVal CreateGradeRange(CreateGradeRangeInput createGradeRangeInput)
        {
            GradeInsertOfExcelOutput gradeInsertOfExcelOutput = _excelAppService.GradeInsertOfExcel(new GradeInsertOfExcelInput() { DataStream = createGradeRangeInput.DataStream });

            if (gradeInsertOfExcelOutput == null)
            {
                return null;
            }

            List<string> errNumber = new List<string>();
            for (int i = 0, length = gradeInsertOfExcelOutput.Grades.Length; i < length; i++)
            {
                CreateGradeInput temp = gradeInsertOfExcelOutput.Grades[i];
                var flag = _gradeRepository.FirstOrDefault(g => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(g.DateJson).Date.DayOfYear == temp.Datetime.DayOfYear && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(g.DateJson).Date.Year == temp.Datetime.Year && g.StudentId == temp.StudentId);
                if (flag == null)
                {
                    Grade grade = new Grade()
                    {
                        StudentId = temp.StudentId,
                        GradeStringJson = JsonConvert.SerializeObject(new GradeData() { Grades = temp.Grades, PenaltyReason = temp.PenaltyReason }),
                        DateJson = JsonConvert.SerializeObject(new GradeOrPrizeDateTime()
                        {
                            Date = temp.Datetime,
                            SchoolYear = temp.SchoolYead,
                            Semester = temp.Semester,
                            Week = temp.Week
                        })
                    };
                    _gradeRepository.Insert(grade);
                }
                else
                {
                    errNumber.Add(temp.StudentId.ToString());
                }
            }

            if (errNumber.Count == gradeInsertOfExcelOutput.Grades.Length)
            {
                return new ReturnVal(ReturnStatu.Err, "添加失败，失败同学的学号：", errNumber);
            }
            else if (errNumber.Count > 0)
            {
                return new ReturnVal(ReturnStatu.Failure, "有部分同学成绩没有添加成功，失败同学的学号：", errNumber);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Success, "全部添加成功");
            }

            throw new NotImplementedException();
        } 
        #endregion

        #region 添加学生
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="createStudentInput"></param>
        /// <returns></returns>
        public ReturnVal CreateStudent(CreateStudentInput createStudentInput)
        {
            var flag = _studentRepository.FirstOrDefault(s => s.Name.Equals(createStudentInput.Name));
            if (flag == null)
            {
                Student student = ObjectMapper.Map<Student>(createStudentInput);
                student.ClassId = UserManager.Users.Where(m => m.Id == AbpSession.UserId).ToList()[0].Teacher.ClassId;
                _studentRepository.Insert(student);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
        } 
        #endregion

        #region 批量添加学生
        /// <summary>
        /// 批量添加学生
        /// </summary>
        /// <param name="createStudentRangeInput"></param>
        /// <returns></returns>
        public ReturnVal CreateStudentRange(CreateStudentRangeInput createStudentRangeInput)
        {
            StudentInsertOfExcelOutput studentInsertOfExcelOutput = _excelAppService.StudentInsertOfExcel(new StudentInsertOfExcelInput() { DataStream = createStudentRangeInput.DataStream });

            if (studentInsertOfExcelOutput == null)
            {
                return null;
            }

            List<string> errStudentName = new List<string>();
            for (int i = 0, length = studentInsertOfExcelOutput.Students.Length; i < length; i++)
            {
                var flag = _studentRepository.FirstOrDefault(m => m.Name.Equals(studentInsertOfExcelOutput.Students[i].Name));
                if (flag == null)
                {
                    _studentRepository.Insert(ObjectMapper.Map<Student>(studentInsertOfExcelOutput.Students[i]));
                }
                else
                {
                    errStudentName.Add(studentInsertOfExcelOutput.Students[i].Name);
                }
            }

            if (errStudentName.Count == studentInsertOfExcelOutput.Students.Length)
            {
                return new ReturnVal(ReturnStatu.Err, "添加失败，失败同学的名字：", errStudentName);
            }
            else if (errStudentName.Count > 0)
            {
                return new ReturnVal(ReturnStatu.Failure, "有部分同学没有添加成功，失败同学的名字：", errStudentName);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Success, "全部添加成功");
            }
        }
        #endregion

        #endregion

        #region 删除

        #region 删除成绩
        /// <summary>
        /// 删除成绩
        /// </summary>
        /// <param name="deleteGradeInput"></param>
        /// <returns></returns>
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
        #endregion

        public ReturnVal DeleteGradeRange(DeleteGradeRangeInput deleteGradeRangeInput)
        {
            throw new NotImplementedException();
        }

        #region 删除学生
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="deleteStudentInput"></param>
        /// <returns></returns>
        public ReturnVal DeleteStudent(DeleteStudentInput deleteStudentInput)
        {
            var flag = _studentRepository.FirstOrDefault(deleteStudentInput.Id);
            if (flag != null)
            {
                _studentRepository.Delete(flag);
                var gradeDeleteList = _gradeRepository.GetAllList(m => m.StudentId == flag.Id);
                for (int i = 0, length = gradeDeleteList.Count; i < length; i++)
                {
                    _gradeRepository.Delete(gradeDeleteList[i]);
                }
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
        } 
        #endregion

        public ReturnVal DeleteStudentRange(DeleteStudentRangeInput deleteStudentRangeInput)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 展示

        #region 展示单个成绩
        /// <summary>
        /// 展示单个成绩
        /// </summary>
        /// <param name="showGradeInput"></param>
        /// <returns></returns>
        public ShowGradeOutput ShowGrade(ShowGradeInput showGradeInput)
        {
            Grade grade = _gradeRepository.FirstOrDefault(showGradeInput.Id);

            if (grade == null)
            {
                return null;
            }

            var date = JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(grade.DateJson);
            var gradedata = JsonConvert.DeserializeObject<GradeData>(grade.GradeStringJson);
            return new ShowGradeOutput()
            {
                Id = grade.Id,
                StudentName = _studentRepository.Get(grade.StudentId).Name,
                Grades = gradedata.Grades,
                PenaltyReason = gradedata.PenaltyReason,
                DateTime = date.Date,
                SchoolYead = date.SchoolYear,
                Semester = date.Semester,
                Week = date.Week,
                SchoolYearAndMore = date.SchoolYear + "  " + date.Semester + "  " + date.Week
            };
        }
        #endregion

        #region 展示单个奖项
        /// <summary>
        /// 展示单个奖项
        /// </summary>
        /// <param name="showPrizeInput"></param>
        /// <returns></returns>
        public ShowPrizeOutput ShowPrize(ShowPrizeInput showPrizeInput)
        {
            Prize prize = _prizeRepository.FirstOrDefault(showPrizeInput.Id);

            if (prize == null)
            {
                return null;
            }

            var date = ObjectMapper.Map<GradeOrPrizeDateTime>(prize.DateJosn);
            return new ShowPrizeOutput()
            {
                StudentName = _studentRepository.Get(prize.StudentId).Name,
                PrizeName = _prizeItemRepository.Get(prize.PrizeItemId).Name,
                DateTime = date.Date,
                SchoolYearAndMore = date.SchoolYear + "  " + date.Semester + "  " + date.Week
            };
        }
        #endregion

        #region 展示单个学生
        /// <summary>
        /// 展示单个学生
        /// </summary>
        /// <param name="showStudentInput"></param>
        /// <returns></returns>
        public ShowStudentOutput ShowStudent(ShowStudentInput showStudentInput)
        {
            Student student = _studentRepository.FirstOrDefault(showStudentInput.Id);

            if (student == null)
            {
                return null;
            }
            return ObjectMapper.Map<ShowStudentOutput>(student);
        }
        #endregion

        #region 分页展示成绩
        /// <summary>
        /// 分页展示成绩
        /// </summary>
        /// <param name="showPageGradeInput"></param>
        /// <returns></returns>
        public ShowPageGradeOutput ShowPageGrade(ShowPageGradeInput showPageGradeInput)
        {
            long count = _gradeRepository.Count();
            showPageGradeInput.PageCount = (int)(count / showPageGradeInput.ShowCount);
            if (count % showPageGradeInput.ShowCount > 0)
            {
                showPageGradeInput.PageCount += 1;
            }

            if (showPageGradeInput.PageIndex > showPageGradeInput.PageCount)
            {
                showPageGradeInput.PageIndex = 1;
            }

            Grade[] grades = _gradeRepository.GetPage(showPageGradeInput.PageIndex, showPageGradeInput.ShowCount);
            var result = ObjectMapper.Map<ShowPageGradeOutput>(showPageGradeInput);
            result.Lenth = grades.Length;
            result.ShowGradeOutputs = grades.Select<Grade, ShowGradeOutput>(m =>
               {
                   GradeOrPrizeDateTime gradeOrPrizeDateTime = JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson);
                   GradeData gradeData = JsonConvert.DeserializeObject<GradeData>(m.GradeStringJson);
                   return new ShowGradeOutput()
                   {
                       Id = m.Id,
                       StudentName = _studentRepository.Get(m.StudentId).Name,
                       Grades = gradeData.Grades,
                       PenaltyReason = gradeData.PenaltyReason,
                       DateTime = gradeOrPrizeDateTime.Date,
                       SchoolYearAndMore = gradeOrPrizeDateTime.SchoolYear + "  " + gradeOrPrizeDateTime.Semester + "  " + gradeOrPrizeDateTime.Week
                   };
               }).ToArray<ShowGradeOutput>();
            return result;
        }
        #endregion

        #region 分页展示奖项
        /// <summary>
        /// 分页展示奖项
        /// </summary>
        /// <param name="showPagePrizeInput"></param>
        /// <returns></returns>
        public ShowPagePrizeOutput ShowPagePrize(ShowPagePrizeInput showPagePrizeInput)
        {
            long count = _prizeRepository.Count();
            showPagePrizeInput.PageCount = (int)(count / showPagePrizeInput.ShowCount);
            if (count % showPagePrizeInput.ShowCount > 0)
            {
                showPagePrizeInput.PageCount += 1;
            }

            if (showPagePrizeInput.PageIndex > showPagePrizeInput.PageCount)
            {
                showPagePrizeInput.PageIndex = 1;
            }

            Prize[] prizes = _prizeRepository.GetPage(showPagePrizeInput.PageIndex, showPagePrizeInput.ShowCount);
            var result = ObjectMapper.Map<ShowPagePrizeOutput>(showPagePrizeInput);
            result.Lenth = prizes.Length;
            result.ShowPrizeOutputs = prizes.Select<Prize, ShowPrizeOutput>(m =>
               {
                   var date = JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn);
                   return new ShowPrizeOutput()
                   {
                       StudentName = _studentRepository.Get(m.StudentId).Name,
                       PrizeName = _prizeItemRepository.Get(m.PrizeItemId).Name,
                       DateTime = date.Date,
                       SchoolYearAndMore = date.SchoolYear + "  " + date.Semester + "  " + date.Week
                   };
               }).ToArray<ShowPrizeOutput>();
            return result;
        }
        #endregion

        #region 分页展示学生
        /// <summary>
        /// 分页展示学生
        /// </summary>
        /// <param name="showPageStudentInput"></param>
        /// <returns></returns>
        public ShowPageStudentOutput ShowPageStudent(ShowPageStudentInput showPageStudentInput)
        {
            int count = _studentRepository.Count();
            showPageStudentInput.PageCount = count / showPageStudentInput.ShowCount;
            if (count % showPageStudentInput.ShowCount > 0)
            {
                showPageStudentInput.PageCount += 1;
            }

            if (showPageStudentInput.PageIndex > showPageStudentInput.PageCount)
            {
                showPageStudentInput.PageIndex = 1;
            }
            //Student[] students = _studentRepository.GetAllList().ToArray<Student>();
            //List<ShowStudentOutput> list = new List<ShowStudentOutput>();

            //int start = (showPageStudentInput.PageIndex - 1) * showPageStudentInput.ShowCount;
            //int end = start + showPageStudentInput.ShowCount;
            //if (end>students.Length)
            //{
            //    end = students.Length;
            //}
            //for (int i = start; i < end; i++)
            //{
            //    list.Add(ObjectMapper.Map<ShowStudentOutput>(students[i]));
            //}

            Student[] students = _studentRepository.GetPage(showPageStudentInput.PageIndex, showPageStudentInput.ShowCount);
            var result = ObjectMapper.Map<ShowPageStudentOutput>(showPageStudentInput);
            result.Lenth = students.Length;
            result.ShowStudentOutputs = students.Select<Student, ShowStudentOutput>(s => ObjectMapper.Map<ShowStudentOutput>(s)).ToArray<ShowStudentOutput>();
            return result;
        }
        #endregion

        #endregion

        #region 更新

        #region 更新成绩
        /// <summary>
        /// 更新成绩
        /// </summary>
        /// <param name="updateGradeInput"></param>
        /// <returns></returns>
        public ReturnVal UpdateGrade(UpdateGradeInput updateGradeInput)
        {
            var flag = _gradeRepository.FirstOrDefault(updateGradeInput.Id);
            if (flag != null)
            {
                Grade grade = flag;
                grade.GradeStringJson = JsonConvert.SerializeObject(new GradeData() { Grades = updateGradeInput.Grades, PenaltyReason = updateGradeInput.PenaltyReason });
                grade.DateJson = JsonConvert.SerializeObject(new GradeOrPrizeDateTime()
                {
                    Date = updateGradeInput.Datetime,
                    SchoolYear = updateGradeInput.SchoolYead,
                    Semester = updateGradeInput.Semester,
                    Week = updateGradeInput.Week
                });
                _gradeRepository.Update(grade);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
        } 
        #endregion

        public ReturnVal UpdateGradeRange(UpdateGradeRangeInput updateGradeRangeInput)
        {
            throw new NotImplementedException();
        }

        #region 更新学生信息
        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="updateStudentInput"></param>
        /// <returns></returns>
        public ReturnVal UpdateStudent(UpdateStudentInput updateStudentInput)
        {
            var flag = _studentRepository.FirstOrDefault(updateStudentInput.Id);
            if (flag != null)
            {
                updateStudentInput.ClassId = flag.ClassId;
                ObjectMapper.Map<UpdateStudentInput, Student>(updateStudentInput, flag);
                _studentRepository.Update(flag);
                return new ReturnVal(ReturnStatu.Success);
            }
            else
            {
                return new ReturnVal(ReturnStatu.Failure);
            }
        } 
        #endregion

        public ReturnVal UpdateStudentRange(UpdateStudentRangeInput updateStudentRangeInput)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 评奖计算

        #region 勿用
        /// <summary>
        /// 勿用
        /// </summary>
        /// <param name="prizeComputInput"></param>
        public void PrizeComput(PrizeComputInput prizeComputInput)
        {
            var gradeList = _gradeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.Year == prizeComputInput.DateTime.Year && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.DayOfYear == prizeComputInput.DateTime.DayOfYear);
            if (gradeList == null || gradeList.Count <= 0)
            {
                return;
            }

            double avgGrade = 0;
            gradeList.ForEach(m => avgGrade += JsonConvert.DeserializeObject<GradeData>(m.GradeStringJson).Sums);
            avgGrade /= gradeList.Count;

            //奖项ID
            Guid tianMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.TianMoFanSheng)).Id;
            Guid zhouMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.ZhouMoFanSheng)).Id;
            Guid yueMoFanShengI = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.YueMoFanSheng)).Id;
            Guid youXiuTuanDui = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.YouXiuTuanDui)).Id;

            #region 评选天模范生
            //评选天模范生
            gradeList.ForEach(m =>
            {
                var prize = _prizeRepository.FirstOrDefault(p => p.DateJosn.Equals(m.DateJson));
                if (prize != null)
                {
                    _prizeRepository.Delete(prize);
                }


                if (JsonConvert.DeserializeObject<GradeData>(m.GradeStringJson).Sums >= avgGrade)
                {
                    _prizeRepository.Insert(new Prize()
                    {
                        PrizeItemId = tianMoFanShengId,
                        StudentId = m.StudentId,
                        DateJosn = m.DateJson
                    });
                }
            });
            #endregion

            #region 评选周模范生
            var prizeTianList = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Week == JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(gradeList[0].DateJson).Week &&
                                                   JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).SchoolYear == JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(gradeList[0].DateJson).SchoolYear &&
                                                   JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Semester == JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(gradeList[0].DateJson).Semester &&
                                                   m.PrizeItemId == tianMoFanShengId);
            var studentList = _studentRepository.GetAll().ToList<Student>();

            //判断这一周有几天
            double weekDayMax = 0;
            for (int i = 0, length = studentList.Count / 2; i < length; i++)
            {
                int flag = prizeTianList.Where(m => m.StudentId == studentList[i].Id).Count();
                if (flag > weekDayMax) weekDayMax = flag;
            }


            //删除已存在的周模范生
            var prizeZhouList = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Week == JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(gradeList[0].DateJson).Week &&
                                                             JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).SchoolYear == JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(gradeList[0].DateJson).SchoolYear &&
                                                             JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Semester == JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(gradeList[0].DateJson).Semester &&
                                                             m.PrizeItemId == zhouMoFanShengId);
            if (prizeZhouList != null)
            {
                for (int i = 0, length = prizeZhouList.Count; i < length; i++)
                {
                    _prizeRepository.Delete(prizeZhouList[i]);
                }
            }
            //新增周模范生
            for (int i = 0, length = studentList.Count; i < length; i++)
            {
                int flag = prizeTianList.Where(m => m.StudentId == studentList[i].Id).Count();
                if (flag == weekDayMax)
                {
                    _prizeRepository.Insert(new Prize()
                    {
                        StudentId = studentList[i].Id,
                        PrizeItemId = zhouMoFanShengId,
                        DateJosn = gradeList[0].DateJson
                    });
                }
            }
            #endregion

            int week = JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(gradeList[0].DateJson).Week;
            List<Prize> prizeMonthOfWeelList = null;
            if (week % 4 != 0)
            {
                prizeMonthOfWeelList = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Week > (week - week % 4) &&
                                                                        JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Week <= week &&
                                                                        m.PrizeItemId == zhouMoFanShengId);
            }
            else
            {
                prizeMonthOfWeelList = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Week > (week - 4) &&
                                                                        JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Week <= week &&
                                                                        m.PrizeItemId == zhouMoFanShengId);
            }

            int monthWeekMax = week % 4 != 0 ? week % 4 : 4;

        }
        #endregion

        #region 计算天模范生
        /// <summary>
        /// 计算天模范生
        /// </summary>
        /// <param name="prizeComputInput"></param>
        public void PrizeTianMoFanShengComput(PrizeTianMoFanShengComputInput prizeComputInput)
        {
            Guid tianMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.TianMoFanSheng)).Id;

            var grades = _gradeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.DayOfYear == prizeComputInput.DateTime.DayOfYear
                                                          && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJson).Date.Year == prizeComputInput.DateTime.Year);

            //计算平均成绩
            double avgGrade = 0;
            grades.ForEach(m => avgGrade += JsonConvert.DeserializeObject<GradeData>(m.GradeStringJson).Sums);
            avgGrade /= grades.Count;

            //添加天模范生
            grades.ForEach(m =>
            {
                var prize = _prizeRepository.FirstOrDefault(p => p.DateJosn.Equals(m.DateJson));
                if (prize != null)
                {
                    _prizeRepository.Delete(prize);
                }

                if (JsonConvert.DeserializeObject<GradeData>(m.GradeStringJson).Sums >= avgGrade)
                {
                    _prizeRepository.Insert(new Prize()
                    {
                        PrizeItemId = tianMoFanShengId,
                        StudentId = m.StudentId,
                        DateJosn = m.DateJson
                    });
                }
            });
        }
        #endregion

        #region 计算周模范生
        /// <summary>
        /// 计算周模范生
        /// </summary>
        /// <param name="prizeZhouMoFanShengComputInput"></param>
        public void PrizeZhouMoFanShengComput(PrizeZhouMoFanShengComputInput prizeZhouMoFanShengComputInput)
        {
            Guid tianMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.TianMoFanSheng)).Id;
            Guid zhouMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.ZhouMoFanSheng)).Id;
            //这个星期一和星期日，在这一年的第几天
            int weekMon = prizeZhouMoFanShengComputInput.DateTime.DayOfYear - ((int)prizeZhouMoFanShengComputInput.DateTime.DayOfWeek - 1);
            int weekSat = prizeZhouMoFanShengComputInput.DateTime.DayOfYear - ((int)prizeZhouMoFanShengComputInput.DateTime.DayOfWeek - 1) + 6;

            //获得这一周所有的天模范生
            var prizesDay = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.DayOfYear >= weekMon
                                                          && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.DayOfYear <= weekSat
                                                          && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.Year == prizeZhouMoFanShengComputInput.DateTime.Year
                                                          && m.PrizeItemId == tianMoFanShengId);
            if (prizesDay==null||prizesDay.Count<=0)
            {
                return;
            }

            //删除已存在的周模范生
            var prizesWeek = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.DayOfYear >= weekMon
                                                          && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.DayOfYear <= weekSat
                                                          && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.Year == prizeZhouMoFanShengComputInput.DateTime.Year
                                                          && m.PrizeItemId == zhouMoFanShengId);
            foreach (Prize item in prizesWeek)
            {
                _prizeRepository.Delete(item);
            }

            //对天模范生进行分组
            var group = from items in prizesDay
                        group items by JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(items.DateJosn).Date.DayOfYear into item
                        select item;
            int dayNumber = group.Count();

            //计算出周模范生。存放在groupOne中
            var groupOne = group.ToList()[0].ToList();
            for (int i = 1, length = dayNumber; i < length; i++)
            {
                var grouptemp = group.ToList()[i].ToList();
                var groupCopy = new List<Prize>(groupOne);
                foreach (Prize item in groupCopy)
                {
                    Prize temp = grouptemp.FirstOrDefault(m => m.StudentId == item.StudentId);
                    if (temp == null)
                    {
                        groupOne.Remove(item);
                    }
                }
            }

            //添加周模范生
            //周模范生生时间采用item当中的时间
            foreach (Prize item in groupOne)
            {
                _prizeRepository.Insert(new Prize()
                {
                    PrizeItemId = zhouMoFanShengId,
                    StudentId = item.StudentId,
                    DateJosn = item.DateJosn
                });
            }
        }
        #endregion

        #region 计算月模范生
        /// <summary>
        /// 计算月模范生
        /// </summary>
        /// <param name="prizeYueMoFanShengComput"></param>
        public void PrizeYueMoFanShengComput(PrizeYueMoFanShengComputInput prizeYueMoFanShengComput)
        {
            Guid zhouMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.ZhouMoFanSheng)).Id;
            Guid yueMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.YueMoFanSheng)).Id;

            //获得一个月的所有周模范生
            var prizeWeek = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.Month == prizeYueMoFanShengComput.DateTime.Month
                                                           && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.Year == prizeYueMoFanShengComput.DateTime.Year
                                                           && m.PrizeItemId == zhouMoFanShengId);
            if (prizeWeek==null||prizeWeek.Count<=0)
            {
                return;
            }

            //获得一个月的所有月模范生，并将这些月模范生删除
            var prizeMonth = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.Month == prizeYueMoFanShengComput.DateTime.Month
                                                           && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Date.Year == prizeYueMoFanShengComput.DateTime.Year
                                                           && m.PrizeItemId == yueMoFanShengId);
            foreach (Prize item in prizeMonth)
            {
                _prizeRepository.Delete(item);
            }

            //对周模范生进行分组
            var group = from items in prizeWeek
                        group items by JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(items.DateJosn).Week into item
                        select item;
            int monthNumber = group.Count();

            //计算出月模范生。存放在groupOne中
            var groupOne = group.ToList()[0].ToList();
            for (int i = 1, length = monthNumber; i < length; i++)
            {
                var grouptemp = group.ToList()[i].ToList();
                var groupCopy = new List<Prize>(groupOne);
                foreach (Prize item in groupCopy)
                {
                    var temp = grouptemp.FirstOrDefault(m => m.StudentId == item.StudentId);
                    if (temp == null)
                    {
                        groupOne.Remove(item);
                    }
                }
            }

            //添加月模范生
            //月模范生生时间采用item当中的时间
            foreach (Prize item in groupOne)
            {
                _prizeRepository.Insert(new Prize()
                {
                    PrizeItemId = yueMoFanShengId,
                    DateJosn = item.DateJosn,
                    StudentId = item.StudentId,
                });
            }
        }
        #endregion

        #region 计算校模范生
        /// <summary>
        /// 计算校模范生
        /// </summary>
        /// <param name="prizeXiaoMoFanShengComput"></param>
        public void PrizeXiaoMoFanShengComput(PrizeXiaoMoFanShengComputInput prizeXiaoMoFanShengComput)
        {
            Guid yueMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.YueMoFanSheng)).Id;
            Guid xiaoMoFanShengId = _prizeItemRepository.FirstOrDefault(m => m.Name.Equals(PrizeItem.XiaoMoFanSheng)).Id;

            //获取一学期的所有月模范生
            var prizeMonth = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).SchoolYear == prizeXiaoMoFanShengComput.SchoolYear
                                                              && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Semester == prizeXiaoMoFanShengComput.Semester
                                                              && m.PrizeItemId == yueMoFanShengId);
            if (prizeMonth==null||prizeMonth.Count<=0)
            {
                return;
            }

            //获取一学期的校模范生
            var prizeXiao = _prizeRepository.GetAllList(m => JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).SchoolYear == prizeXiaoMoFanShengComput.SchoolYear
                                                                && JsonConvert.DeserializeObject<GradeOrPrizeDateTime>(m.DateJosn).Semester == prizeXiaoMoFanShengComput.Semester
                                                                && m.PrizeItemId == xiaoMoFanShengId);
            //删除一学期的校模范生
            foreach (Prize item in prizeXiao)
            {
                _prizeRepository.Delete(item);
            }

            //按照学号，对月模范生进行分组
            var group = from items in prizeMonth
                        group items by items.StudentId into item
                        select item;

            //添加校模范生
            foreach (IGrouping<int, Prize> item in group)
            {
                if (item.Count() >= 3)
                {
                    var temp = item.ToList()[0];
                    _prizeRepository.Insert(new Prize()
                    {
                        PrizeItemId = xiaoMoFanShengId,
                        DateJosn = temp.DateJosn,
                        StudentId = temp.StudentId
                    });
                }
            }
        }  
        #endregion

        #endregion

    }
}
