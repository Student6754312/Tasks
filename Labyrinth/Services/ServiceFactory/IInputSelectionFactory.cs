using IOServices;

namespace Labyrinth.Services.ServiceFactory
{
    public interface IInputSelectionFactory
    {
        IInputService GetInputService();
    }
}