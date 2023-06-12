using Autofac;
using Dubbizle.Data;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.Services;
using Mapper;

namespace Dubbizle.API.Config
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(Context)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //builder.RegisterAssemblyTypes(typeof(ProfileService).Assembly).InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(ProfileMap).Assembly).InstancePerLifetimeScope();


            // builder.RegisterAssemblyTypes(typeof(BranchService).Assembly).InstancePerLifetimeScope();
        }
    }
}
