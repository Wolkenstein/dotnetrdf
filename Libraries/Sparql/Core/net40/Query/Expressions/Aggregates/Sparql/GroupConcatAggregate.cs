﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF.Query.Aggregation;
using VDS.RDF.Query.Aggregation.Sparql;
using VDS.RDF.Specifications;
using VDS.RDF.Writing.Formatting;

namespace VDS.RDF.Query.Expressions.Aggregates.Sparql
{
    public class GroupConcatAggregate
        : BaseAggregate
    {
        public GroupConcatAggregate(IExpression expr)
            : this(expr, null) {}

        public GroupConcatAggregate(IExpression expr, IExpression separatorExpr)
            : base(MakeArguments(expr, separatorExpr)) {}

        private static IEnumerable<IExpression> MakeArguments(IExpression expr, IExpression separatorExpr)
        {
            return separatorExpr != null ? new IExpression[] {expr, separatorExpr} : expr.AsEnumerable();
        }

        public override IExpression Copy(IEnumerable<IExpression> args)
        {
            List<IExpression> exprs = args.ToList();
            return exprs.Count == 1 ? new GroupConcatAggregate(exprs[0], null) : new GroupConcatAggregate(exprs[0], exprs[1]);
        }

        public override string Functor
        {
            get { return SparqlSpecsHelper.SparqlKeywordGroupConcat; }
        }

        public override IAccumulator CreateAccumulator()
        {
            return this.Arguments.Count > 1 ? new GroupConcatAccumulator(this.Arguments[0], this.Arguments[1]) : new GroupConcatAccumulator(this.Arguments[0]);
        }

        public override string ToString(IAlgebraFormatter formatter)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.Functor.ToLowerInvariant());
            builder.Append('(');
            for (int i = 0; i < this.Arguments.Count - 1; i++)
            {
                if (i > 0) builder.Append(", ");
                builder.Append(this.Arguments[i].ToString(formatter));
            }
            if (this.Arguments.Count > 1)
            {
                builder.Append("; SEPARATOR = ");
                builder.Append(this.Arguments[this.Arguments.Count - 1].ToString(formatter));
            }
            builder.Append(")");
            return builder.ToString();
        }

        public override string ToPrefixString(IAlgebraFormatter formatter)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('(');
            builder.Append(this.Functor.ToLowerInvariant());
            if (this.Arguments.Count > 1)
            {
                builder.Append(" (separator ");
                builder.Append(this.Arguments[this.Arguments.Count - 1].ToPrefixString(formatter));
                builder.Append(')');
            }
            for (int i = 0; i < this.Arguments.Count - 1; i++)
            {
                builder.Append(' ');
                builder.Append(this.Arguments[i].ToPrefixString(formatter));
            }
            builder.Append(')');
            return builder.ToString();
        }
    }
}