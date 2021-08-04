using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.DataAccess.Infrastructure;
using Ninject.Modules;

namespace NewsPortal.Logic.Common.Util
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}