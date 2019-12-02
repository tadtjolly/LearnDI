using Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDi
{
    public class WebDiModule : BaseModule
    {
        public override void PreInitialize()
        {
            var assembly = typeof(WebDiModule).Assembly;
            DIManager.RegisterDI(assembly);
        }
    }
}
