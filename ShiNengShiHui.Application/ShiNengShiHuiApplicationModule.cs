using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using ShiNengShiHui.Users;
using ShiNengShiHui.Entities.Teachers;
using ShiNengShiHui.Entities.Classes;

namespace ShiNengShiHui
{
    [DependsOn(typeof(ShiNengShiHuiCoreModule), typeof(AbpAutoMapperModule))]
    public class ShiNengShiHuiApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
                #region Administrator
                mapper.CreateMap<User, AppServices.AdministratorDTO.UserShowOutput>()
                .ForMember(n => n.TeacherName, m => m.MapFrom(input => input.Teacher.Name));

                mapper.CreateMap<Teacher, AppServices.AdministratorDTO.TeacherShowOutput>()
                        .ForMember(n => n.ClassName, m => m.MapFrom(input => input.Class.Display));
                #endregion

                #region Headmaster
                mapper.CreateMap<Teacher, AppServices.HeadmasterDTO.TeacherShowOutput>()
                        .ForMember(n => n.ClassName, m => m.MapFrom(input => input.Class.Display));

                mapper.CreateMap<Class, AppServices.HeadmasterDTO.ClassShowOutput>()
                        .ForMember(n => n.Name, m => m.MapFrom(input => input.Display));
                #endregion
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
