namespace CopyDotnetTestCommand.Model
{
    internal readonly struct FilterCommand
    {
        public FilterCommand(string projectPath, FilterExpression filterExpression)
        {
            this.ProjectPath = projectPath;
            this.FilterExpression = filterExpression;
        }

        public string ProjectPath { get; }

        public FilterExpression FilterExpression { get; }

        public override string ToString()
        {
            return this.ProjectPath is null
                ? $"dotnet test --filter \"{this.FilterExpression}\""
                : $"dotnet test \"{this.ProjectPath}\" --filter \"{this.FilterExpression}\"";
        }
    }
}
