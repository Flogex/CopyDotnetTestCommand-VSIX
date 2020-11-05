using System;

namespace CopyDotnetTestCommand.Model
{
    internal readonly struct CodeElement
    {
        public CodeElement(string fullName, CodeElementKind kind, Project? containingProject)
        {
            this.FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            this.Kind = kind;
            this.ContainingProject = containingProject;
        }

        public string FullName { get; }

        public CodeElementKind Kind { get; }

        public Project? ContainingProject { get; }
    }
}
