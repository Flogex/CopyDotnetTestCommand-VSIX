using CopyDotnetTestCommand.Model;
using FluentAssertions;
using Xunit;
using static CopyDotnetTestCommand.Model.CodeElementKind;

namespace CopyDotnetTestCommand.UnitTests
{
    public class FilterCommandBuilderTests
    {
        private static readonly Project _dummyProject = new Project(@"C:\MyProject\MyProject.csproj");

        [Fact]
        public void CommandShouldStartWithDotnetTest()
        {
            var dummyCodeElement = new CodeElement("DummyProject", Namespace, _dummyProject);

            var command = FilterCommandBuilder.Create(dummyCodeElement).ToString();

            command.Should().StartWith("dotnet test");
        }

        [Fact]
        public void WhenCodeElementHasNoContainingProject_CommandShouldNotIncludePathToProject()
        {
            var codeElement = new CodeElement("MyProject", Namespace, null);

            var command = FilterCommandBuilder.Create(codeElement).ToString();

            command.Should().StartWith("dotnet test --filter");
        }

        [Fact]
        public void WhenCodeElementHasContainingProject_CommandShouldIncludePathToProject()
        {
            var project = new Project(@"C:\MyProject\MyProject.csproj");
            var codeElement = new CodeElement("MyProject", Namespace, project);

            var command = FilterCommandBuilder.Create(codeElement).ToString();

            command.Should().StartWith(@"dotnet test ""C:\MyProject\MyProject.csproj""");
        }

        [Fact]
        public void WhenCursorPointsToMethod_FilterShouldUseExactMatchOfMethodName()
        {
            var selectedMethod = new CodeElement("MyProject.TestClass.TestMethod", Method, _dummyProject);

            var command = FilterCommandBuilder.Create(selectedMethod).ToString();

            command.Should().EndWith("--filter \"FullyQualifiedName=MyProject.TestClass.TestMethod\"");
        }

        [Fact]
        public void WhenCursorPointsToClass_FilterShouldUseContainsOperatorWithClassName()
        {
            var selectedClass = new CodeElement("MyProject.TestClass", Class, _dummyProject);

            var command = FilterCommandBuilder.Create(selectedClass).ToString();

            command.Should().EndWith("--filter \"FullyQualifiedName~MyProject.TestClass\"");
        }

        [Fact]
        public void WhenCursorPointsToNamespace_FilterShouldUseContainsOperatorWithNamespace()
        {
            var selectedNamespace = new CodeElement("MyProject", Namespace, _dummyProject);

            var command = FilterCommandBuilder.Create(selectedNamespace).ToString();

            command.Should().EndWith("--filter \"FullyQualifiedName~MyProject\"");
        }
    }
}