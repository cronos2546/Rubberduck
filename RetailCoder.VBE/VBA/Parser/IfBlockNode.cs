﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Rubberduck.VBA.Parser.Grammar;

namespace Rubberduck.VBA.Parser
{
    [ComVisible(false)]
    public class IfBlockNode : CodeBlockNode
    {
        public IfBlockNode(Instruction instruction, string scope, Match match, IEnumerable<SyntaxTreeNode> nodes)
            : base(instruction, scope, match, match.Groups["expression"].Success ? new string[]{} : new[] {ReservedKeywords.End+" "+ReservedKeywords.If}, null, nodes)
        {
        }

        public Expression Condition { get { return new Expression(RegexMatch.Groups["condition"].Value); } }
        public Expression Expression { get { return new Expression(RegexMatch.Groups["expression"].Value); } }
    }

    [ComVisible(false)]
    public class ForLoopNode : CodeBlockNode
    {
        public ForLoopNode(Instruction instruction, string scope, Match match, IEnumerable<SyntaxTreeNode> nodes) 
            : base(instruction, scope, match, new[] {ReservedKeywords.Next}, null, nodes)
        {
        }

        public Expression Lower { get { return new Expression(RegexMatch.Groups["lower"].Value); } }
        public Expression Upper { get { return new Expression(RegexMatch.Groups["upper"].Value); } }
        public Expression Step { get { return new Expression(RegexMatch.Groups["step"].Value); } }
    }
}