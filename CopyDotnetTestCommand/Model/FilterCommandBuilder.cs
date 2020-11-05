namespace CopyDotnetTestCommand.Model
{
    internal static class FilterCommandBuilder
    {
        public static FilterCommand Create(CodeElement activeCodeElement)
        {
            var filePath = GetProjectFilePath(activeCodeElement);
            var filterExpression = GetFilterExpression(activeCodeElement);
            return new FilterCommand(filePath, filterExpression);
        }

        private static FilterExpression GetFilterExpression(CodeElement codeElement)
        {
            // If a class or namespace is selected, 'contains' operator is used
            var filterOperator = codeElement.Kind == CodeElementKind.Method
                ? FilterOperator.ExactMatch
                : FilterOperator.Contains;

            return new FilterExpression("FullyQualifiedName", filterOperator, codeElement.FullName);
        }

        private static string GetProjectFilePath(CodeElement codeElement)
        {
            return codeElement.ContainingProject?.FilePath;
        }
    }
}
