namespace Nodexr.NodeTypes;

using BlazorNodes.Core;

public class FilledNode : BaseNode
{
    public bool Rounded { get; set; } = true;
    public override IEnumerable<INodeInput> GetAllInputs()
    {
        return new List<INodeInput>();
    }

    public FilledNode()
    {
        Title = "Filled";
    }
}
