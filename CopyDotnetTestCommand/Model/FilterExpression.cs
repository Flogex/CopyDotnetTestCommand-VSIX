using System;

namespace CopyDotnetTestCommand.Model
{
    internal readonly struct FilterExpression
    {
        public FilterExpression(string property, FilterOperator filterOperator, string value)
        {
            this.Property = property ?? throw new ArgumentNullException(nameof(property));
            this.Operator = filterOperator;
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Property { get; }

        public FilterOperator Operator { get; }

        public string Value { get; }

        public override string ToString()
        {
            return this.Property + this.Operator.GetSymbol() + this.Value;
        }
    }
}
