namespace Nodexr.NodeTypes;

using BlazorNodes.Core;

public class OutlineNode : BaseNode
{
    public bool Rounded { get; set; } = true;
    public override IEnumerable<INodeInput> GetAllInputs()
    {
        return new List<INodeInput>();
    }

    public OutlineNode()
    {
        Title = "Circle Outline";
    }
}
