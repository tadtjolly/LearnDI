using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Proxy;
using System;
using System.Reflection;
using System.Text;

namespace Core.DependencyInjection
{
    public static class TestWindsorExtensions
    {
        public static ComponentRegistration<T> OverridesExistingRegistration<T>(this ComponentRegistration<T> componentRegistration) where T : class
        {
            return componentRegistration
                                .Named(Guid.NewGuid().ToString())
                                .IsDefault();
        }
    }

    public class DIManager : IDIManager
    {
        public static DIManager Instance { get; private set; }

        private static readonly ProxyGenerator ProxyGeneratorInstance = new ProxyGenerator();

        public IWindsorContainer IocContainer { get; private set; }
        static DIManager()
        {
            Instance = new DIManager();
        }

        public DIManager()
        {
            IocContainer = CreateContainer();
            IocContainer.Register(
                Component
                    .For<DIManager, IDIManager>()
                    .Instance(this)
            );
        }

        private IWindsorContainer CreateContainer()
        {
            return new WindsorContainer(new DefaultProxyFactory(ProxyGeneratorInstance));
        }

        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }

        public void RegisterDI(Assembly assembly)
        {
            IocContainer.Register(
                Classes.FromAssembly(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransient>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );

            IocContainer.Register(
                Classes.FromAssembly(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ISingleton>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton()
                );
        }

        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        public void RegisterInstance<TType>(TType instance, bool @override = true)
        {
            if (@override)
            {
                IocContainer.Register(ApplyLifestyle(Component.For(typeof(TType)).Instance(instance).OverridesExistingRegistration(), DependencyLifeStyle.Singleton));
            }
            else
            {
                IocContainer.Register(ApplyLifestyle(Component.For(typeof(TType)).Instance(instance), DependencyLifeStyle.Singleton));
            }
        }

        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        public void RegisterServiceBase<T>(Assembly assembly)
        {
            IocContainer.Register(
                Classes.FromAssembly(assembly)
                    .BasedOn<T>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .LifestyleTransient()
                );
        }
    }
}
