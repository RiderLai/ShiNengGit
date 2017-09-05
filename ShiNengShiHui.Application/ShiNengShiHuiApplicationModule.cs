using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using ShiNengShiHui.Users;
using ShiNengShiHui.AppServices.AdministratorDTO;
using ShiNengShiHui.Entities.Teachers;

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
                mapper.CreateMap<User, UserShowOutput>()
                        .ForMember(n => n.TeacherName, m => m.MapFrom(input => input.Teacher.Name));

                mapper.CreateMap<Teacher, TeacherShowOutput>()
                        .ForMember(n => n.ClassName, m => m.MapFrom(input => input.Class.Display));
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
