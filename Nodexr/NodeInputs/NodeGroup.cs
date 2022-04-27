namespace Nodexr.NodeInputs;

using BlazorNodes.Core;

public class NodeGroup : NodeInputBase
{
    public BaseNode InputNode { get; set; }
    public IList<BaseNode> ExitNodes { get; } = new List<BaseNode>();

    public void AddExitNode(BaseNode node)
    {
        ExitNodes.Add(node);
    }

    public bool RemoveExitNode(BaseNode node)
    {
        return ExitNodes.Remove(node);
    }
}
