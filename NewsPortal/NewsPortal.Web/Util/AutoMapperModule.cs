using AutoMapper;
using NewsPortal.Web.Mappings;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace NewsPortal.Web.Util
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToMethod(CreateConfiguration).InSingletonScope();
        }

        private IMapper CreateConfiguration(IContext context)
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile<ViewModelToDomainModelProfile>();
                config.AddProfile<DomainModelToViewModelProfile>();
            })
                .CreateMapper(type => context.Kernel.Get(type));
        }
    }
}