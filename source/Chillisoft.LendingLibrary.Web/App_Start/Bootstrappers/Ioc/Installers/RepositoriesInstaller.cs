using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
using Chillisoft.LendingLibrary.DB.Repositories;

namespace Chillisoft.LendingLibrary.Web.Bootstrappers.Ioc.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBorrowerRepository>()
                .ImplementedBy<BorrowerRepository>()
                .LifestylePerWebRequest());
            container.Register(Component.For<IItemRepository>()
                .ImplementedBy<ItemRepository>()
                .LifestylePerWebRequest());
            container.Register(Component.For<IBorrowerItemRepository>()
                .ImplementedBy<BorrowerItemRepository>()
                .LifestylePerWebRequest());
            container.Register(Component.For<IRolesRepository>()
                .ImplementedBy<RoleRepository>()
                .LifestylePerWebRequest());
        }
    }
}