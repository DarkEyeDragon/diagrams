namespace Nodexr.NodeInputs;

using BlazorNodes.Core;

public class InputImage : NodeInputBase
{
    public string ImagePath { get; set; }
    public override int Height => 50;
}
