﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegexNodes.Shared.NodeTypes
{
    public class WildcardNode : Node
    {
        public override string Title => "Wildcard";
        public override string NodeInfo => "Matches any of the specified types of character. Note: the 'Everything' option will only match newlines if the Regex is in singleline mode.";

        [NodeInput]
        protected InputCheckbox InputAllowAll { get; } = new InputCheckbox(true) { Title = "Everything" };
        [NodeInput]
        protected InputCheckbox InputAllowWhitespace { get; } = new InputCheckbox() { Title = "Whitespace" };
        [NodeInput]
        protected InputCheckbox InputAllowUppercase { get; } = new InputCheckbox() { Title = "Uppercase Letters" };
        [NodeInput]
        protected InputCheckbox InputAllowLowercase { get; } = new InputCheckbox() { Title = "Lowercase Letters" };
        [NodeInput]
        protected InputCheckbox InputAllowDigits { get; } = new InputCheckbox() { Title = "Digits" };
        [NodeInput]
        protected InputCheckbox InputAllowUnderscore { get; } = new InputCheckbox() { Title = "Underscore" };
        [NodeInput]
        protected InputCheckbox InputAllowOther { get; } = new InputCheckbox() { Title = "Other" };

        [NodeInput]
        public InputDropdown InputCount { get; } = new InputDropdown(
            Quantifier.Repetitions.one,
            Quantifier.Repetitions.zeroOrMore,
            Quantifier.Repetitions.oneOrMore,
            Quantifier.Repetitions.zeroOrOne,
            Quantifier.Repetitions.number,
            Quantifier.Repetitions.range)
        { Title = "Repetitions:" };
        [NodeInput]
        public InputNumber InputNumber { get; } = new InputNumber(0, min: 0) { Title = "Amount:" };
        [NodeInput]
        public InputNumber InputMin { get; } = new InputNumber(0, min: 0) { Title = "Minimum:" };
        [NodeInput]
        public InputNumber InputMax { get; } = new InputNumber(1, min: 0) { Title = "Maximum:" };

        public WildcardNode()
        {
            bool isAllowAllUnchecked() => !InputAllowAll.IsChecked;

            InputAllowWhitespace.IsEnabled = isAllowAllUnchecked;
            InputAllowUnderscore.IsEnabled = isAllowAllUnchecked;
            InputAllowUppercase.IsEnabled = isAllowAllUnchecked;
            InputAllowLowercase.IsEnabled = isAllowAllUnchecked;
            InputAllowDigits.IsEnabled = isAllowAllUnchecked;
            InputAllowOther.IsEnabled = isAllowAllUnchecked;

            InputNumber.IsEnabled = () => InputCount.DropdownValue == Quantifier.Repetitions.number;
            InputMin.IsEnabled = () => InputCount.DropdownValue == Quantifier.Repetitions.range;
            InputMax.IsEnabled = () => InputCount.DropdownValue == Quantifier.Repetitions.range;
        }

        protected override string GetValue()
        {
            string result;

            string suffix = Quantifier.Repetitions.GetSuffix(
                InputCount.DropdownValue,
                InputNumber.InputContents,
                InputMin.GetValue(),
                InputMax.GetValue());

            if (InputAllowAll.IsChecked)
            {
                result = ".";
                return result + suffix;
            }

            var inputs = (
                w: InputAllowWhitespace.IsChecked,
                L: InputAllowUppercase.IsChecked,
                l: InputAllowLowercase.IsChecked,
                d: InputAllowDigits.IsChecked,
                u: InputAllowUnderscore.IsChecked,
                o: InputAllowOther.IsChecked
                );

            result = inputs switch
            {
                //Handle special cases where simplification is possible - when "other' is ticked
                (true, true, true, true, true, true) => @".",
                (true, true, true, false, true, true) => @"\D",
                (false, true, true, true, true, true) => @"\S",
                (true, false, false, false, false, true) => @"\W",

                //Handle special cases where simplification is possible - when "other' is not ticked
                (false, false, false, false, false, false) => "",
                (false, false, false, false, true, false) => @"_",
                (false, false, false, true, false, false) => @"\d",
                (true, false, false, false, false, false) => @"\s",
                (false, true, true, true, true, false) => @"\w",

                //Handle general case when "other' is ticked
                _ when inputs.o => "[^" + GetClassContents(w: !inputs.w, L: !inputs.L, l: !inputs.l, d: !inputs.d, u: !inputs.u) + "]",
                //Handle general case when "other' is not ticked
                _ => "[" + GetClassContents(w: inputs.w, L: inputs.L, l: inputs.l, d: inputs.d, u: inputs.u) + "]",
            };

            return result + suffix;
        }

        private string GetClassContents(bool w, bool L, bool l, bool d, bool u)
        {
            string result = (w ? "\\s" : "");
            if(L && l && d && u)
            {
                result += "\\w";
            }
            else
            {
                result +=
                    (L ? "A-Z" : "") +
                    (l ? "a-z" : "") +
                    (d ? "\\d" : "") +
                    (u ? "_" : "");
            }
            return result;
        }
    }
}
