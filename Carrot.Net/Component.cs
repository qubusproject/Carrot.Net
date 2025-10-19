namespace Carrot.Net;

public interface IComponent
{
    void Render(IView targetView);
}