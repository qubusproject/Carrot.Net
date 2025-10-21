
using CarrotNet;
using CarrotNet.Blocks;

var targetInfo = TargetInfo.GetStdoutTarget();

PlainForm form = new(120, 5, targetInfo);

GridBlock grid = new(3, 1);

grid[0, 0] = new TextBlock("{");

grid[1, 0] = new IndentBlock(4, new TextBlock("Hello, world!"));

grid[2, 0] = new TextBlock("}");

CaretUnderlineBlock underline = new(grid, 9);

FrameBlock frame = new(underline, 3, 1);

frame.Render(form);

string renderedResult = form.ToPrintableString();

Console.Out.WriteLine(renderedResult);
