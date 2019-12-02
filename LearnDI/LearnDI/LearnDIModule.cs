using Core.Module;
using LearnDI.Connection;
using LearnDI.Interface;
using LearnDI.Services;

namespace LearnDI
{
    public class LearnDIModule : BaseModule
    {
        public override void PreInitialize()
        {
            DIManager.RegisterDI(typeof(LearnDIModule).Assembly);
            DIManager.Register<IDiService, DiService>(Core.DependencyInjection.DependencyLifeStyle.Singleton);
            DIManager.Register<DiDbContext>();
        }
    }
}
