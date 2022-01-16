using IOServices;

namespace Fibonacci.Services.ServiceFactory
{
    public interface IInputSelectionFactory
    {
        IInputService GetInputService();
    }
}