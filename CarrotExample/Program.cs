
using CarrotNet;
using CarrotNet.Blocks;

var targetInfo = TargetInfo.GetStdoutTarget();

UserDefinedStyle style = new();

style.AddRule(blockSelector: "framed-block",
              attributes: new Dictionary<string, object> { ["margin-x"] = 3, ["margin-y"] = 1, });

PlainForm form = new(120, 5, targetInfo);

IBlock[] codeBlocks = [new TextBlock("{"), new IndentBlock(4, new TextBlock("Hello, world!")), new TextBlock("}")];

LineBlock codeColumn = new(codeBlocks, Direction.Down);

CaretUnderlineBlock underline = new(codeColumn, 9);

FramedBlock framed = new(underline);

framed.Render(form, style);

string renderedResult = form.ToPrintableString();

Console.Out.WriteLine(renderedResult);
