using NServiceBus;
using NServiceBus.Logging;

public class MyService
{
    static ILog logger = LogManager.GetLogger<MyService>();
    
    public void WriteHello()
    {
        logger.Info("Hello from MyService.");
    }
}

public interface ITesterke
{
}

public class Testerke : ITesterke
{
    readonly IBus bus;

    public Testerke(IBus bus)
    {
        this.bus = bus;
    }
}