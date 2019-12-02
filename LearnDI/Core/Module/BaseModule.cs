using Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Module
{
    public abstract class BaseModule
    {
        public IDIManager DIManager { get; set; }
        public virtual void PreInitialize()
        {

        }

        public virtual void Initialize()
        {

        }
        public virtual void PosInitialize()
        {

        }
    }
}
