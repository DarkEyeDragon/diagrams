namespace Nodexr.Services;
using Microsoft.AspNetCore.Components;
using Nodexr.NodeTypes;
using Microsoft.AspNetCore.WebUtilities;
using Blazored.Toast.Services;
using BlazorNodes.Core;
using Nodexr.Nodes;

public interface INodeHandler
{
    NodeResult CachedOutput { get; }

    event EventHandler OutputChanged;
    event EventHandler OnRequireNoodleRefresh;
    event EventHandler OnRequireNodeGraphRefresh;

    NodeTree Tree { get; }

    void DeleteSelectedNodes();
    void ForceRefreshNodeGraph();
    void ForceRefreshNoodles();
    void RevertPreviousParse();
}

public class NodeHandler : INodeHandler
{
    private NodeTree? tree;

    /// <summary>
    /// The <see cref="NodeTree"/> containing the collection of nodes in the current expression.
    /// </summary>
    public NodeTree Tree
    {
        get => tree!;
        private set
        {
            // TODO refactor this. The current implementation breaks if the Output node is replaced.
            if (tree != null)
            {
                var result = GetOutputNode(tree);
                if(result != null) result.OutputChanged -= OnOutputChanged;
            }
            var result2 = GetOutputNode(value);
            if (result2 != null)
            {
                result2.OutputChanged += OnOutputChanged;
            }
            tree = value;
        }
    }

    public NodeResult CachedOutput => GetOutputNode(Tree).CachedOutput;

    /// <summary>
    /// Called when the output of the node graph has changed.
    /// </summary>
    public event EventHandler? OutputChanged;

    /// <summary>
    /// Called when the state of the noodles has changed, but the noodles have not been re-rendered automatically.
    /// </summary>
    public event EventHandler? OnRequireNoodleRefresh;

    /// <summary>
    /// Called when the state of the node graph has changed, but the node graph has not been re-rendered automatically.
    /// </summary>
    public event EventHandler? OnRequireNodeGraphRefresh;

    private readonly IToastService toastService;

    //Stores the previous tree from before the most recent parse, so that the parse can be reverted.
    private NodeTree? treePrevious;

    public NodeHandler(NavigationManager navManager, IToastService toastService)
    {
        this.toastService = toastService;
        if (Tree is null)
        {
            Tree = CreateDefaultNodeTree();
        }
    }

    public void RevertPreviousParse()
    {
        Tree = treePrevious ?? throw new InvalidOperationException("No previous tree to revert to");
        ForceRefreshNodeGraph();
        OnOutputChanged(this, EventArgs.Empty);
    }

    public void ForceRefreshNodeGraph()
    {
        OnRequireNodeGraphRefresh?.Invoke(this, EventArgs.Empty);
    }

    public void ForceRefreshNoodles()
    {
        OnRequireNoodleRefresh?.Invoke(this, EventArgs.Empty);
    }

    public void DeleteSelectedNodes()
    {
        var selectedNodes = Tree.GetSelectedNodes().ToList();
        if (selectedNodes.Count == 0) return;

        foreach (var node in selectedNodes)
        {
            if (node is OutputNode)
            {
                toastService.ShowInfo("", "Can't delete the Output node");
            }
            else
            {
                Tree.DeleteNode(node);
            }
        }
        OutputChanged?.Invoke(this, EventArgs.Empty);
        ForceRefreshNodeGraph();
    }

    private static OutputNode? GetOutputNode(NodeTree tree)
    {
        if (!tree.Nodes.Any()) return null;
        return tree.Nodes.OfType<OutputNode>().SingleOrDefault();
    }

    private void OnOutputChanged(object? sender, EventArgs e)
    {
        OutputChanged?.Invoke(this, e);
    }

    private static NodeTree CreateDefaultNodeTree()
    {
        var tree = new NodeTree();
        var outlineNode = new OutlineNode() { Pos = new(200, 300) };
        var outlineNode2 = new OutlineNode() { Pos = new(200, 450) };
        tree.AddNode(outlineNode);
        tree.AddNode(outlineNode2);
        return tree;
    }
}
