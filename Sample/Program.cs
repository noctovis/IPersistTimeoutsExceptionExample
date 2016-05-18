using System;
using Microsoft.Practices.Unity;
using NServiceBus;

static class Program
{
    static void Main()
    {
        Console.Title = "Samples.Unity";
        #region ContainerConfiguration
        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Test");
        busConfiguration.UseTransport<AzureServiceBusTransport>().ConnectionString("xxx");
        busConfiguration.UsePersistence<InMemoryPersistence>();
        //busConfiguration.License(ConfigurationService.Instance.NServiceBusLicense);
        //LogManager.UseFactory(new NServiceBusToStackifyLoggerFactory());

        UnityContainer container = new UnityContainer();
        container.RegisterInstance(new MyService());
        container.RegisterType<ITesterke, Testerke>();
        busConfiguration.UseContainer<UnityBuilder>(c => c.UseExistingContainer(container));
        #endregion

        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.EnableInstallers();

        using (IBus bus = Bus.Create(busConfiguration).Start())
        {
            bus.SendLocal(new MyMessage());
            var testerke = container.Resolve<ITesterke>();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}