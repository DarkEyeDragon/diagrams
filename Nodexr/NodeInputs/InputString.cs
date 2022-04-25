namespace Nodexr.NodeInputs;

using BlazorNodes.Core;

public class InputString : NodeInputBase
{
    private string _value;

    public string Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged();
        }
    }

    public override int Height => 150;
    public override int Width => 150;

    public bool Round { get; set; }

    public InputString(string contents)
    {
        _value = contents;
    }

    public string GetValue() => Value;
}
