
using CarrotNet;
using CarrotNet.Blocks;

var targetInfo = TargetInfo.GetStdoutTarget();

UserDefinedStyle style = new();

style.AddRule(blockSelector: "framed-block",
              attributes: new Dictionary<string, object> { ["margin-x"] = 3, ["margin-y"] = 1, });

PlainForm form = new(120, 5, targetInfo);

GridBlock grid = new(3, 1);

grid[0, 0] = new TextBlock("{");

grid[1, 0] = new IndentBlock(4, new TextBlock("Hello, world!"));

grid[2, 0] = new TextBlock("}");

CaretUnderlineBlock underline = new(grid, 9);

FramedBlock framed = new(underline);

framed.Render(form, style);

string renderedResult = form.ToPrintableString();

Console.Out.WriteLine(renderedResult);
