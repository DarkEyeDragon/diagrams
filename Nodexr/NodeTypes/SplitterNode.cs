namespace Nodexr.NodeTypes;

using BlazorNodes.Core;
using NodeInputs;

public class SplitterNode : BaseNode
{
    public override string NodeInfo => "This nodes allows you to split a single input into multiple outputs";

    [NodeInput]
    public NodeGroup InputNode { get; set; }
}
