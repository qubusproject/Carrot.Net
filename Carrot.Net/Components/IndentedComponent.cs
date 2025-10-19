namespace Carrot.Net.Components;

public sealed class IndentedComponent(int indentationLevel, IComponent component) : IComponent
{
    public void Render(IView targetView)
    {
        component.Render(new IndentedView(indentationLevel, targetView));
    }
}
