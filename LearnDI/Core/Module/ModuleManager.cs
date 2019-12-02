using Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Module
{
    public class ModuleManager
    {
        public IDIManager DIManager { get; set; }

        public void Bootstrap()
        {
            var baseModuleType = typeof(BaseModule);
            var allModuleTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => baseModuleType.IsAssignableFrom(p) && p.IsAbstract == false).ToList();

            RegisterModule(allModuleTypes);
            Init(allModuleTypes);
        }

        private void Init(List<Type> types)
        {
            var modules = CreateModule(types);
            modules.ForEach(m => m.Module.PreInitialize());
            modules.ForEach(m => m.Module.Initialize());
            modules.ForEach(m => m.Module.PosInitialize());
        }

        private List<ModuleInfo> CreateModule(List<Type> types)
        {
            List<ModuleInfo> modules = new List<ModuleInfo>();

            foreach (var type in types)
            {
                var module = DIManager.Resolve(type) as BaseModule;
                var moduleInfo = new ModuleInfo();
                moduleInfo.Module = module;
                moduleInfo.Type = type;
                modules.Add(moduleInfo);
            }

            return modules;
        }

        private void RegisterModule(List<Type> types)
        {
            foreach (var type in types)
            {
                DIManager.Register(type);
            }
        }
    }
}
