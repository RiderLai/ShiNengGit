using Abp.Timing;
using ShiNengShiHui.Entities.Classes;
using ShiNengShiHui.Entities.Prizes;
using ShiNengShiHui.Entities.Teachers;
using ShiNengShiHui.EntityFramework;
using System;
using System.Linq;

namespace ShiNengShiHui.Migrations.SeedData.EntitiesData
{
    public class DefaultEntitiesDataCreator
    {

        #region Default Setting
        public const string DefalutClassName = "test";
        public const string DefalutTeacherName = "test"; 
        #endregion

        private readonly ShiNengShiHuiDbContext _context;

        public DefaultEntitiesDataCreator(ShiNengShiHuiDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        public void CreateEditions()
        {
            //add default class
            var defaultClass = _context.Class.FirstOrDefault(c => c.Name.Equals(DefaultEntitiesDataCreator.DefalutClassName));
            if (defaultClass == null)
            {
                defaultClass = new Class()
                {
                    Name = DefaultEntitiesDataCreator.DefalutClassName,
                    Display=DefaultEntitiesDataCreator.DefalutClassName,
                    InTime = Clock.Now
                };
                _context.Class.Add(defaultClass);
                _context.SaveChanges();
            }

            //add default teacher
            var defaultTeacher = _context.Teacher.FirstOrDefault(c => c.Name.Equals(DefaultEntitiesDataCreator.DefalutTeacherName));
            if (defaultTeacher == null)
            {

                var defatultTeacherClass = _context.Class.FirstOrDefault(c => c.Name.Equals(DefaultEntitiesDataCreator.DefalutClassName));
                
                defaultTeacher = new Teacher()
                {
                    Name = DefaultEntitiesDataCreator.DefalutTeacherName,
                    Sex = true,
                    Class = defatultTeacherClass
                };
                _context.Teacher.Add(defaultTeacher);
                _context.SaveChanges();
            }

            //add prize
            var tianMoFanSheng = _context.PrizeItem.FirstOrDefault(c => c.Name.Equals(PrizeItem.TianMoFanSheng));
            if (tianMoFanSheng == null)
            {
                tianMoFanSheng = new PrizeItem()
                {
                    Id = Guid.NewGuid(),
                    Name = PrizeItem.TianMoFanSheng
                };
                _context.PrizeItem.Add(tianMoFanSheng);
                _context.SaveChanges();
            }

            var zhouMoFanSheng = _context.PrizeItem.FirstOrDefault(c => c.Name.Equals(PrizeItem.ZhouMoFanSheng));
            if (zhouMoFanSheng == null)
            {
                zhouMoFanSheng = new PrizeItem()
                {
                    Id = Guid.NewGuid(),
                    Name = PrizeItem.ZhouMoFanSheng
                };
                _context.PrizeItem.Add(zhouMoFanSheng);
                _context.SaveChanges();
            }

            var youXiuTuanDui = _context.PrizeItem.FirstOrDefault(c => c.Name.Equals(PrizeItem.YouXiuTuanDui));
            if (youXiuTuanDui==null)
            {
                youXiuTuanDui = new PrizeItem()
                {
                    Id = Guid.NewGuid(),
                    Name = PrizeItem.YouXiuTuanDui
                };
                _context.PrizeItem.Add(youXiuTuanDui);
                _context.SaveChanges();
            }

            var yueMoFanSheng = _context.PrizeItem.FirstOrDefault(c => c.Name.Equals(PrizeItem.YueMoFanSheng));
            if (yueMoFanSheng == null)
            {
                yueMoFanSheng = new PrizeItem()
                {
                    Id = Guid.NewGuid(),
                    Name = PrizeItem.YueMoFanSheng
                };
                _context.PrizeItem.Add(yueMoFanSheng);
                _context.SaveChanges();
            }

            var xiaoMoFanSheng = _context.PrizeItem.FirstOrDefault(c => c.Name.Equals(PrizeItem.XiaoMoFanSheng));
            if (xiaoMoFanSheng==null)
            {
                xiaoMoFanSheng = new PrizeItem()
                {
                    Id = Guid.NewGuid(),
                    Name = PrizeItem.XiaoMoFanSheng
                };
                _context.PrizeItem.Add(xiaoMoFanSheng);
                _context.SaveChanges();
            }
        }
    }
}
