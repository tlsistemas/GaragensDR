using Autofac;

namespace GaragensDR.Infra.CrossCutting.Ioc
{
    public class ModuleIOC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Carrega IOC
            ConfigurationIOC.Load(builder);
            #endregion
        }
    }
}
