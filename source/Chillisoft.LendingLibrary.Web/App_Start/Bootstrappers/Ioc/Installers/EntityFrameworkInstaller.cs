using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Chillisoft.LendingLibrary.DB;

namespace Chillisoft.LendingLibrary.Web.Bootstrappers.Ioc.Installers
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        private Func<ComponentRegistration<ILendingLibraryDbContext>, ComponentRegistration<ILendingLibraryDbContext>> _lifestyleConfiguration;

        public EntityFrameworkInstaller()
        {
            _lifestyleConfiguration = ConfigureLifestylePerRequest;
        }
        public EntityFrameworkInstaller(
            Func<ComponentRegistration<ILendingLibraryDbContext>, ComponentRegistration<ILendingLibraryDbContext>> lifestyleConfiguration)
        {
            _lifestyleConfiguration = lifestyleConfiguration;
        }

        private ComponentRegistration<ILendingLibraryDbContext> ConfigureLifestylePerRequest(ComponentRegistration<ILendingLibraryDbContext> componentRegistration)
        {
            return componentRegistration.LifestylePerWebRequest();
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(_lifestyleConfiguration(Component.For<ILendingLibraryDbContext>()
                .ImplementedBy<LendingLibraryDbContext>()));
        }
    }
}