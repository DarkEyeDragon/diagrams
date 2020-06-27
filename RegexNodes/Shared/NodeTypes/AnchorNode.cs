﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegexNodes.Shared.NodeTypes
{
    public class AnchorNode : Node
    {
        public override string Title => "Anchor";
        public override string NodeInfo => "Inserts a start-of-line or end-of-line character. Useful for ensuring that your regex only matches if it's at a specific position in a line.";

        [NodeInput]
        protected InputDropdown InputAnchorType { get; } = new InputDropdown(Modes.startLine, Modes.endLine, Modes.wordBoundary) { Title = "Type of anchor:" };

        private static class Modes
        {
            public const string startLine = "Start of line";
            public const string endLine = "End of line";
            public const string wordBoundary = "Word boundary";
        }

        protected override string GetValue()
        {
            string result;
            switch (InputAnchorType.DropdownValue)
            {
                case Modes.startLine: result = "^"; break;
                case Modes.endLine: result = "$"; break;
                case Modes.wordBoundary: result = "\\b"; break;
                default: result = ""; break;
            }
            return result;
        }
    }
}
