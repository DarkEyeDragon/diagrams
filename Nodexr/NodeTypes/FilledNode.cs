namespace Nodexr.NodeTypes;

using BlazorNodes.Core;
using NodeInputs;
using Utils;

public class FilledNode : BaseNode
{
    public override string Title => "Circular Node";

    public override string NodeInfo => "";

    [NodeInput] public InputImage Input { get; } = new InputImage { ImagePath = "images/whatson.png", Title = "Test", Description = "Test"};
}
