using IOServices;

namespace Fibonacci.Factory
{
    public interface IInputSelectionFactory
    {
        IInputService GetInputService();
    }
}