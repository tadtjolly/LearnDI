using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.DependencyInjection
{
    public interface IDIManager
    {
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class;
        void RegisterInstance<TType>(TType instance, bool @override = true);
        T Resolve<T>();

        T Resolve<T>(Type type);

        object Resolve(Type type);

        void RegisterDI(Assembly assembly);
        void RegisterServiceBase<T>(Assembly assembly);

        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

    }
}
