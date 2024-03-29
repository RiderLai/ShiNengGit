﻿using System;
using System.Collections.Generic;
using System.Linq;
using ShiNengShiHui.Entities.Students;
using ShiNengShiHui.Entities.Grades;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using ShiNengShiHui.AppServices.TeacherDTO;
using ShiNengShiHui.AppServices.ExcelDTO;

namespace ShiNengShiHui.AppServices
{
    public class ExcelAppService : ShiNengShiHuiAppServiceBase, IExcelAppService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradeRepository;

        public ExcelAppService(IStudentRepository studentRepository,
            IGradeRepository gradeRepository
            )
        {
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
        }

        #region excel转DataTable
        /// <summary>
        /// 将excel表格转换成DataTable
        /// </summary>
        /// <param name="rowColumName">第几行是表头</param>
        /// /// <param name="endData">第几行结束</param>
        /// <returns></returns>
        protected DataTable ExcelToDataTable(HSSFWorkbook wk, int rowColumName, int endData)
        {
            DataTable dt = new DataTable();

            ISheet sheet = wk.GetSheetAt(0);
            IRow rowth = sheet.GetRow(rowColumName);

            for (int i = 0, length = rowth.LastCellNum; i < length - 3; i++)
            {
                //没有判断表格内容为空
                int k = i;
                dt.Columns.Add(new DataColumn(rowth.GetCell(i).StringCellValue));
            }

            for (int i = rowColumName + 1, length = sheet.LastRowNum - endData; i <= length; i++)
            {
                IRow rowtemp = sheet.GetRow(i);
                DataRow row = dt.NewRow();
                for (int j = 0, jlength = dt.Columns.Count; j < jlength; j++)
                {
                    ICell cell = rowtemp.GetCell(j);
                    if (cell == null)
                    {
                        row[j] = "";
                    }
                    else
                    {
                        switch (cell.CellType)
                        {
                            case CellType.Boolean:
                                row[j] = cell.BooleanCellValue;
                                break;
                            case CellType.String:
                                row[j] = cell.StringCellValue;
                                break;
                            case CellType.Numeric:
                                short format = cell.CellStyle.DataFormat;
                                //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理  
                                if (format == 14 || format == 31 || format == 57 || format == 58)
                                    row[j] = cell.DateCellValue;
                                else
                                    row[j] = cell.NumericCellValue;
                                break;
                            case CellType.Blank:
                                row[j] = "";
                                break;
                        }
                    }
                }
                dt.Rows.Add(row);
            }

            return dt;
        } 
        #endregion


