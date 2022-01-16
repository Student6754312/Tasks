namespace IOServices
{
    public interface IServiceBaseFactory<TS>
    {
        TS GetService();
    }
}