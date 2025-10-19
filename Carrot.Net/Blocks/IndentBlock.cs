namespace Carrot.Net.Blocks;

public sealed class IndentBlock(int indentationLevel, IBlock indentedBlock) : IBlock
{
    public void Render(IForm targetForm)
    {
        indentedBlock.Render(new IndentedForm(indentationLevel, targetForm));
    }
}
