namespace Nodexr.NodeTypes;

using System.Reflection;
using BlazorNodes.Core;
using NodeInputs;

public abstract class BaseNode : INodeViewModel
{
    public string Styling { get; set; } = "black 2px solid";
    public InputProcedural Previous { get; } = new();

    public IInputPort PrimaryInput => Previous;
    public INodeOutput<NodeResult>? PreviousNode
    {
        get => Previous.ConnectedNode;
        set => Previous.ConnectedNode = value;
    }
    public string NodeInfo { get; }
    public string Title { get; set; } = "Title";
    public bool IsCollapsed { get; set; } = false;
    private Vector2 pos;

    public Vector2 Pos
    {
        get => pos;
        set
        {
            pos = value;
            CalculateInputsPos();
        }
    }    
    public bool Selected { get; set; }
    public IEnumerable<INodeInput> NodeInputs { get; }
    public string OutputTooltip { get; }

    public BaseNode()
    {
        var inputProperties = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(prop => Attribute.IsDefined(prop, typeof(NodeInputAttribute)));

        NodeInputs = inputProperties
            .Select(prop => prop.GetValue(this))
            .OfType<INodeInput>()
            .ToList();
    }

    public void CalculateInputsPos()
    {
        Previous.Pos = new Vector2(Pos.x + 2, Pos.y + 13);
        if (IsCollapsed)
        {
            const int startHeight = 13;
            foreach (var input in NodeInputs)
            {
                switch (input)
                {
                    case InputProcedural input1:
                        input1.Pos = new Vector2(Pos.x + 2, Pos.y + startHeight);
                        break;
                    case InputCollection input1:
                        input1.Pos = new Vector2(Pos.x + 2, Pos.y + startHeight);
                        foreach (var input2 in input1.Inputs)
                        {
                            input2.Pos = new Vector2(Pos.x + 2, Pos.y + startHeight);
                        }

                        break;
                }
            }
        }
    }

    public void OnLayoutChanged(object? sender, EventArgs e)
    {
        CalculateInputsPos();
        foreach (var input in GetAllInputs().OfType<IInputPort>())
            input.Refresh();
        LayoutChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Get all of the inputs to the node, including the 'previous' input and the sub-inputs of any InputCollections.
    /// InputCollections themselves are not returned.
    /// </summary>
    public virtual IEnumerable<INodeInput> GetAllInputs()
    {
        yield return Previous;
        foreach (var input in NodeInputs)
        {
            if (input is InputCollection coll)
            {
                foreach (var subInput in coll.Inputs)
                    yield return subInput;
            }
            else
            {
                yield return input;
            }
        }
    }

    public event EventHandler? LayoutChanged;
    public event EventHandler? SelectionChanged;
    public Vector2 OutputPos => Pos + new Vector2(154, 13);
    public string CssName { get; }
    public string CssColor { get; } = "orange";
    public event EventHandler? OutputChanged;
}
