﻿using System.Collections.Generic;

namespace RegexNodes.Shared.NodeTypes
{
    public class IfElseNode : Node
    {
        public override string Title => "If-Else";
        public override string NodeInfo => "[Todo]";

        public override List<INodeInput> NodeInputs
        {
            get
            {
                return new List<INodeInput> { Input, InputGroupType, GroupName, CustomPrefix };
            }
        }

        protected InputProcedural Input { get; set; } = new InputProcedural();

        protected InputDropdown InputGroupType { get; set; } = new InputDropdown(
            "Capturing",
            "Non-capturing",
            "Named",
            "Custom")
        { Title = "Type of group:" };

        protected InputString GroupName { get; set; } = new InputString("") { Title = "Name:" };
        protected InputString CustomPrefix { get; set; } = new InputString("") { Title = "Prefix:" };

        public IfElseNode()
        {
            GroupName.IsEnabled = (() => InputGroupType.DropdownValue == "Named");
            CustomPrefix.IsEnabled = (() => InputGroupType.DropdownValue == "Custom");
        }

        public override string GetValue()
        {
            string input = Input.GetValue().RemoveNonCapturingGroup();
            string prefix = "";
            switch (InputGroupType.DropdownValue)
            {
                case "Capturing": prefix = "("; break;
                case "Non-capturing": prefix = "(?:"; break;
                case "Named": prefix = $"(?<{GroupName.GetValue()}>"; break;
                case "Custom": prefix = "(" + CustomPrefix.GetValue(); break;
            };
            //string prefix = (InputGroupType.Value == "Capturing") ? "(" : "(?:";
            //if (input.StartsWith("(?:") && input.EndsWith(")"))
            //{
            //    return UpdateCache(prefix + input.Substring(3));
            //}
            return UpdateCache($"{prefix}{input})");
        }
    }
}
