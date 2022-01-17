namespace IOServices.ServiceFactory.Base
{
    public interface IServiceBaseFactory<TS>
    {
        TS GetService();
    }
}