        public GradeExcelOutput GradeExcelDown()
        {
            string[] ShiNengShiHuiItem = new string[10] { "敬", "善", "净", "捡", "勤", "静", "厚", "乐", "跑", "勇" };

            HSSFWorkbook wk = new HSSFWorkbook();

            ISheet sheet = wk.CreateSheet("成绩");

            #region 标题

            //创建表格
            IRow rowTitle = sheet.CreateRow(0);
            ICell cellTitile = rowTitle.CreateCell(0);
            cellTitile.SetCellValue("学生十能十会成绩导入模板");
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 12));

            //设置样式
            ICellStyle styleTitile = wk.CreateCellStyle();
            styleTitile.Alignment = HorizontalAlignment.Center;
            IFont fontTitile = wk.CreateFont();
            fontTitile.Boldweight = short.MaxValue;
            fontTitile.FontHeightInPoints = 22;
            fontTitile.FontName = "宋体";
            styleTitile.SetFont(fontTitile);

            //将样式添加到表格中
            cellTitile.CellStyle = styleTitile;


            rowTitle.CreateCell(13).SetCellValue("Tip：导入时请从表格第三行开始填入数据");

            IFont fontTip = wk.CreateFont();
            fontTip.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;

            ICellStyle styleTipe = wk.CreateCellStyle();
            styleTipe.SetFont(fontTip);
            styleTipe.Alignment = HorizontalAlignment.Center;
            styleTipe.VerticalAlignment = VerticalAlignment.Center;

            rowTitle.GetCell(13).CellStyle = styleTipe;
            sheet.SetColumnWidth(15, 20 * 256);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 13, 15));
            #endregion

            #region 第二行 表头

            //设置样式
            ICellStyle styleTH = wk.CreateCellStyle();
            styleTH.Alignment = HorizontalAlignment.Center;

            //设置背景颜色
            //styleTH.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            styleTH.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            styleTH.FillPattern = FillPattern.SolidForeground;

            IFont fontTH = wk.CreateFont();
            fontTH.Boldweight = short.MaxValue;
            styleTH.SetFont(fontTH);

            //创建表格
            IRow rowTH = sheet.CreateRow(1);

            ICell cellsid = rowTH.CreateCell(0);
            cellsid.SetCellValue("学号");
            cellsid.CellStyle = styleTH;
            sheet.SetColumnWidth(0, 20 * 256);

            ICell cellname = rowTH.CreateCell(1);
            cellname.SetCellValue("姓名");
            cellname.CellStyle = styleTH;
            sheet.SetColumnWidth(1, 20 * 256);

            rowTH.CreateCell(2).SetCellValue("扣分原因");
            rowTH.GetCell(2).CellStyle = styleTH;
            sheet.SetColumnWidth(2, 20 * 256);

            ICell[] cellGrade = new ICell[10];
            for (int i = 0; i < 10; i++)
            {
                cellGrade[i] = rowTH.CreateCell(3 + i);
                cellGrade[i].SetCellValue(ShiNengShiHuiItem[i]);
                sheet.SetColumnWidth(3 + i, 20 * 256);
                cellGrade[i].CellStyle = styleTH;
            }
            #endregion

            #region 学生信息
            Student[] students = _studentRepository.GetAll().ToArray<Student>();
            for (int i = 0, length = students.Length; i < length; i++)
            {
                sheet.CreateRow(2 + i).CreateCell(0).SetCellValue(students[i].Id);
                sheet.GetRow(2 + i).CreateCell(1).SetCellValue(students[i].Name);
                for (int j = 0; j < 10; j++)
                {
                    sheet.GetRow(2 + i).CreateCell(3 + j).SetCellValue(10);
                }
            }
            #endregion

            #region 日期
            //设置单元格
            sheet.CreateRow(2 + students.Length).CreateCell(0).SetCellValue("日期：");
            sheet.CreateRow(2 + students.Length + 1).CreateCell(0).SetCellValue("学年：");
            sheet.GetRow(2 + students.Length + 1).CreateCell(2).SetCellValue("学期：");
            sheet.GetRow(2 + students.Length + 1).CreateCell(4).SetCellValue("周：");

            //设置样式
            ICellStyle dateTitileStyle = wk.CreateCellStyle();
            IFont dateFont = wk.CreateFont();
            dateFont.Boldweight = short.MaxValue;
            dateTitileStyle.SetFont(dateFont);

            sheet.GetRow(2 + students.Length).GetCell(0).CellStyle = dateTitileStyle;
            sheet.GetRow(2 + students.Length + 1).GetCell(0).CellStyle = dateTitileStyle;
            sheet.GetRow(2 + students.Length + 1).GetCell(2).CellStyle = dateTitileStyle;
            sheet.GetRow(2 + students.Length + 1).GetCell(4).CellStyle = dateTitileStyle;

            //设置时间样式
            ICellStyle dateStyle = wk.CreateCellStyle();
            dateStyle.DataFormat = wk.CreateDataFormat().GetFormat("yyyy年MM月dd日");
            sheet.GetRow(2 + students.Length).CreateCell(1).SetCellValue(DateTime.Now.ToString());
            sheet.GetRow(2 + students.Length).GetCell(1).CellStyle = dateStyle;
            #endregion

            MemoryStream stream = new MemoryStream();
            wk.Write(stream);

            return new GradeExcelOutput()
            {
                ExcelData = stream
            };
        }

        public GradeInsertOfExcelOutput GradeInsertOfExcel(GradeInsertOfExcelInput gradeInsertOfExcelInput)
        {
            if (gradeInsertOfExcelInput.DataStream==null)
            {
                return null;
            }

            #region excel转换成CreateGradeInput
            HSSFWorkbook wk = new HSSFWorkbook(gradeInsertOfExcelInput.DataStream);
            ISheet sheet = wk.GetSheetAt(0);
            DataTable dataTable = this.ExcelToDataTable(wk, 1, 2);

            //获取时间
            ICell dateCell = sheet.GetRow(sheet.LastRowNum - 1).GetCell(1);
            ICell schoolYearCell = sheet.GetRow(sheet.LastRowNum).GetCell(1);
            ICell semesterCell = sheet.GetRow(sheet.LastRowNum).GetCell(3);
            ICell weekCell = sheet.GetRow(sheet.LastRowNum).GetCell(5);

            //类型转换
            //TODO:若没有按照指定的格式输入会报错
            DateTime date = dateCell.CellType == CellType.Numeric ? dateCell.DateCellValue : Convert.ToDateTime(dateCell.StringCellValue);
            int schoolYear = (int)schoolYearCell.NumericCellValue;
            int semester = (int)semesterCell.NumericCellValue;
            int week = (int)weekCell.NumericCellValue;

            List<CreateGradeInput> gradeCreateList = new List<CreateGradeInput>();
            for (int i = 0, length = dataTable.Rows.Count; i < length; i++)
            {
                DataRow row = dataTable.Rows[i];
                int[] grades = new int[10];
                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        grades[j] = Convert.ToInt32(row[3 + j]);
                    }
                    catch (Exception)
                    {
                        grades[j] = 0;
                    }
                }
                gradeCreateList.Add(new CreateGradeInput()
                {
                    StudentId = Convert.ToInt32(row[0]),
                    Grades = grades,
                    PenaltyReason = string.IsNullOrEmpty(row[2].ToString()) ? null : row[2].ToString(),
                    Datetime = date,
                    SchoolYead = schoolYear,
                    Semester = semester,
                    Week = week
                });
            } 
            #endregion

            return new GradeInsertOfExcelOutput() { Grades = gradeCreateList.ToArray() };
        }

        public StudentExcelDownOutput StudentExcelDown()
        {
            HSSFWorkbook wk = new HSSFWorkbook();

            ISheet sheet = wk.CreateSheet("学生");

            #region 标题

            //创建表格
            IRow rowTitle = sheet.CreateRow(0);
            ICell cellTitile = rowTitle.CreateCell(0);
            cellTitile.SetCellValue("学生信息导入模板");
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 2));

            //设置样式
            ICellStyle styleTitile = wk.CreateCellStyle();
            styleTitile.Alignment = HorizontalAlignment.Center;
            IFont fontTitile = wk.CreateFont();
            fontTitile.Boldweight = short.MaxValue;
            fontTitile.FontHeightInPoints = 22;
            fontTitile.FontName = "宋体";
            styleTitile.SetFont(fontTitile);

            //将样式添加到表格中
            cellTitile.CellStyle = styleTitile;


            rowTitle.CreateCell(3).SetCellValue("Tip：导入时请从表格第三行开始填入数据");

            IFont fontTip = wk.CreateFont();
            fontTip.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;

            ICellStyle styleTipe = wk.CreateCellStyle();
            styleTipe.SetFont(fontTip);
            styleTipe.Alignment = HorizontalAlignment.Center;
            styleTipe.VerticalAlignment = VerticalAlignment.Center;

            rowTitle.GetCell(3).CellStyle = styleTipe;
            sheet.SetColumnWidth(5, 20 * 256);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 3, 5));
            #endregion

            #region 第二行 表头

            //设置样式
            ICellStyle styleTH = wk.CreateCellStyle();
            styleTH.Alignment = HorizontalAlignment.Center;

            //设置背景颜色
            //styleTH.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            styleTH.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            styleTH.FillPattern = FillPattern.SolidForeground;

            IFont fontTH = wk.CreateFont();
            fontTH.Boldweight = short.MaxValue;
            styleTH.SetFont(fontTH);

            //创建表格
            IRow rowTH = sheet.CreateRow(1);

            ICell cellsid = rowTH.CreateCell(0);
            cellsid.SetCellValue("姓名");
            cellsid.CellStyle = styleTH;
            sheet.SetColumnWidth(0, 20 * 256);

            ICell cellname = rowTH.CreateCell(1);
            cellname.SetCellValue("性别");
            cellname.CellStyle = styleTH;

            rowTH.CreateCell(2).SetCellValue("小组号(请输入1-4)");
            rowTH.GetCell(2).CellStyle = styleTH;
            sheet.SetColumnWidth(2, 20 * 256);
            #endregion

            MemoryStream stream = new MemoryStream();
            wk.Write(stream);
            return new StudentExcelDownOutput() { ExcelData=stream};
        }

        public StudentInsertOfExcelOutput StudentInsertOfExcel(StudentInsertOfExcelInput studentInsertOfExcelInput)
        {
            if (studentInsertOfExcelInput.DataStream==null)
            {
                return null;
            }

            HSSFWorkbook wk = new HSSFWorkbook(studentInsertOfExcelInput.DataStream);
            DataTable dataTable = this.ExcelToDataTable(wk, 1, 0);


            #region DataTable转CreateStudentInput
            List<CreateStudentInput> studentCreateList = new List<CreateStudentInput>();
            int ClassId = UserManager.Users.Where(m => m.Id == AbpSession.UserId).ToList()[0].Teacher.ClassId;
            for (int i = 0, length = dataTable.Rows.Count; i < length; i++)
            {
                DataRow row = dataTable.Rows[i];

                string name = string.IsNullOrEmpty(row[0].ToString()) ? "null" : row[0].ToString();
                bool sex = row[1].ToString().Equals("男") ? true : false;
                int? group = string.IsNullOrEmpty(row[2].ToString()) ? null : (int?)Convert.ToInt32(row[2]);
                studentCreateList.Add(new CreateStudentInput()
                {
                    Name = name,
                    Sex = sex,
                    Group = group,
                    ClassId = ClassId
                });
            } 
            #endregion

            return new StudentInsertOfExcelOutput() { Students = studentCreateList.ToArray() };
        }
    }
}
