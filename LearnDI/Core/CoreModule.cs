using Core.Module;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class CoreModule : BaseModule
    {
        public override void PreInitialize()
        {
            DIManager.RegisterDI(typeof(CoreModule).Assembly);
        }
    }
}
