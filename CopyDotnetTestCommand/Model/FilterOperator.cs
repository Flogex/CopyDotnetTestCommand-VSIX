using System;
using static CopyDotnetTestCommand.Model.FilterOperator;

namespace CopyDotnetTestCommand.Model
{
    internal enum FilterOperator
    {
        ExactMatch,
        NotExactMatch,
        Contains,
        NotContains
    }

    internal static class FilterOperatorExtensions
    {
        public static string GetSymbol(this FilterOperator filterOperator)
        {
            switch (filterOperator)
            {
                case ExactMatch: return "=";
                case NotExactMatch: return "!=";
                case Contains: return "~";
                case NotContains: return "!~";
                default: throw new NotSupportedException($"Unknown {nameof(FilterOperator)}.");
            }
        }
    }
}
