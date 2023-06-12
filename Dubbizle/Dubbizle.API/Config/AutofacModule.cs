using Autofac;
using Dubbizle.Data;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dubbizle.API.Config
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(Context)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(CategoryServise).Assembly).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(AdvertismentService).Assembly).InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(AdvertismentServise).Assembly).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(SubCategory_FilterService).Assembly).InstancePerLifetimeScope();


            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>();
            // builder.RegisterAssemblyTypes(typeof(BranchService).Assembly).InstancePerLifetimeScope();

            //builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>();

            //builder.RegisterType<UserManager<ApplicationUser>>();

            //builder.RegisterType<UserManager<IdentityRole>>();

        }
    }
}
