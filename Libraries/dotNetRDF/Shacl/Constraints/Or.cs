﻿/*
// <copyright>
// dotNetRDF is free and open source software licensed under the MIT License
// -------------------------------------------------------------------------
// 
// Copyright (c) 2009-2017 dotNetRDF Project (http://dotnetrdf.org/)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is furnished
// to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
*/

namespace VDS.RDF.Shacl.Constraints
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using VDS.RDF.Shacl.Validation;

    internal class Or : Constraint
    {
        [DebuggerStepThrough]
        internal Or(Shape shape, INode node)
            : base(shape, node)
        {
        }

        internal override INode ConstraintComponent
        {
            get
            {
                return Vocabulary.OrConstraintComponent;
            }
        }

        internal override bool Validate(INode focusNode, IEnumerable<INode> valueNodes, Report report)
        {
            var invalidValues =
                from valueNode in valueNodes
                from member in this.Graph.GetListItems(this)
                let shape = Shape.Parse(member)
                group shape.Validate(valueNode) by valueNode into valid
                where !valid.Any(isValid => isValid)
                select valid.Key;

            return ReportValueNodes(focusNode, invalidValues, report);
        }
    }
}