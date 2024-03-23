using Autofac;
using BootcampApp.Core.IUnitOfWorks;
using BootcampApp.Core.Repositories;
using BootcampApp.Repository;
using BootcampApp.Repository.Repositories;
using BootcampApp.Repository.UnitOfWorks;
using BootcampApp.Service.Mapping;
using BootcampApp.Service.Services;
using NLayer.Core.Services;
using System.Reflection;
using Module = Autofac.Module;


namespace BootcampApp.Web.Modules
{
    public class RepoServiceModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<BootcampAppDbContext>().AsSelf().InstancePerLifetimeScope();


            var webAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(BootcampAppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(webAssembly, repoAssembly!, serviceAssembly!).Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(webAssembly, repoAssembly!, serviceAssembly!).Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();


            //InstancePerLifetimeScope => Scope karşılık gelir
            //InstancePerDependency => transiet karşılık gelir
        }
    }
}
