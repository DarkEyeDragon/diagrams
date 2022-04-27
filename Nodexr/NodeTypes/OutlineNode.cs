namespace Nodexr.NodeTypes;

using BlazorNodes.Core;
using NodeInputs;
using Utils;

public class OutlineNode : BaseNode
{
    public override string Title { get; set; } = "Outline Node";

    public override string NodeInfo => "Inserts text into your Regex which will be interpreted literally, " +
                                       "so all special characters are escaped with a backslash. E.g. $25.99? becomes \\$25\\.99\\?" +
                                       "\nNote: Backslash characters (\\), and the character immediately following them, are not escaped." +
                                       "\nTo insert a string with no escaping, turn off the 'Escape' option. Warning: this may create an invalid or unexpected output.";

    [NodeInput] public InputString Input { get; } = new InputString("Content");
    public bool Rounded { get => Input.Round; set => Input.Round = value; }
}
